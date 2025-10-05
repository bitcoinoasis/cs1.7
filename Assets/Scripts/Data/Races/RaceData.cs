using UnityEngine;

public enum RaceType
{
    Human,
    Orc,
    Undead,
    NightElf,
    BloodElf,
    Troll,
    Dwarf,
    Celestial
}

public enum RaceTier
{
    Starter,      // Easy to learn, forgiving
    Intermediate, // Requires strategy
    Advanced,     // High skill ceiling
    Expert        // Complex mechanics
}

[CreateAssetMenu(fileName = "New Race", menuName = "Warcraft/Race Data")]
public class RaceData : ScriptableObject
{
    [Header("Race Info")]
    public RaceType raceType;
    public string raceName;
    [TextArea(2, 4)]
    public string description;
    public RaceTier tier = RaceTier.Starter;
    
    [Header("Base Stats")]
    public float healthMultiplier = 1f;
    public float speedMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float armorBonus = 0f;
    public float healthRegenPerSecond = 0f;
    
    [Header("Abilities (3 total)")]
    public AbilityData ultimateAbility;   // Q key
    public AbilityData ability2;          // E key
    public AbilityData ability3;          // F key
    
    [Header("Visual")]
    public Color raceColor = Color.white;
    public Sprite raceIcon;
    
    [Header("Leveling")]
    public int maxLevel = 10;
    public int[] xpRequiredPerLevel; // XP needed for each level
    
    [Header("Balance Info")]
    [TextArea(2, 3)]
    public string playstyle;
    [TextArea(2, 3)]
    public string bestFor;
    [TextArea(2, 3)]
    public string counters;
    
    // Helper property for UI access
    public AbilityData[] abilities => new AbilityData[] { ultimateAbility, ability2, ability3 };
}
