using UnityEngine;

/// <summary>
/// Orc Ultimate: Critical Strike
/// Passive ability - chance to deal massive bonus damage
/// </summary>
public class CriticalStrikeAbility : Ability
{
    private Weapon weapon;
    
    protected override void OnInitialize()
    {
        weapon = GetComponentInChildren<Weapon>();
        
        if (weapon != null)
        {
            // Hook into weapon's shoot event
            weapon.onBeforeShoot += CheckCriticalStrike;
            Debug.Log($"[CriticalStrike] Hooked into weapon. Chance: {GetValue()}%, Multiplier: {GetSecondaryValue()}x");
        }
        else
        {
            Debug.LogWarning("[CriticalStrike] No weapon found!");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability, no manual activation
    }
    
    void CheckCriticalStrike()
    {
        if (weapon == null) return;
        
        float critChance = GetValue(); // 8%, 12%, 15%, 18%, 20%
        float critMultiplier = GetSecondaryValue(); // 1.5x, 1.75x, 2.0x, 2.25x, 2.5x
        
        // Roll for critical strike
        if (Random.Range(0f, 100f) < critChance)
        {
            // Apply critical strike multiplier
            weapon.nextShotDamageMultiplier *= critMultiplier;
            
            // Visual effect
            PlayVisualEffect();
            
            Debug.Log($"<color=orange>CRITICAL STRIKE! {critMultiplier}x damage</color>");
        }
    }
    
    void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.onBeforeShoot -= CheckCriticalStrike;
        }
    }
}
