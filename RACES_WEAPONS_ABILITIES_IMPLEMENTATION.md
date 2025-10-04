# Race, Weapon & Ability Implementation Guide

**Step-by-step guide to set up all 8 races, 20 weapons, and abilities in Unity**

This guide shows you how to implement everything from [RACES_WEAPONS_ABILITIES_COMPLETE.md](RACES_WEAPONS_ABILITIES_COMPLETE.md) in your Unity project.

---

## 📋 Prerequisites

Before starting, ensure you have:
- ✅ Unity project at `/Users/syedshah/cs1.7`
- ✅ Completed [QUICK_SETUP.md](QUICK_SETUP.md)
- ✅ Working player and weapon systems
- ✅ Read [RACES_WEAPONS_ABILITIES_COMPLETE.md](RACES_WEAPONS_ABILITIES_COMPLETE.md)

**Estimated Time:** 4-6 hours for complete implementation

---

## 🎯 Implementation Overview

### Phase 1: Data Setup (30 min)
- Create ScriptableObjects for all races
- Create ScriptableObjects for all weapons
- Configure stats and values

### Phase 2: Core Scripts (1 hour)
- Enhance RaceData.cs with abilities
- Create ability system scripts
- Add cooldown/mana management

### Phase 3: Race Abilities (2-3 hours)
- Implement 8 races × 3 abilities each
- Add visual effects
- Test each ability

### Phase 4: Weapons (1 hour)
- Configure all 20 weapons
- Set up weapon stats
- Test weapon balance

### Phase 5: UI & Polish (30 min)
- Ability cooldown UI
- Mana bar (Blood Elf)
- Visual effect polish

---

## 📁 PHASE 1: Data Setup

### Step 1.1: Enhance RaceData ScriptableObject

Open `Assets/Scripts/Data/RaceData.cs` and update it:

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "Warcraft/Race Data")]
public class RaceData : ScriptableObject
{
    [Header("Basic Info")]
    public string raceName;
    public string description;
    public RaceTier tier; // Starter, Intermediate, Advanced, Expert
    
    [Header("Base Stats")]
    public float healthMultiplier = 1.0f;
    public float speedMultiplier = 1.0f;
    public float damageMultiplier = 1.0f;
    public int maxLevel = 10;
    
    [Header("Abilities")]
    public AbilityData ultimateAbility;
    public AbilityData ability2;
    public AbilityData ability3;
    
    [Header("Visual")]
    public Color raceColor = Color.white;
    public Sprite raceIcon;
    
    [Header("Balance")]
    [TextArea(3, 5)]
    public string playstyle;
    [TextArea(3, 5)]
    public string bestFor;
    [TextArea(3, 5)]
    public string counters;
}

public enum RaceTier
{
    Starter,      // Easy to learn
    Intermediate, // Requires strategy
    Advanced,     // High skill ceiling
    Expert        // Complex mechanics
}
```

### Step 1.2: Create AbilityData ScriptableObject

Create new file: `Assets/Scripts/Data/AbilityData.cs`

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Warcraft/Ability Data")]
public class AbilityData : ScriptableObject
{
    [Header("Basic Info")]
    public string abilityName;
    [TextArea(2, 4)]
    public string description;
    public AbilityType abilityType;
    public Sprite icon;
    
    [Header("Stats per Level (5 levels)")]
    public float[] valuesPerLevel = new float[5]; // Main stat (damage, heal, etc.)
    public float[] secondaryValuesPerLevel = new float[5]; // Secondary stat (duration, range, etc.)
    public float[] cooldownsPerLevel = new float[5]; // Cooldown in seconds
    
    [Header("Costs")]
    public float[] manaCostPerLevel = new float[5]; // For Blood Elf
    
    [Header("Visual Effects")]
    public GameObject visualEffectPrefab;
    public Color effectColor = Color.white;
    public AudioClip soundEffect;
    
    [Header("Balance Notes")]
    [TextArea(3, 5)]
    public string balanceNotes;
}

public enum AbilityType
{
    Passive,           // Always active
    ActiveTargeted,    // Aim at target
    ActiveSelf,        // Self-cast
    ActiveAoE,         // Area of effect
    PassiveTrigger     // Passive but triggers on condition
}
```

