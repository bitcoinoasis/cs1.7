using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Race selection menu UI - displays all 8 races with stats and abilities
/// </summary>
public class RaceSelectionUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject selectionPanel;
    public Transform raceButtonContainer;
    public GameObject raceButtonPrefab;
    
    [Header("Race Details Panel")]
    public TextMeshProUGUI raceNameText;
    public TextMeshProUGUI raceDescriptionText;
    public TextMeshProUGUI raceTierText;
    public Image raceIcon;
    
    [Header("Stats Display")]
    public TextMeshProUGUI healthMultiplierText;
    public TextMeshProUGUI damageMultiplierText;
    public TextMeshProUGUI speedMultiplierText;
    public Slider healthBar;
    public Slider damageBar;
    public Slider speedBar;
    
    [Header("Abilities Display")]
    public GameObject[] abilitySlots;
    public TextMeshProUGUI[] abilityNameTexts;
    public TextMeshProUGUI[] abilityDescriptionTexts;
    public Image[] abilityIcons;
    
    [Header("Race Data")]
    public RaceData[] allRaces;
    
    private RaceData selectedRace;
    private List<Button> raceButtons = new List<Button>();

    void Start()
    {
        PopulateRaceButtons();
        selectionPanel.SetActive(true);
    }

    void PopulateRaceButtons()
    {
        // Clear existing buttons
        foreach (Transform child in raceButtonContainer)
        {
            Destroy(child.gameObject);
        }
        raceButtons.Clear();

        // Create button for each race
        foreach (RaceData race in allRaces)
        {
            GameObject buttonObj = Instantiate(raceButtonPrefab, raceButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            
            // Set button text/icon
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = race.raceName;
            }

            // Set color based on tier
            Image buttonImage = buttonObj.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = GetTierColor(race.tier);
            }

            // Add click listener
            RaceData raceRef = race; // Capture for lambda
            button.onClick.AddListener(() => OnRaceSelected(raceRef));
            
            raceButtons.Add(button);
        }

        // Select first race by default
        if (allRaces.Length > 0)
        {
            OnRaceSelected(allRaces[0]);
        }
    }

    void OnRaceSelected(RaceData race)
    {
        selectedRace = race;
        UpdateRaceDetails();
    }

    void UpdateRaceDetails()
    {
        if (selectedRace == null) return;

        // Update name and description
        raceNameText.text = selectedRace.raceName;
        raceDescriptionText.text = selectedRace.description;
        raceTierText.text = $"Tier: {selectedRace.tier}";
        raceTierText.color = GetTierColor(selectedRace.tier);

        // Update icon
        if (raceIcon != null && selectedRace.raceIcon != null)
        {
            raceIcon.sprite = selectedRace.raceIcon;
        }

        // Update stat multipliers
        healthMultiplierText.text = $"HP: {selectedRace.healthMultiplier:P0}";
        damageMultiplierText.text = $"Damage: {selectedRace.damageMultiplier:P0}";
        speedMultiplierText.text = $"Speed: {selectedRace.speedMultiplier:P0}";

        // Update stat bars (normalized to 0.8-1.2 range)
        healthBar.value = Mathf.InverseLerp(0.8f, 1.2f, selectedRace.healthMultiplier);
        damageBar.value = Mathf.InverseLerp(0.8f, 1.2f, selectedRace.damageMultiplier);
        speedBar.value = Mathf.InverseLerp(0.8f, 1.2f, selectedRace.speedMultiplier);

        // Update abilities
        for (int i = 0; i < 3; i++)
        {
            if (i < selectedRace.abilities.Length && selectedRace.abilities[i] != null)
            {
                abilitySlots[i].SetActive(true);
                
                AbilityData ability = selectedRace.abilities[i];
                abilityNameTexts[i].text = ability.abilityName;
                abilityDescriptionTexts[i].text = ability.description;
                
                if (abilityIcons[i] != null && ability.abilityIcon != null)
                {
                    abilityIcons[i].sprite = ability.abilityIcon;
                }
            }
            else
            {
                abilitySlots[i].SetActive(false);
            }
        }
    }

    Color GetTierColor(string tier)
    {
        switch (tier.ToUpper())
        {
            case "S":
                return new Color(1f, 0.84f, 0f); // Gold
            case "A":
                return new Color(0.75f, 0.75f, 0.75f); // Silver
            case "B":
                return new Color(0.8f, 0.5f, 0.2f); // Bronze
            case "C":
                return Color.white;
            default:
                return Color.white;
        }
    }

    public void ConfirmSelection()
    {
        if (selectedRace != null)
        {
            // Apply race to player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerRace playerRace = player.GetComponent<PlayerRace>();
                if (playerRace == null)
                {
                    playerRace = player.AddComponent<PlayerRace>();
                }
                playerRace.SetRace(selectedRace);
                
                Debug.Log($"Selected race: {selectedRace.raceName}");
            }

            // Close selection menu
            selectionPanel.SetActive(false);
        }
    }

    public void RandomSelection()
    {
        if (allRaces.Length > 0)
        {
            RaceData randomRace = allRaces[Random.Range(0, allRaces.Length)];
            OnRaceSelected(randomRace);
            ConfirmSelection();
        }
    }

    public void ShowSelectionMenu()
    {
        selectionPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void HideSelectionMenu()
    {
        selectionPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }
}
