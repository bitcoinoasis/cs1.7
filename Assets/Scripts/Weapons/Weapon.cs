using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData weaponData;
    
    [Header("References")]
    public Camera playerCamera;
    public Transform weaponHolder;
    public GameObject muzzleFlash;
    public AudioSource audioSource;
    
    [Header("Events")]
    public UnityEvent<int, int> OnAmmoChanged; // current ammo, reserve ammo
    
    [Header("Race Modifiers")]
    public float damageMultiplier = 1.0f; // Set by AbilitySystem
    public float nextShotDamageMultiplier = 1.0f; // For critical strikes, reset after each shot
    
    [Header("Ability Hooks")]
    public System.Action onBeforeShoot; // Called before shooting
    public System.Action<GameObject, float> onHitEnemy; // Called when hitting enemy (enemy, damage)
    public System.Action<float> onDealDamage; // Called when dealing damage (amount)
    
    private int currentAmmo;
    private int reserveAmmo;
    
    // Public properties for ability access
    public int CurrentAmmo 
    { 
        get => currentAmmo; 
        set 
        { 
            currentAmmo = value; 
            OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
        } 
    }
    public int ReserveAmmo 
    { 
        get => reserveAmmo; 
        set 
        { 
            reserveAmmo = value; 
            OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
        } 
    }
    
    private float nextFireTime = 0f;
    private bool isReloading = false;
    private float currentRecoil = 0f;
    private Crosshair crosshair;
    
    private void Start()
    {
        if (weaponData != null)
        {
            currentAmmo = weaponData.magazineSize;
            reserveAmmo = weaponData.reserveAmmo;
            OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
        }
        
        // Auto-find camera if not assigned
        if (playerCamera == null)
        {
            // Try to find the player's camera by going up the hierarchy
            Transform parent = transform.parent;
            while (parent != null)
            {
                // Look for camera in this parent or its children
                Camera cam = parent.GetComponentInChildren<Camera>();
                if (cam != null)
                {
                    playerCamera = cam;
                    break;
                }
                parent = parent.parent;
            }
            
            // Fallback: Try to find camera tagged as MainCamera
            if (playerCamera == null)
            {
                playerCamera = Camera.main;
            }
            
            // Last resort: Find any camera
            if (playerCamera == null)
            {
                playerCamera = FindFirstObjectByType<Camera>();
            }
        }
        
        // Find crosshair
        crosshair = FindFirstObjectByType<Crosshair>();
    }
    
    private void Update()
    {
        if (isReloading) return;
        
        // Handle shooting
        if (weaponData.isAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
            }
        }
        
        // Handle reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < weaponData.magazineSize && reserveAmmo > 0)
        {
            StartReload();
        }
        
        // Apply recoil recovery
        if (currentRecoil > 0)
        {
            currentRecoil = Mathf.Lerp(currentRecoil, 0, weaponData.recoilReturnSpeed * Time.deltaTime);
        }
    }
    
    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            // Auto reload if out of ammo
            if (reserveAmmo > 0)
            {
                StartReload();
            }
            return;
        }
        
        // Call ability hooks BEFORE shooting (for Critical Strike, etc.)
        onBeforeShoot?.Invoke();
        
        // Track shot time for invisibility
        TrackShotTime();
        
        currentAmmo--;
        nextFireTime = Time.time + weaponData.fireRate;
        OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
        
        // Notify crosshair
        if (crosshair != null)
        {
            crosshair.OnShoot();
        }
        
        // Apply recoil
        currentRecoil += weaponData.recoilAmount;
        
        // Raycast for hit detection
        Vector3 shootDirection = playerCamera.transform.forward;
        
        // Add recoil spread
        shootDirection.x += Random.Range(-currentRecoil, currentRecoil) * 0.01f;
        shootDirection.y += Random.Range(-currentRecoil, currentRecoil) * 0.01f;
        
        RaycastHit hit;
        Vector3 shootStart = playerCamera.transform.position;
        
        if (Physics.Raycast(shootStart, shootDirection, out hit, weaponData.range))
        {
            // Create bullet tracer to hit point
            BulletEffect.CreateTracer(shootStart, hit.point, Color.yellow, 0.05f, 0.1f);
            
            // Create impact effect
            BulletEffect.CreateImpact(hit.point, hit.normal, Color.red, 0.2f, 0.15f);
            
            // First check if we hit a hitbox (for precise damage)
            Hitbox hitbox = hit.collider.GetComponent<Hitbox>();
            if (hitbox != null)
            {
                // Calculate final damage with race modifiers and critical strikes
                float finalDamage = weaponData.damage * damageMultiplier * nextShotDamageMultiplier;
                
                // Apply damage through hitbox (it handles multipliers)
                hitbox.TakeDamage(finalDamage);
                
                // Call ability hooks
                onHitEnemy?.Invoke(hit.collider.gameObject, finalDamage);
                onDealDamage?.Invoke(finalDamage);
                
                // Show hit marker for headshots
                if (hitbox.hitboxType == HitboxType.Head)
                {
                    Debug.Log("⊕ HEADSHOT!");
                    // Make impact effect brighter for headshots
                    BulletEffect.CreateImpact(hit.point, hit.normal, Color.yellow, 0.3f, 0.2f);
                }
                
                // Reset next shot multiplier
                nextShotDamageMultiplier = 1.0f;
            }
            else
            {
                // Calculate final damage
                float finalDamage = weaponData.damage * damageMultiplier * nextShotDamageMultiplier;
                
                // Fallback: Check if we hit a player directly
                PlayerHealth targetHealth = hit.collider.GetComponent<PlayerHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage((int)finalDamage, gameObject);
                    onHitEnemy?.Invoke(hit.collider.gameObject, finalDamage);
                    onDealDamage?.Invoke(finalDamage);
                }
                
                // Fallback: Check if we hit a bot directly
                Bot targetBot = hit.collider.GetComponent<Bot>();
                if (targetBot != null)
                {
                    targetBot.TakeDamage(finalDamage, gameObject);
                    onHitEnemy?.Invoke(hit.collider.gameObject, finalDamage);
                    onDealDamage?.Invoke(finalDamage);
                }
                
                // Reset next shot multiplier
                nextShotDamageMultiplier = 1.0f;
            }
        }
        else
        {
            // Missed - create tracer to max range
            Vector3 missPoint = shootStart + shootDirection * weaponData.range;
            BulletEffect.CreateTracer(shootStart, missPoint, Color.yellow, 0.05f, 0.1f);
        }
        
        // Visual and audio effects
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(true);
            Invoke(nameof(HideMuzzleFlash), 0.05f);
        }
        
        if (audioSource != null && weaponData.shootSound != null)
        {
            audioSource.PlayOneShot(weaponData.shootSound);
        }
    }
    
    private void HideMuzzleFlash()
    {
        if (muzzleFlash != null)
            muzzleFlash.SetActive(false);
    }
    
    private void StartReload()
    {
        if (isReloading) return;
        
        isReloading = true;
        
        if (audioSource != null && weaponData.reloadSound != null)
        {
            audioSource.PlayOneShot(weaponData.reloadSound);
        }
        
        Invoke(nameof(FinishReload), weaponData.reloadTime);
    }
    
    private void FinishReload()
    {
        int ammoNeeded = weaponData.magazineSize - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);
        
        currentAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;
        
        isReloading = false;
        OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
    }
    
    private void CreateHitEffect(Vector3 position, Vector3 normal)
    {
        // You can instantiate bullet hole decals or particle effects here
        Debug.DrawRay(position, normal * 0.5f, Color.red, 2f);
    }
    
    public void AddAmmo(int amount)
    {
        reserveAmmo = Mathf.Min(reserveAmmo + amount, weaponData.reserveAmmo);
        OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
    }
    
    // Helper methods for abilities
    private float lastShotTime = 0f;
    
    public bool HasShotRecently(float timeWindow)
    {
        return (Time.time - lastShotTime) < timeWindow;
    }
    
    void TrackShotTime()
    {
        lastShotTime = Time.time;
    }
}

