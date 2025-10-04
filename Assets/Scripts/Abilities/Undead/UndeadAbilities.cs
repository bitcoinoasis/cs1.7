using UnityEngine;

/// <summary>
/// Undead Ultimate: Vampiric Aura
/// Passive - heal for a percentage of damage dealt
/// </summary>
public class VampiricAuraAbility : Ability
{
    private Weapon weapon;
    private PlayerHealth playerHealth;
    
    protected override void OnInitialize()
    {
        weapon = GetComponentInChildren<Weapon>();
        playerHealth = GetComponent<PlayerHealth>();
        
        if (weapon != null)
        {
            weapon.onDealDamage += OnDealDamage;
            Debug.Log($"[Vampiric Aura] Initialized. Life steal: {GetValue()}%");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    void OnDealDamage(float damageDealt)
    {
        if (playerHealth == null) return;
        
        float lifeStealPercent = GetValue() / 100f; // 10%, 15%, 20%, 25%, 30%
        float healAmount = damageDealt * lifeStealPercent;
        
        // Cap healing at 5 HP per shot
        healAmount = Mathf.Min(healAmount, 5f);
        
        playerHealth.Heal((int)healAmount);
        
        // Visual effect
        PlayVisualEffect();
        
        Debug.Log($"[Vampiric Aura] Healed {healAmount:F1} HP from {damageDealt:F1} damage");
    }
    
    void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.onDealDamage -= OnDealDamage;
        }
    }
}

/// <summary>
/// Undead Ability 2: Levitation
/// Passive - reduced gravity, higher jumps, no fall damage
/// </summary>
public class LevitationAbility : Ability
{
    private PlayerController playerController;
    private float originalGravity;
    private float originalJumpForce;
    
    protected override void OnInitialize()
    {
        playerController = GetComponent<PlayerController>();
        
        if (playerController != null)
        {
            // Store original values
            originalGravity = playerController.gravity;
            originalJumpForce = playerController.jumpForce;
            
            float gravityMultiplier = GetValue(); // 0.9x, 0.85x, 0.8x, 0.75x, 0.7x
            float jumpMultiplier = GetSecondaryValue(); // 1.2x, 1.3x, 1.4x, 1.5x, 1.6x
            
            // Apply multipliers
            playerController.gravity = originalGravity * gravityMultiplier;
            playerController.jumpForce = originalJumpForce * jumpMultiplier;
            
            Debug.Log($"[Levitation] Gravity: {gravityMultiplier}x, Jump: {jumpMultiplier}x");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    void OnDestroy()
    {
        // Restore original values
        if (playerController != null)
        {
            playerController.gravity = originalGravity;
            playerController.jumpForce = originalJumpForce;
        }
    }
}

/// <summary>
/// Undead Ability 3: Unholy Aura
/// Passive - enemies near you take damage over time
/// </summary>
public class UnholyAuraAbility : Ability
{
    private float tickTimer = 0f;
    private const float TICK_INTERVAL = 0.5f;
    
    protected override void OnInitialize()
    {
        Debug.Log($"[Unholy Aura] Initialized. Damage: {GetValue()}/sec, Radius: {GetSecondaryValue()}m");
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    protected override void Update()
    {
        base.Update();
        
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_INTERVAL)
        {
            tickTimer = 0f;
            DamageNearbyEnemies();
        }
    }
    
    void DamageNearbyEnemies()
    {
        float damagePerSecond = GetValue(); // 2, 3, 4, 5, 6
        float radius = GetSecondaryValue(); // 3m, 3.5m, 4m, 4.5m, 5m
        
        float damagePerTick = damagePerSecond * TICK_INTERVAL;
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in hitColliders)
        {
            if (col.gameObject == gameObject) continue; // Skip self
            
            PlayerHealth enemy = col.GetComponent<PlayerHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)damagePerTick);
                Debug.Log($"[Unholy Aura] Damaged {col.gameObject.name} for {damagePerTick:F1}");
            }
        }
    }
}
