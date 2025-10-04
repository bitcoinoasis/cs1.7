using UnityEngine;

/// <summary>
/// Human Ultimate: Devotion Aura
/// Passive - reduce all incoming damage
/// </summary>
public class DevotionAuraAbility : Ability
{
    private PlayerHealth playerHealth;
    
    protected override void OnInitialize()
    {
        playerHealth = GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.onBeforeTakeDamage += ApplyDamageReduction;
            Debug.Log($"[Devotion Aura] Initialized. Damage reduction: {GetValue()}%");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    void ApplyDamageReduction(ref float damage)
    {
        float damageReduction = GetValue() / 100f; // 5%, 8%, 10%, 12%, 15%
        
        float originalDamage = damage;
        damage *= (1f - damageReduction);
        
        Debug.Log($"[Devotion Aura] Reduced damage: {originalDamage:F1} → {damage:F1} ({damageReduction * 100f}% reduction)");
    }
    
    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.onBeforeTakeDamage -= ApplyDamageReduction;
        }
    }
}
