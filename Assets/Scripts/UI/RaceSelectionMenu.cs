using UnityEngine;
using UnityEngine.UI;

public class RaceSelectionMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject menuPanel;
    public Transform raceButtonContainer;
    public GameObject raceButtonPrefab;
    
    [Header("Race Data")]
    public RaceData[] availableRaces;
    
    [Header("Info Display")]
    public Text raceNameText;
    public Text raceDescriptionText;
    public Text raceStatsText;
    
    private PlayerRace playerRace;
    private RaceData selectedRace;
    
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRace = player.GetComponent<PlayerRace>();
        }
        
        // Create race selection buttons
        CreateRaceButtons();
        
        // Show menu at start
        ShowMenu();
    }
    
    private void Update()
    {
        // Toggle menu with Tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
    }
    
    private void CreateRaceButtons()
    {
        foreach (RaceData race in availableRaces)
        {
            GameObject buttonObj = Instantiate(raceButtonPrefab, raceButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            
            if (buttonText != null)
                buttonText.text = race.raceName;
            
            // Add click listener
            button.onClick.AddListener(() => SelectRace(race));
        }
    }
    
    private void SelectRace(RaceData race)
    {
        selectedRace = race;
        DisplayRaceInfo(race);
    }
    
    private void DisplayRaceInfo(RaceData race)
    {
        if (raceNameText != null)
            raceNameText.text = race.raceName;
        
        if (raceDescriptionText != null)
            raceDescriptionText.text = race.description;
        
        if (raceStatsText != null)
        {
            string stats = $"Health: {race.healthMultiplier:P0}\n";
            stats += $"Speed: {race.speedMultiplier:P0}\n";
            stats += $"Damage: {race.damageMultiplier:P0}\n";
            stats += $"Armor Bonus: +{race.armorBonus}\n";
            stats += $"Health Regen: {race.healthRegenPerSecond}/s\n\n";
            stats += "Abilities:\n";
            
            if (race.ultimateAbility != null)
                stats += $"- {race.ultimateAbility.abilityName} (Ultimate)\n";
            if (race.ability2 != null)
                stats += $"- {race.ability2.abilityName} (Ability 2)\n";
            if (race.ability3 != null)
                stats += $"- {race.ability3.abilityName} (Ability 3)\n";
            
            raceStatsText.text = stats;
        }
    }
    
    public void ConfirmRaceSelection()
    {
        if (selectedRace != null && playerRace != null)
        {
            playerRace.SetRace(selectedRace);
            
            // Note: Abilities are now handled by AbilitySystem component
            // which automatically initializes from the RaceData
            
            HideMenu();
            
            // Unlock cursor when game starts
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    public void ShowMenu()
    {
        if (menuPanel != null)
            menuPanel.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // Pause game
    }
    
    public void HideMenu()
    {
        if (menuPanel != null)
            menuPanel.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f; // Resume game
    }
    
    public void ToggleMenu()
    {
        if (menuPanel != null)
        {
            if (menuPanel.activeSelf)
                HideMenu();
            else
                ShowMenu();
        }
    }
}