### Step 1.3: Update WeaponData ScriptableObject

Open `Assets/Scripts/Data/WeaponData.cs` and enhance it:

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Warcraft/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic Info")]
    public string weaponName;
    public WeaponType weaponType;
    public Sprite weaponIcon;
    
    [Header("Damage Stats")]
    public float damage = 30f;
    public float fireRate = 0.1f; // Seconds between shots
    public float headshotMultiplier = 4f;
    public float legMultiplier = 0.75f;
    public float armorPenetration = 0.5f; // 0-1 (percentage)
    
    [Header("Ammo")]
    public int magazineSize = 30;
    public int reserveAmmo = 90;
    public float reloadTime = 2.5f;
    
    [Header("Accuracy")]
    public float baseAccuracy = 1.0f; // Lower = more accurate
    public float recoilAmount = 2f;
    public float range = 1000f;
    
    [Header("Movement")]
    public float movementSpeedMultiplier = 0.9f; // 1.0 = 250 units/sec
    
    [Header("Economy")]
    public int price = 2500;
    public int killReward = 300;
    public int bonusReward = 0; // Extra for shotgun kills, etc.
    
    [Header("Special Features")]
    public bool isSilenced = false;
    public bool hasScope = false;
    public float scopeZoom = 2f; // 2x or 4x
    public bool isBurstMode = false;
    public int burstCount = 3;
    
    [Header("Visual/Audio")]
    public GameObject weaponPrefab;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public GameObject muzzleFlashPrefab;
}

public enum WeaponType
{
    Pistol,
    SMG,
    Rifle,
    Sniper,
    Shotgun,
    Melee
}
```

---

## 🔨 PHASE 2: Create All Data Assets

### Step 2.1: Create Race Assets

In Unity Editor:

1. **Right-click in Project** → `Create` → `Warcraft` → `Race Data`
2. Create 8 race assets:

```
Assets/Data/Races/
├── Race_Orc.asset
├── Race_Undead.asset
├── Race_Human.asset
├── Race_NightElf.asset
├── Race_BloodElf.asset
├── Race_Troll.asset
├── Race_Dwarf.asset
└── Race_Celestial.asset
```

### Step 2.2: Configure Orc (Example)

Select `Race_Orc.asset` in Inspector:

```yaml
Basic Info:
  Race Name: "Orc"
  Description: "Aggressive tank, trades speed for durability"
  Tier: Starter
  
Base Stats:
  Health Multiplier: 1.30
  Speed Multiplier: 0.85
  Damage Multiplier: 1.15
  Max Level: 10
  
Abilities:
  Ultimate Ability: [Drag Ability_CriticalStrike here]
  Ability 2: [Drag Ability_Bash here]
  Ability 3: [Drag Ability_Reincarnation here]
  
Visual:
  Race Color: #FF6B35 (Orange/Red)
  Race Icon: [Drag orc icon sprite]
  
Balance:
  Playstyle: "Aggressive tank, trades speed for durability"
  Best For: "New players, aggressive playstyle, holding positions"
  Counters: "Fast races, snipers, long-range combat"
```

### Step 2.3: Create Ability Assets

Create abilities for Orc:

**Ability_CriticalStrike.asset:**
```yaml
Basic Info:
  Ability Name: "Critical Strike"
  Description: "Your attacks have a chance to deal massive bonus damage"
  Ability Type: Passive
  
Stats per Level:
  Values Per Level: [8, 12, 15, 18, 20] (chance %)
  Secondary Values Per Level: [1.5, 1.75, 2.0, 2.25, 2.5] (damage multiplier)
  Cooldowns Per Level: [0, 0, 0, 0, 0] (passive = no cooldown)
  
