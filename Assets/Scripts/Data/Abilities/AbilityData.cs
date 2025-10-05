using UnityEngine;

public enum AbilityType
{
    Passive,           // Always active (Critical Strike, Evasion)
    ActiveSelf,        // Self-cast (Teleport, Invisibility)
    ActiveTargeted,    // Aim at target (Healing Wave, Mana Burn)
    ActiveAoE,         // Area of effect (Ground Slam, Unholy Aura)
    PassiveTrigger     // Passive but triggers on condition (Reincarnation, Bash)
}

[CreateAssetMenu(fileName = "New Ability", menuName = "Warcraft/Ability Data")]
public class AbilityData : ScriptableObject
{
    [Header("Basic Info")]
    public string abilityName;
    [TextArea(2, 4)]
    public string description;
    public AbilityType abilityType;
    public Sprite abilityIcon;
    
    [Header("Stats per Level (5 levels)")]
    [Tooltip("Main stat: damage, heal, chance %, etc.")]
    public float[] valuesPerLevel = new float[5] { 0, 0, 0, 0, 0 };
    
    [Tooltip("Secondary stat: duration, range, multiplier, etc.")]
    public float[] secondaryValuesPerLevel = new float[5] { 0, 0, 0, 0, 0 };
    
    [Tooltip("Cooldown in seconds (0 = passive)")]
    public float[] cooldownsPerLevel = new float[5] { 0, 0, 0, 0, 0 };
    
    [Header("Costs")]
    [Tooltip("Mana cost per level (for Blood Elf)")]
    public float[] manaCostPerLevel = new float[5] { 0, 0, 0, 0, 0 };
    
    [Header("Visual Effects")]
    public GameObject visualEffectPrefab;
    public Color effectColor = Color.white;
    public AudioClip soundEffect;
    
    [Header("Balance Notes")]
    [TextArea(3, 5)]
    public string balanceNotes;
    
    // Helper property for UI - returns level 1 cooldown
    public float cooldown => cooldownsPerLevel != null && cooldownsPerLevel.Length > 0 ? cooldownsPerLevel[0] : 0f;
}
