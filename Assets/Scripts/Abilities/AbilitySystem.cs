using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Main ability system - manages race abilities, cooldowns, and mana
/// Attach to Player object
/// </summary>
public class AbilitySystem : MonoBehaviour
{
    [Header("Race Configuration")]
    public RaceData currentRace;
    public int currentLevel = 1;
    
    [Header("Ability References")]
    private Ability ultimateAbility;
    private Ability ability2;
    private Ability ability3;
    
    [Header("Mana System (Blood Elf)")]
    public float maxMana = 0f;
    public float currentMana = 0f;
    public float manaRegenRate = 2f;
    private float manaRegenCooldown = 0f;
    
    [Header("Runtime Info")]
    public bool abilitiesInitialized = false;
    
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private Weapon weapon;
    
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        weapon = GetComponentInChildren<Weapon>();
        
        if (currentRace != null)
        {
            ApplyRaceStats();
            InitializeAbilities();
            abilitiesInitialized = true;
            Debug.Log($"[AbilitySystem] Initialized race: {currentRace.raceName} at level {currentLevel}");
        }
        else
        {
            Debug.LogWarning("[AbilitySystem] No race assigned! Please assign a RaceData in the Inspector.");
        }
    }
    
    void Update()
    {
        if (!abilitiesInitialized) return;
        
        // Handle ability inputs
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ultimateAbility?.TryActivate();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ability2?.TryActivate();
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            ability3?.TryActivate();
        }
        
        // Mana regeneration (for Blood Elf)
        if (maxMana > 0)
        {
            if (manaRegenCooldown > 0)
            {
                manaRegenCooldown -= Time.deltaTime;
            }
            else if (currentMana < maxMana)
            {
                currentMana += manaRegenRate * Time.deltaTime;
                currentMana = Mathf.Min(currentMana, maxMana);
            }
        }
    }
    
    void ApplyRaceStats()
    {
        // Apply health multiplier
        if (playerHealth != null)
        {
            playerHealth.maxHealth = Mathf.RoundToInt(100 * currentRace.healthMultiplier);
            playerHealth.currentHealth = playerHealth.maxHealth;
            Debug.Log($"[AbilitySystem] Applied health: {playerHealth.maxHealth} HP");
        }
        
        // Apply speed multiplier
        if (playerController != null)
        {
            playerController.walkSpeed *= currentRace.speedMultiplier;
            playerController.sprintSpeed *= currentRace.speedMultiplier;
            Debug.Log($"[AbilitySystem] Applied speed multiplier: {currentRace.speedMultiplier}x");
        }
        
        // Damage multiplier will be applied in Weapon.cs
        if (weapon != null)
        {
            weapon.damageMultiplier = currentRace.damageMultiplier;
            Debug.Log($"[AbilitySystem] Applied damage multiplier: {currentRace.damageMultiplier}x");
        }
        
        // Set up mana for Blood Elf
        if (currentRace.raceName == "Blood Elf")
        {
            int level = Mathf.Clamp(currentLevel, 1, 5);
            maxMana = 20 + (level - 1) * 10; // 20, 30, 40, 50, 60
            currentMana = maxMana;
            Debug.Log($"[AbilitySystem] Blood Elf mana initialized: {currentMana}/{maxMana}");
        }
    }
    
    void InitializeAbilities()
    {
        // Create ability instances based on race
        if (currentRace.ultimateAbility != null)
        {
            ultimateAbility = CreateAbilityInstance(currentRace.ultimateAbility, "Ultimate");
        }
        
        if (currentRace.ability2 != null)
        {
            ability2 = CreateAbilityInstance(currentRace.ability2, "Ability2");
        }
        
        if (currentRace.ability3 != null)
        {
            ability3 = CreateAbilityInstance(currentRace.ability3, "Ability3");
        }
    }
    
    Ability CreateAbilityInstance(AbilityData data, string slot)
    {
        if (data == null) return null;
        
        string abilityName = data.abilityName;
        Ability newAbility = null;
        
        // Create appropriate ability type based on name
        switch (abilityName)
        {
            // Orc abilities
            case "Critical Strike":
                newAbility = gameObject.AddComponent<CriticalStrikeAbility>();
                break;
            case "Bash":
                newAbility = gameObject.AddComponent<BashAbility>();
                break;
            case "Reincarnation":
                newAbility = gameObject.AddComponent<ReincarnationAbility>();
                break;
                
            // Undead abilities
            case "Vampiric Aura":
                newAbility = gameObject.AddComponent<VampiricAuraAbility>();
                break;
            case "Levitation":
                newAbility = gameObject.AddComponent<LevitationAbility>();
                break;
            case "Unholy Aura":
                newAbility = gameObject.AddComponent<UnholyAuraAbility>();
                break;
                
            // Human abilities
            case "Devotion Aura":
                newAbility = gameObject.AddComponent<DevotionAuraAbility>();
                break;
            case "Invisibility":
                newAbility = gameObject.AddComponent<InvisibilityAbility>();
                break;
            case "Teleport":
                newAbility = gameObject.AddComponent<TeleportAbility>();
                break;
                
            // Night Elf abilities
            case "Evasion":
                newAbility = gameObject.AddComponent<EvasionAbility>();
                break;
            case "Blink":
                newAbility = gameObject.AddComponent<BlinkAbility>();
                break;
            case "Thorns Aura":
                newAbility = gameObject.AddComponent<ThornsAuraAbility>();
                break;
                
            default:
                Debug.LogWarning($"[AbilitySystem] No ability class found for: {abilityName}");
                break;
        }
        
        if (newAbility != null)
        {
            newAbility.Initialize(data, this, currentLevel);
            Debug.Log($"[AbilitySystem] Created ability: {abilityName} in slot {slot}");
        }
        
        return newAbility;
    }
    
    public bool SpendMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            manaRegenCooldown = 3f; // Pause regen for 3 seconds
            return true;
        }
        return false;
    }
    
    public void OnTakeDamage(float damage)
    {
        // Pause mana regen when taking damage
        if (maxMana > 0)
        {
            manaRegenCooldown = 3f;
        }
    }
    
    // Public getters for UI
    public Ability GetUltimateAbility() => ultimateAbility;
    public Ability GetAbility2() => ability2;
    public Ability GetAbility3() => ability3;
    
    // Debug method to reset all cooldowns
    public void ResetAllCooldowns()
    {
        if (ultimateAbility != null)
            ultimateAbility.currentCooldown = 0f;
        if (ability2 != null)
            ability2.currentCooldown = 0f;
        if (ability3 != null)
            ability3.currentCooldown = 0f;
            
        Debug.Log("[AbilitySystem] All cooldowns reset!");
    }
}