Visual Effects:
  Effect Color: Orange (#FF6B35)
  
Balance Notes:
  "Unreliable but rewarding. Works on headshots (stacks multiplicatively).
   Visual: Orange damage numbers. Sound cue for enemy awareness."
```

**Ability_Bash.asset:**
```yaml
Basic Info:
  Ability Name: "Bash"
  Description: "Chance to stun enemies for a brief moment"
  Ability Type: Passive
  
Stats per Level:
  Values Per Level: [5, 8, 10, 12, 15] (chance %)
  Secondary Values Per Level: [0.3, 0.4, 0.5, 0.6, 0.75] (stun duration)
  Cooldowns Per Level: [0, 0, 0, 0, 0]
  
Visual Effects:
  Effect Color: Yellow
  
Balance Notes:
  "Stun freezes movement but not shooting. Screen shake for victim.
   Cannot stun same target twice in 3 seconds."
```

**Ability_Reincarnation.asset:**
```yaml
Basic Info:
  Ability Name: "Reincarnation"
  Description: "Respawn at death location with partial HP (once per round)"
  Ability Type: PassiveTrigger
  
Stats per Level:
  Values Per Level: [25, 35, 45, 55, 65] (HP % on respawn)
  Secondary Values Per Level: [0, 0, 0, 0, 0]
  Cooldowns Per Level: [60, 50, 40, 30, 20] (once per round)
  
Visual Effects:
  Effect Color: Green
  
Balance Notes:
  "Only works once per round. 2-second revival delay (vulnerable).
   Cannot be used if killed by headshot. Weapons kept but ammo at 50%.
   Visual: Green glow during revival."
```

### Step 2.4: Repeat for All 8 Races

Follow the same pattern using stats from [RACES_WEAPONS_ABILITIES_COMPLETE.md](RACES_WEAPONS_ABILITIES_COMPLETE.md):

- **Orc:** Critical Strike, Bash, Reincarnation
- **Undead:** Vampiric Aura, Levitation, Unholy Aura
- **Human:** Devotion Aura, Invisibility, Teleport
- **Night Elf:** Evasion, Blink, Thorns Aura
- **Blood Elf:** Mana Shield, Arcane Blast, Mana Burn
- **Troll:** Berserker Rage, Throwing Axes, Regeneration
- **Dwarf:** Iron Skin, Entrenchment, Ground Slam
- **Celestial:** Divine Shield, Healing Wave, Aura of Light

**Total: 24 ability assets to create**

---

## 🔫 PHASE 3: Create Weapon Assets

### Step 3.1: Create Weapon Assets

In Unity Editor:

```
Assets/Data/Weapons/
├── Pistols/
│   ├── Weapon_USP.asset
│   ├── Weapon_Glock.asset
│   ├── Weapon_Deagle.asset
│   └── Weapon_P250.asset
├── SMGs/
│   ├── Weapon_MP5.asset
│   └── Weapon_P90.asset
├── Rifles/
│   ├── Weapon_AK47.asset
│   ├── Weapon_M4A4.asset
│   ├── Weapon_M4A1S.asset
│   ├── Weapon_AUG.asset
│   ├── Weapon_SG553.asset
│   ├── Weapon_Galil.asset
│   └── Weapon_FAMAS.asset
├── Snipers/
│   ├── Weapon_AWP.asset
│   └── Weapon_Scout.asset
└── Shotguns/
    ├── Weapon_Nova.asset
    └── Weapon_XM1014.asset
```

### Step 3.2: Configure AK-47 (Example)

Select `Weapon_AK47.asset`:

```yaml
Basic Info:
  Weapon Name: "AK-47"
  Weapon Type: Rifle
  
Damage Stats:
  Damage: 36
  Fire Rate: 0.1
  Headshot Multiplier: 4
  Leg Multiplier: 0.75
  Armor Penetration: 0.775
  
Ammo:
  Magazine Size: 30
  Reserve Ammo: 90
  Reload Time: 2.5
  
Accuracy:
  Base Accuracy: 1.0
  Recoil Amount: 3.5
  Range: 8192
  
Movement:
  Movement Speed Multiplier: 0.86
  
Economy:
  Price: 2500
  Kill Reward: 300
  Bonus Reward: 0
  
Special Features:
  Is Silenced: false
  Has Scope: false
  Is Burst Mode: false
```

### Step 3.3: Quick Reference for All Weapons

Copy these values from [RACES_WEAPONS_ABILITIES_COMPLETE.md](RACES_WEAPONS_ABILITIES_COMPLETE.md):

**Pistols:**
- USP-S: 35 dmg, 0.15s rate, 12 mag, 50% armor pen, $0
- Glock: 28 dmg, 0.12s rate, 20 mag, 47% armor pen, $0
- Deagle: 54 dmg, 0.25s rate, 7 mag, 93% armor pen, $650
- P250: 38 dmg, 0.15s rate, 13 mag, 64% armor pen, $300

**SMGs:**
- MP5: 27 dmg, 0.08s rate, 30 mag, 57% armor pen, $1500
- P90: 26 dmg, 0.07s rate, 50 mag, 65% armor pen, $2350

**Rifles:**
- AK-47: 36 dmg, 0.1s rate, 30 mag, 77.5% armor pen, $2500
- M4A4: 33 dmg, 0.09s rate, 30 mag, 70% armor pen, $3100
- M4A1-S: 33 dmg, 0.09s rate, 20 mag, 70% armor pen, $2900 (silenced)
- AUG: 28 dmg, 0.09s rate, 30 mag, 90% armor pen, $3300 (scope)
- SG 553: 30 dmg, 0.09s rate, 30 mag, 100% armor pen, $3000 (scope)
- Galil: 30 dmg, 0.09s rate, 35 mag, 77% armor pen, $1800
- FAMAS: 30 dmg, 0.09s rate, 25 mag, 70% armor pen, $2050

**Snipers:**
- AWP: 115 dmg, 1.5s rate, 10 mag, 97.5% armor pen, $4750
- Scout: 88 dmg, 1.25s rate, 10 mag, 85% armor pen, $1700

**Shotguns:**
- Nova: 26×9 dmg, 0.9s rate, 8 mag, 50% armor pen, $1050
- XM1014: 20×6 dmg, 0.35s rate, 7 mag, 80% armor pen, $2000

---

## 💻 PHASE 4: Core Ability System Scripts

### Step 4.1: Create AbilitySystem.cs

Create: `Assets/Scripts/Abilities/AbilitySystem.cs`

```csharp
using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour
{
    [Header("Race Data")]
    public RaceData currentRace;
    public int currentLevel = 1;
    
    [Header("Ability References")]
    private Ability ultimateAbility;
    private Ability ability2;
    private Ability ability3;
    
    [Header("Mana (Blood Elf)")]
    public float maxMana = 0f;
    public float currentMana = 0f;
    public float manaRegenRate = 2f;
    private float manaRegenCooldown = 0f;
    
    private PlayerInfo playerInfo;
    private Weapon weapon;
    
    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        weapon = GetComponentInChildren<Weapon>();
        
        if (currentRace != null)
        {
            ApplyRaceStats();
            InitializeAbilities();
        }
    }
    
    void Update()
    {
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
        if (playerInfo != null)
        {
            playerInfo.maxHealth = Mathf.RoundToInt(100 * currentRace.healthMultiplier);
            playerInfo.health = playerInfo.maxHealth;
        }
        
        // Apply speed multiplier
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.moveSpeed *= currentRace.speedMultiplier;
        }
        
        // Damage multiplier will be applied in Weapon.cs
        if (weapon != null)
        {
            weapon.damageMultiplier = currentRace.damageMultiplier;
        }
        
        // Set up mana for Blood Elf
        if (currentRace.raceName == "Blood Elf")
        {
            int level = Mathf.Clamp(currentLevel, 1, 5);
            maxMana = 20 + (level - 1) * 10; // 20, 30, 40, 50, 60
            currentMana = maxMana;
        }
    }
    
    void InitializeAbilities()
    {
        // Create ability instances
        if (currentRace.ultimateAbility != null)
        {
            ultimateAbility = CreateAbilityInstance(currentRace.ultimateAbility, KeyCode.Q);
        }
        
        if (currentRace.ability2 != null)
        {
            ability2 = CreateAbilityInstance(currentRace.ability2, KeyCode.E);
        }
        
        if (currentRace.ability3 != null)
        {
            ability3 = CreateAbilityInstance(currentRace.ability3, KeyCode.F);
        }
    }
    
    Ability CreateAbilityInstance(AbilityData data, KeyCode key)
    {
        // Create appropriate ability type based on race
        string abilityName = data.abilityName;
        Ability newAbility = null;
        
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
                
            // Add more cases for other races...
            
            default:
                Debug.LogWarning($"No ability class found for: {abilityName}");
                break;
        }
        
        if (newAbility != null)
        {
            newAbility.Initialize(data, this, currentLevel);
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
    
    public void TakeDamage(float damage)
    {
        // Pause mana regen when taking damage
        if (maxMana > 0)
        {
            manaRegenCooldown = 3f;
        }
    }
}
```

### Step 4.2: Create Base Ability Class

Create: `Assets/Scripts/Abilities/Ability.cs`

```csharp
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected AbilityData data;
    protected AbilitySystem abilitySystem;
    protected int level;
    protected float currentCooldown = 0f;
    
    public bool IsReady => currentCooldown <= 0f;
    public float CooldownRemaining => currentCooldown;
    
    public virtual void Initialize(AbilityData abilityData, AbilitySystem system, int abilityLevel)
    {
        data = abilityData;
        abilitySystem = system;
        level = Mathf.Clamp(abilityLevel, 1, 5) - 1; // Convert to 0-4 index
    }
    
    protected virtual void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
    
    public virtual bool TryActivate()
    {
        if (data.abilityType == AbilityType.Passive)
        {
            // Passives don't need activation
            return false;
        }
        
        if (!IsReady)
        {
            Debug.Log($"{data.abilityName} is on cooldown: {currentCooldown:F1}s");
            return false;
        }
        
        // Check mana cost
        float manaCost = data.manaCostPerLevel[level];
        if (manaCost > 0 && !abilitySystem.SpendMana(manaCost))
        {
            Debug.Log($"Not enough mana for {data.abilityName}");
            return false;
        }
        
        Activate();
        currentCooldown = data.cooldownsPerLevel[level];
        return true;
    }
    
    protected abstract void Activate();
    
    protected float GetValue()
    {
        return data.valuesPerLevel[level];
    }
    
    protected float GetSecondaryValue()
    {
        return data.secondaryValuesPerLevel[level];
    }
    
    protected void PlayVisualEffect()
    {
        if (data.visualEffectPrefab != null)
        {
            GameObject effect = Instantiate(data.visualEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        if (data.soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(data.soundEffect, transform.position);
        }
    }
}
```

---

## 🎮 PHASE 5: Implement Sample Abilities

### Step 5.1: Orc - Critical Strike

Create: `Assets/Scripts/Abilities/Orc/CriticalStrikeAbility.cs`

```csharp
using UnityEngine;

public class CriticalStrikeAbility : Ability
{
    private Weapon weapon;
    
    public override void Initialize(AbilityData abilityData, AbilitySystem system, int abilityLevel)
    {
        base.Initialize(abilityData, system, abilityLevel);
        weapon = GetComponentInChildren<Weapon>();
        
        if (weapon != null)
        {
            weapon.onShoot += CheckCriticalStrike;
        }
    }
    
    protected override void Activate()
    {
        // Passive ability, no activation needed
    }
    
    void CheckCriticalStrike()
    {
        float critChance = GetValue(); // 8%, 12%, 15%, 18%, 20%
        float critMultiplier = GetSecondaryValue(); // 1.5x, 1.75x, 2.0x, 2.25x, 2.5x
        
        if (Random.Range(0f, 100f) < critChance)
        {
            // Apply critical strike
            weapon.nextShotMultiplier = critMultiplier;
            
            // Visual effect
            PlayVisualEffect();
            Debug.Log($"CRITICAL STRIKE! {critMultiplier}x damage");
        }
    }
    
    void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.onShoot -= CheckCriticalStrike;
        }
    }
}
```

### Step 5.2: Human - Teleport

Create: `Assets/Scripts/Abilities/Human/TeleportAbility.cs`

```csharp
using UnityEngine;

