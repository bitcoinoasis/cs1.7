using UnityEngine;

/// <summary>
/// Base class for all abilities
/// Handles cooldowns, activation, and common functionality
/// </summary>
public abstract class Ability : MonoBehaviour
{
    protected AbilityData data;
    protected AbilitySystem abilitySystem;
    protected int level; // 0-4 (representing levels 1-5)
    protected float currentCooldown = 0f;
    
    // Public properties
    public bool IsReady => currentCooldown <= 0f;
    public float CooldownRemaining => currentCooldown;
    public string AbilityName => data != null ? data.abilityName : "Unknown";
    public AbilityType AbilityType => data != null ? data.abilityType : AbilityType.Passive;
    
    /// <summary>
    /// Initialize the ability with data
    /// </summary>
    public virtual void Initialize(AbilityData abilityData, AbilitySystem system, int abilityLevel)
    {
        data = abilityData;
        abilitySystem = system;
        level = Mathf.Clamp(abilityLevel, 1, 5) - 1; // Convert to 0-4 index
        
        Debug.Log($"[Ability] {data.abilityName} initialized at level {abilityLevel}");
        
        // Call OnInitialize for subclasses
        OnInitialize();
    }
    
    /// <summary>
    /// Override this for custom initialization in subclasses
    /// </summary>
    protected virtual void OnInitialize() { }
    
    protected virtual void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
    
    /// <summary>
    /// Try to activate the ability
    /// </summary>
    public virtual bool TryActivate()
    {
        // Passive abilities don't activate manually
        if (data.abilityType == AbilityType.Passive)
        {
            return false;
        }
        
        // Check cooldown
        if (!IsReady)
        {
            Debug.Log($"[Ability] {data.abilityName} is on cooldown: {currentCooldown:F1}s remaining");
            return false;
        }
        
        // Check mana cost
        float manaCost = GetManaCost();
        if (manaCost > 0 && !abilitySystem.SpendMana(manaCost))
        {
            Debug.Log($"[Ability] Not enough mana for {data.abilityName}");
            return false;
        }
        
        // Activate the ability
        Activate();
        
        // Start cooldown
        float cooldown = GetCooldown();
        if (cooldown > 0)
        {
            currentCooldown = cooldown;
        }
        
        return true;
    }
    
    /// <summary>
    /// Override this to implement ability logic
    /// </summary>
    protected abstract void Activate();
    
    /// <summary>
    /// Get the primary value for current level
    /// </summary>
    protected float GetValue()
    {
        if (data == null || level < 0 || level >= data.valuesPerLevel.Length)
            return 0f;
        return data.valuesPerLevel[level];
    }
    
    /// <summary>
    /// Get the secondary value for current level
    /// </summary>
    protected float GetSecondaryValue()
    {
        if (data == null || level < 0 || level >= data.secondaryValuesPerLevel.Length)
            return 0f;
        return data.secondaryValuesPerLevel[level];
    }
    
    /// <summary>
    /// Get the cooldown for current level
    /// </summary>
    protected float GetCooldown()
    {
        if (data == null || level < 0 || level >= data.cooldownsPerLevel.Length)
            return 0f;
        return data.cooldownsPerLevel[level];
    }
    
    /// <summary>
    /// Get the mana cost for current level
    /// </summary>
    protected float GetManaCost()
    {
        if (data == null || level < 0 || level >= data.manaCostPerLevel.Length)
            return 0f;
        return data.manaCostPerLevel[level];
    }
    
    /// <summary>
    /// Play visual effect
    /// </summary>
    protected void PlayVisualEffect(Vector3 position)
    {
        if (data.visualEffectPrefab != null)
        {
            GameObject effect = Instantiate(data.visualEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        if (data.soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(data.soundEffect, position);
        }
    }
    
    /// <summary>
    /// Play visual effect at ability owner's position
    /// </summary>
    protected void PlayVisualEffect()
    {
        PlayVisualEffect(transform.position);
    }
}
