using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [Header("Health & Armor")]
    public Text healthText;
    public Image healthBar;
    public Text armorText;
    
    [Header("Ammo")]
    public Text ammoText;
    
    [Header("Race Info")]
    public Text raceText;
    public Text levelText;
    
    [Header("XP Bar")]
    public Image xpBar;
    public Text xpText;
    
    [Header("Abilities")]
    public Transform abilityPanel;
    public GameObject abilitySlotPrefab;
    
    [Header("Crosshair")]
    public Image crosshair;
    
    private PlayerHealth playerHealth;
    private PlayerExperience playerExperience;
    private PlayerRace playerRace;
    private AbilitySystem abilitySystem;
    private Weapon currentWeapon;
    
    private void Start()
    {
        // Find player components
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerExperience = player.GetComponent<PlayerExperience>();
            playerRace = player.GetComponent<PlayerRace>();
            abilitySystem = player.GetComponent<AbilitySystem>();
            
            // Subscribe to events
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged.AddListener(UpdateHealth);
                playerHealth.OnArmorChanged.AddListener(UpdateArmor);
            }
            
            if (playerExperience != null)
            {
                playerExperience.OnXPChanged.AddListener(UpdateXP);
                playerExperience.OnLevelUp.AddListener(OnLevelUp);
            }
            
            if (playerRace != null)
            {
                playerRace.OnRaceChanged.AddListener(UpdateRace);
                UpdateRace(playerRace.GetCurrentRace());
            }
        }
    }
    
    private void Update()
    {
        // Update weapon ammo if weapon changed
        WeaponManager weaponManager = FindFirstObjectByType<WeaponManager>();
        if (weaponManager != null)
        {
            Weapon weapon = weaponManager.GetCurrentWeapon();
            if (weapon != currentWeapon)
            {
                if (currentWeapon != null)
                {
                    currentWeapon.OnAmmoChanged.RemoveListener(UpdateAmmo);
                }
                
                currentWeapon = weapon;
                
                if (currentWeapon != null)
                {
                    currentWeapon.OnAmmoChanged.AddListener(UpdateAmmo);
                }
            }
        }
    }
    
    private void UpdateHealth(int health)
    {
        if (healthText != null)
            healthText.text = health.ToString();
        
        if (healthBar != null && playerHealth != null)
            healthBar.fillAmount = (float)health / playerHealth.maxHealth;
    }
    
    private void UpdateArmor(int armor)
    {
        if (armorText != null)
            armorText.text = armor.ToString();
    }
    
    private void UpdateAmmo(int currentAmmo, int reserveAmmo)
    {
        if (ammoText != null)
            ammoText.text = $"{currentAmmo} / {reserveAmmo}";
    }
    
    private void UpdateXP(int currentXP, int xpForNext, int level)
    {
        if (xpBar != null && playerExperience != null)
        {
            xpBar.fillAmount = playerExperience.GetLevelProgress();
        }
        
        if (xpText != null)
        {
            xpText.text = $"{currentXP} / {xpForNext}";
        }
        
        if (levelText != null)
        {
            levelText.text = $"Level {level}";
        }
    }
    
    private void OnLevelUp(int newLevel)
    {
        // Show level up notification
        Debug.Log($"LEVEL UP! Now level {newLevel}");
    }
    
    private void UpdateRace(RaceData race)
    {
        if (raceText != null && race != null)
        {
            raceText.text = race.raceName;
        }
    }
}
