using UnityEngine;

/// <summary>
/// Simple race selection menu using OnGUI
/// Shows on first spawn, allows choosing from 8 races
/// </summary>
public class SimpleRaceSelection : MonoBehaviour
{
    [Header("Race Selection")]
    public RaceData[] availableRaces;
    public bool showOnStart = true;
    
    private bool menuOpen = false;
    private Vector2 scrollPosition;
    private PlayerRace playerRace;
    private bool hasSelectedRace = false;

    private void Start()
    {
        playerRace = GetComponent<PlayerRace>();
        
        // Load all races from Resources
        if (availableRaces == null || availableRaces.Length == 0)
        {
            availableRaces = Resources.LoadAll<RaceData>("Races");
            if (availableRaces.Length == 0)
            {
                availableRaces = UnityEngine.Resources.FindObjectsOfTypeAll<RaceData>();
            }
        }

        if (showOnStart && !hasSelectedRace)
        {
            menuOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; // Pause game
        }
    }

    private void Update()
    {
        // Toggle menu with R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleMenu();
        }
    }

    private void OnGUI()
    {
        if (!menuOpen) return;

        // Semi-transparent background
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        
        // Main menu panel
        float panelWidth = 800;
        float panelHeight = 600;
        float panelX = (Screen.width - panelWidth) / 2;
        float panelY = (Screen.height - panelHeight) / 2;
        
        GUILayout.BeginArea(new Rect(panelX, panelY, panelWidth, panelHeight));
        
        // Title
        GUILayout.BeginVertical("box");
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 24;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("SELECT YOUR RACE", titleStyle);
        
        GUILayout.Space(20);
        
        // Scroll view for races
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(450));
        
        if (availableRaces != null && availableRaces.Length > 0)
        {
            foreach (RaceData race in availableRaces)
            {
                if (race == null) continue;
                
                GUILayout.BeginHorizontal("box");
                
                // Race info
                GUILayout.BeginVertical(GUILayout.Width(550));
                
                GUIStyle raceNameStyle = new GUIStyle(GUI.skin.label);
                raceNameStyle.fontSize = 18;
                raceNameStyle.fontStyle = FontStyle.Bold;
                raceNameStyle.normal.textColor = GetTierColor(race.tier);
                GUILayout.Label(race.raceName + $" (Tier {race.tier})", raceNameStyle);
                
                GUILayout.Label(race.description);
                GUILayout.Space(5);
                
                // Stats
                GUILayout.Label($"Health: {race.healthMultiplier:F2}x | Damage: {race.damageMultiplier:F2}x | Speed: {race.speedMultiplier:F2}x");
                
                // Abilities
                GUILayout.Label($"Abilities: {race.GetAbilityCount()}", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic });
                if (race.ultimateAbility != null) GUILayout.Label($"  Q: {race.ultimateAbility.abilityName}", new GUIStyle(GUI.skin.label) { fontSize = 10 });
                if (race.ability2 != null) GUILayout.Label($"  E: {race.ability2.abilityName}", new GUIStyle(GUI.skin.label) { fontSize = 10 });
                if (race.ability3 != null) GUILayout.Label($"  R: {race.ability3.abilityName}", new GUIStyle(GUI.skin.label) { fontSize = 10 });
                
                GUILayout.EndVertical();
                
                // Select button
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("SELECT", GUILayout.Width(150), GUILayout.Height(80)))
                {
                    SelectRace(race);
                }
                
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
            }
        }
        else
        {
            GUILayout.Label("No races available! Please run the Game Builder first.");
        }
        
        GUILayout.EndScrollView();
        
        GUILayout.Space(10);
        
        // Current race display
        if (playerRace != null && playerRace.currentRace != null)
        {
            GUILayout.Label($"Current Race: {playerRace.currentRace.raceName}", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
        }
        
        // Close button (only if race already selected)
        if (hasSelectedRace)
        {
            if (GUILayout.Button("Close (Press R to reopen)", GUILayout.Height(30)))
            {
                CloseMenu();
            }
        }
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void SelectRace(RaceData race)
    {
        if (playerRace == null) return;

        playerRace.SetRace(race);
        hasSelectedRace = true;
        
        Debug.Log($"[SimpleRaceSelection] Selected race: {race.raceName}");
        
        // Show confirmation
        if (showOnStart)
        {
            CloseMenu();
        }
    }

    private void ToggleMenu()
    {
        menuOpen = !menuOpen;
        
        if (menuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            CloseMenu();
        }
    }

    private void CloseMenu()
    {
        menuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    private Color GetTierColor(RaceTier tier)
    {
        switch (tier)
        {
            case RaceTier.Starter: return Color.white;
            case RaceTier.Intermediate: return Color.green;
            case RaceTier.Advanced: return Color.cyan;
            case RaceTier.Expert: return Color.magenta;
            default: return Color.gray;
        }
    }
}
