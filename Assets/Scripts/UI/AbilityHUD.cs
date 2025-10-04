using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// In-game HUD for displaying ability cooldowns and status
/// </summary>
public class AbilityHUD : MonoBehaviour
{
    [Header("Ability UI Elements")]
    public GameObject[] abilityPanels;
    public Image[] abilityIcons;
    public TextMeshProUGUI[] abilityNames;
    public TextMeshProUGUI[] cooldownTexts;
    public Image[] cooldownOverlays;
    public TextMeshProUGUI[] levelTexts;
    
    [Header("Passive Indicator")]
    public GameObject passiveIndicator;
    public TextMeshProUGUI passiveText;
    
    [Header("Mana Display (Blood Elf)")]
    public GameObject manaPanel;
    public Slider manaBar;
    public TextMeshProUGUI manaText;

    private AbilitySystem abilitySystem;
    private PlayerRace playerRace;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            abilitySystem = player.GetComponent<AbilitySystem>();
            playerRace = player.GetComponent<PlayerRace>();
        }

        UpdateAbilityDisplay();
    }

    void Update()
    {
        if (abilitySystem == null) return;

        UpdateCooldowns();
        UpdateMana();
    }

    void UpdateAbilityDisplay()
    {
        if (playerRace == null || playerRace.CurrentRace == null) return;

        RaceData race = playerRace.CurrentRace;
        
        // Update each ability slot
        for (int i = 0; i < 3; i++)
        {
            if (i < race.abilities.Length && race.abilities[i] != null)
            {
                abilityPanels[i].SetActive(true);
                
                AbilityData ability = race.abilities[i];
                
                // Set icon
                if (abilityIcons[i] != null && ability.abilityIcon != null)
                {
                    abilityIcons[i].sprite = ability.abilityIcon;
                }
                
                // Set name
                if (abilityNames[i] != null)
                {
                    abilityNames[i].text = ability.abilityName;
                }
                
                // Set level
                if (levelTexts[i] != null)
                {
                    int level = abilitySystem != null ? abilitySystem.GetAbilityLevel(i) : 1;
                    levelTexts[i].text = $"Lvl {level}";
                }
            }
            else
            {
                abilityPanels[i].SetActive(false);
            }
        }

        // Show mana panel for Blood Elf
        if (manaPanel != null)
        {
            manaPanel.SetActive(race.raceType == RaceType.BloodElf);
        }
    }

    void UpdateCooldowns()
    {
        if (playerRace == null || playerRace.CurrentRace == null) return;

        for (int i = 0; i < 3; i++)
        {
            if (i >= playerRace.CurrentRace.abilities.Length) continue;
            
            AbilityData ability = playerRace.CurrentRace.abilities[i];
            if (ability == null) continue;

            // Get cooldown from ability system
            float remainingCooldown = abilitySystem.GetRemainingCooldown(i);
            bool isOnCooldown = remainingCooldown > 0;

            // Update cooldown text
            if (cooldownTexts[i] != null)
            {
                if (isOnCooldown)
                {
                    cooldownTexts[i].gameObject.SetActive(true);
                    cooldownTexts[i].text = remainingCooldown.ToString("F1") + "s";
                }
                else
                {
                    cooldownTexts[i].gameObject.SetActive(false);
                }
            }

            // Update cooldown overlay
            if (cooldownOverlays[i] != null)
            {
                if (isOnCooldown)
                {
                    float cooldownPercent = remainingCooldown / GetAbilityCooldown(ability, abilitySystem.GetAbilityLevel(i));
                    cooldownOverlays[i].fillAmount = cooldownPercent;
                    cooldownOverlays[i].gameObject.SetActive(true);
                }
                else
                {
                    cooldownOverlays[i].gameObject.SetActive(false);
                }
            }
        }
    }

    float GetAbilityCooldown(AbilityData ability, int level)
    {
        // Get cooldown based on ability level (1-5)
        return ability.cooldown; // This should be adjusted based on level in your ability implementation
    }

    void UpdateMana()
    {
        if (!manaPanel.activeSelf) return;

        // Blood Elf mana system
        BloodElfManaShieldAbility manaShield = FindObjectOfType<BloodElfManaShieldAbility>();
        if (manaShield != null)
        {
            float currentMana = manaShield.currentMana;
            float maxMana = manaShield.GetMaxMana();
            
            if (manaBar != null)
            {
                manaBar.maxValue = maxMana;
                manaBar.value = currentMana;
            }

            if (manaText != null)
            {
                manaText.text = $"{currentMana:F0} / {maxMana:F0}";
            }
        }
    }

    /// <summary>
    /// Show ability level up notification
    /// </summary>
    public void ShowAbilityLevelUp(int abilityIndex, int newLevel)
    {
        // TODO: Add visual/audio feedback for level up
        Debug.Log($"Ability {abilityIndex} leveled up to {newLevel}!");
    }

    /// <summary>
    /// Flash ability on cooldown
    /// </summary>
    public void FlashAbilityCooldown(int abilityIndex)
    {
        // TODO: Add flash animation when ability used
    }
}