public class TeleportAbility : Ability
{
    protected override void Activate()
    {
        float distance = GetValue(); // 5m, 7m, 9m, 11m, 13m
        
        // Calculate teleport destination
        Vector3 direction = transform.forward;
        Vector3 destination = transform.position + direction * distance;
        
        // Check if path is clear (raycast)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            // Stop at wall
            destination = hit.point - direction * 0.5f;
        }
        
        // Teleport
        transform.position = destination;
        
        // Visual effect
        PlayVisualEffect();
        
        Debug.Log($"Teleported {distance}m forward");
    }
}
```

### Step 5.3: Night Elf - Evasion

Create: `Assets/Scripts/Abilities/NightElf/EvasionAbility.cs`

```csharp
using UnityEngine;

public class EvasionAbility : Ability
{
    private PlayerInfo playerInfo;
    
    public override void Initialize(AbilityData abilityData, AbilitySystem system, int abilityLevel)
    {
        base.Initialize(abilityData, system, abilityLevel);
        playerInfo = GetComponent<PlayerInfo>();
        
        if (playerInfo != null)
        {
            playerInfo.onTakeDamage += TryEvade;
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    bool TryEvade(ref float damage)
    {
        float dodgeChance = GetValue(); // 10%, 13%, 16%, 19%, 22%
        
        if (Random.Range(0f, 100f) < dodgeChance)
        {
            damage = 0; // Completely avoid damage
            PlayVisualEffect();
            Debug.Log("EVADED!");
            return true;
        }
        return false;
    }
    
    void OnDestroy()
    {
        if (playerInfo != null)
        {
            playerInfo.onTakeDamage -= TryEvade;
        }
    }
}
```

---

## 🎨 PHASE 6: Update Weapon Script

### Step 6.1: Enhance Weapon.cs

Open `Assets/Scripts/Combat/Weapon.cs` and add:

```csharp
public class Weapon : MonoBehaviour
{
    // ... existing code ...
    
    [Header("Race Modifiers")]
    public float damageMultiplier = 1.0f; // Set by AbilitySystem
    public float nextShotMultiplier = 1.0f; // For critical strikes
    
    [Header("Events")]
    public System.Action onShoot; // For abilities to hook into
    
    void Shoot()
    {
        // ... existing shoot code ...
        
        // Trigger onShoot event BEFORE shooting
        onShoot?.Invoke();
        
        // Calculate final damage
        float finalDamage = weaponData.damage * damageMultiplier * nextShotMultiplier;
        
        // Apply damage to target
        // ... existing damage code ...
        
        // Reset next shot multiplier
        nextShotMultiplier = 1.0f;
    }
}
```

---

## 🖥️ PHASE 7: Update UI

### Step 7.1: Create Ability UI

Create: `Assets/Scripts/UI/AbilityUI.cs`

```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [Header("References")]
    public AbilitySystem abilitySystem;
    
    [Header("UI Elements")]
    public Image ultimateIcon;
    public Image ability2Icon;
    public Image ability3Icon;
    
    public TextMeshProUGUI ultimateCooldownText;
    public TextMeshProUGUI ability2CooldownText;
    public TextMeshProUGUI ability3CooldownText;
    
    public Image manaBargame (fill image for Blood Elf)
    public TextMeshProUGUI manaText;
    
    void Update()
    {
        if (abilitySystem == null) return;
        
        // Update cooldown displays
        UpdateAbilityIcon(ultimateIcon, ultimateCooldownText, abilitySystem.ultimateAbility);
        UpdateAbilityIcon(ability2Icon, ability2CooldownText, abilitySystem.ability2);
        UpdateAbilityIcon(ability3Icon, ability3CooldownText, abilitySystem.ability3);
        
        // Update mana bar (Blood Elf)
        if (abilitySystem.maxMana > 0)
        {
            manaBar.fillAmount = abilitySystem.currentMana / abilitySystem.maxMana;
            manaText.text = $"{Mathf.CeilToInt(abilitySystem.currentMana)}/{abilitySystem.maxMana}";
            manaBar.gameObject.SetActive(true);
        }
        else
        {
            manaBar.gameObject.SetActive(false);
        }
    }
    
    void UpdateAbilityIcon(Image icon, TextMeshProUGUI cooldownText, Ability ability)
    {
        if (ability == null) return;
        
        if (ability.IsReady)
        {
            icon.color = Color.white; // Full color
            cooldownText.text = "";
        }
        else
        {
            icon.color = Color.gray; // Grayscale
            cooldownText.text = Mathf.CeilToInt(ability.CooldownRemaining).ToString();
        }
    }
}
```

---

## ✅ Quick Setup Checklist

### Data Assets (Unity Editor):
- [ ] Create 8 RaceData assets
- [ ] Create 24 AbilityData assets (3 per race)
- [ ] Create 20 WeaponData assets
- [ ] Configure all stats from balance guide

### Scripts (C# Code):
- [ ] Update RaceData.cs with abilities
- [ ] Create AbilityData.cs ScriptableObject
- [ ] Update WeaponData.cs with full stats
- [ ] Create AbilitySystem.cs component
- [ ] Create base Ability.cs class
- [ ] Create 24 ability scripts (one per ability)
- [ ] Update Weapon.cs with modifiers
- [ ] Create AbilityUI.cs for cooldowns

### Scene Setup:
- [ ] Add AbilitySystem component to Player
- [ ] Assign RaceData to AbilitySystem
- [ ] Add AbilityUI to Canvas
- [ ] Link ability icons/text to UI
- [ ] Test each ability individually

---

## 🚀 Testing Protocol

### Test Each Race:
1. **Select race** in AbilitySystem component
2. **Press Play** in Unity
3. **Test Ultimate** (Q key)
4. **Test Ability 2** (E key)
5. **Test Ability 3** (F key)
6. **Verify cooldowns** display correctly
7. **Check visual effects** appear
8. **Confirm damage/healing** values

### Test Each Weapon:
1. **Select weapon** in WeaponManager
2. **Check damage** numbers
3. **Verify fire rate** feels correct
4. **Test magazine/reload**
5. **Confirm movement speed**
6. **Test special features** (scope, silencer, burst)

---

## 📝 Notes

### Implementation Priority:
1. **Start with Orc** - Simplest abilities (passives)
2. **Then Human** - Basic active abilities
3. **Then Undead** - Passive effects
4. **Save Blood Elf for last** - Most complex (mana system)

### Time Estimates:
- **Creating data assets:** 1-2 hours
- **Core ability system:** 1 hour
- **Basic abilities (3 races):** 2 hours
- **All abilities (8 races):** 4-5 hours
- **UI & polish:** 1 hour
- **Testing & balancing:** 2-3 hours

**Total: 11-14 hours for complete implementation**

### Quick Start:
If you want to test quickly:
1. Create **only Orc race** (30 min)
2. Create **only AK-47 weapon** (10 min)
3. Implement **Critical Strike ability** (20 min)
4. Test and iterate

---

## 🆘 Troubleshooting

**Abilities not activating?**
- Check AbilitySystem is on Player object
- Verify RaceData is assigned
- Check console for error messages

**Cooldowns not working?**
- Ensure Update() is called in Ability.cs
- Verify cooldown values in AbilityData

**Visual effects not showing?**
- Assign effect prefabs in AbilityData
- Check effect layer/scale is visible

**Mana not regenerating?**
- Verify race is "Blood Elf"
- Check manaRegenRate > 0
- Ensure not taking damage (pauses regen)

---

## 📚 Next Steps

After implementing:
1. **Read [ARCHITECTURE.md](ARCHITECTURE.md)** for code organization
2. **Review [DEBUG_COMMANDS.md](DEBUG_COMMANDS.md)** for testing
3. **Check [RACES_WEAPONS_ABILITIES_COMPLETE.md](RACES_WEAPONS_ABILITIES_COMPLETE.md)** for balance details
4. **Playtest extensively** and adjust values
5. **Add visual effects** and polish

---

**Ready to implement? Start with Phase 1!** 🎮

*Estimated completion time: 4-6 hours for basic setup, 11-14 hours for full implementation*