// 2025-10-04 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

/// <summary>
/// Automatically initializes all required game systems when scene loads
/// </summary>
public class SceneInitializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        Debug.Log("[SceneInitializer] Starting automatic scene setup...");
        
        // Create all game systems in order
        CreateGameManagers();
        CreateUICanvas();
        CreatePlayer();
        CreateSpawnPoints();
        
        Debug.Log("[SceneInitializer] Scene setup complete!");
    }

    static void CreateGameManagers()
    {
        // Check if already exists
        if (GameObject.Find("GameManagers") != null)
        {
            Debug.Log("[SceneInitializer] GameManagers already exists, skipping creation.");
            return;
        }

        // Create GameManagers parent GameObject
        GameObject gameManagers = new GameObject("GameManagers");
        DontDestroyOnLoad(gameManagers);

        // GameModeManager
        GameObject gameModeObj = new GameObject("GameModeManager");
        gameModeObj.transform.SetParent(gameManagers.transform);
        GameModeManager gameModeManager = gameModeObj.AddComponent<GameModeManager>();
        
        // Configure default settings
        gameModeManager.currentGameMode = GameModeManager.GameMode.TeamDeathmatch;
        gameModeManager.killsToWin = 30;
        gameModeManager.roundTimeLimit = 120f;
        gameModeManager.buyTimeSeconds = 15;

        // EconomySystem
        GameObject economyObj = new GameObject("EconomySystem");
        economyObj.transform.SetParent(gameManagers.transform);
        EconomySystem economySystem = economyObj.AddComponent<EconomySystem>();
        
        // Configure economy settings
        economySystem.startingMoney = 800;
        economySystem.maxMoney = 16000;
        economySystem.standardKillReward = 300;

        // BotSpawner
        GameObject botSpawnerObj = new GameObject("BotSpawner");
        botSpawnerObj.transform.SetParent(gameManagers.transform);
        BotSpawner botSpawner = botSpawnerObj.AddComponent<BotSpawner>();
        
        // Configure bot spawner
        botSpawner.team1BotCount = 4;
        botSpawner.team2BotCount = 4;
        botSpawner.spawnOnStart = false; // Wait for spawn points to be created
        botSpawner.defaultDifficulty = BotAI.BotDifficulty.Medium;

        Debug.Log("[SceneInitializer] GameManagers created with GameModeManager, EconomySystem, and BotSpawner");
    }

    static void CreateUICanvas()
    {
        // Check if already exists
        if (GameObject.Find("UI Canvas") != null)
        {
            Debug.Log("[SceneInitializer] UI Canvas already exists, skipping creation.");
            return;
        }

        // Create Canvas
        GameObject canvasObj = new GameObject("UI Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

        // Create UI components
        CreateCrosshair(canvasObj.transform);
        CreateAbilityHUD(canvasObj.transform);
        CreateBuyMenu(canvasObj.transform);
        CreateScoreboard(canvasObj.transform);
        CreateKillFeed(canvasObj.transform);

        Debug.Log("[SceneInitializer] UI Canvas created with all UI components");
    }

    static void CreateCrosshair(Transform parent)
    {
        GameObject crosshairObj = new GameObject("Crosshair");
        crosshairObj.transform.SetParent(parent);
        
        RectTransform rect = crosshairObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(20, 20);
        rect.anchoredPosition = Vector2.zero;

        Crosshair crosshair = crosshairObj.AddComponent<Crosshair>();
        
        // Create default crosshair image
        GameObject imageObj = new GameObject("Image");
        imageObj.transform.SetParent(crosshairObj.transform);
        RectTransform imageRect = imageObj.AddComponent<RectTransform>();
        imageRect.anchorMin = Vector2.zero;
        imageRect.anchorMax = Vector2.one;
        imageRect.sizeDelta = Vector2.zero;
        imageRect.anchoredPosition = Vector2.zero;
        
        UnityEngine.UI.Image image = imageObj.AddComponent<UnityEngine.UI.Image>();
        image.color = Color.white;
    }

    static void CreateAbilityHUD(Transform parent)
    {
        GameObject hudObj = new GameObject("AbilityHUD");
        hudObj.transform.SetParent(parent);
        
        RectTransform rect = hudObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.pivot = new Vector2(0, 0);
        rect.anchoredPosition = new Vector2(20, 20);
        rect.sizeDelta = new Vector2(400, 100);

        AbilityHUD abilityHUD = hudObj.AddComponent<AbilityHUD>();
    }

    static void CreateBuyMenu(Transform parent)
    {
        GameObject buyMenuObj = new GameObject("BuyMenu");
        buyMenuObj.transform.SetParent(parent);
        
        RectTransform rect = buyMenuObj.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;

        BuyMenuUI buyMenu = buyMenuObj.AddComponent<BuyMenuUI>();
        buyMenuObj.SetActive(false); // Hidden by default
    }

    static void CreateScoreboard(Transform parent)
    {
        GameObject scoreboardObj = new GameObject("Scoreboard");
        scoreboardObj.transform.SetParent(parent);
        
        RectTransform rect = scoreboardObj.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;

        ScoreboardUI scoreboard = scoreboardObj.AddComponent<ScoreboardUI>();
        scoreboardObj.SetActive(false); // Hidden by default (show with Tab key)
    }

    static void CreateKillFeed(Transform parent)
    {
        GameObject killFeedObj = new GameObject("KillFeed");
        killFeedObj.transform.SetParent(parent);
        
        RectTransform rect = killFeedObj.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 1);
        rect.anchorMax = new Vector2(1, 1);
        rect.pivot = new Vector2(1, 1);
        rect.anchoredPosition = new Vector2(-20, -20);
        rect.sizeDelta = new Vector2(300, 400);

        KillFeedUI killFeed = killFeedObj.AddComponent<KillFeedUI>();
    }

    static void CreatePlayer()
    {
        // Check if already exists
        if (GameObject.Find("Player") != null)
        {
            Debug.Log("[SceneInitializer] Player already exists, skipping creation.");
            return;
        }

        GameObject playerObj = new GameObject("Player");
        playerObj.tag = "Player";
        playerObj.layer = LayerMask.NameToLayer("Default");

        // Add player components
        CharacterController controller = playerObj.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = new Vector3(0, 1, 0);

        playerObj.AddComponent<PlayerController>();
        playerObj.AddComponent<PlayerHealth>();
        playerObj.AddComponent<PlayerRace>();
        playerObj.AddComponent<AbilitySystem>();

        // Create camera
        GameObject cameraObj = new GameObject("Main Camera");
        cameraObj.tag = "MainCamera";
        cameraObj.transform.SetParent(playerObj.transform);
        cameraObj.transform.localPosition = new Vector3(0, 1.6f, 0);
        
        Camera camera = cameraObj.AddComponent<Camera>();
        camera.fieldOfView = 90f;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 1000f;
        
        cameraObj.AddComponent<AudioListener>();

        // Create weapon holder
        GameObject weaponHolder = new GameObject("WeaponHolder");
        weaponHolder.transform.SetParent(cameraObj.transform);
        weaponHolder.transform.localPosition = new Vector3(0, -0.2f, 0.5f);

        // Position player at spawn
        playerObj.transform.position = new Vector3(0, 1, 0);

        Debug.Log("[SceneInitializer] Player created with camera and components");
    }

    static void CreateSpawnPoints()
    {
        // Check if already exists
        if (GameObject.Find("SpawnPoints") != null)
        {
            Debug.Log("[SceneInitializer] SpawnPoints already exist, skipping creation.");
            return;
        }

        GameObject spawnPointsParent = new GameObject("SpawnPoints");

        // Create Team 1 (Terrorist) spawns
        GameObject team1Parent = new GameObject("Team1_Spawns");
        team1Parent.transform.SetParent(spawnPointsParent.transform);

        for (int i = 0; i < 5; i++)
        {
            GameObject spawn = new GameObject($"T_Spawn_{i + 1}");
            spawn.tag = "Respawn";
            spawn.transform.SetParent(team1Parent.transform);
            
            // Arrange in a line
            spawn.transform.position = new Vector3(-10 + (i * 2), 1, -10);
            spawn.transform.rotation = Quaternion.Euler(0, 0, 0);

            // Add visual indicator (gizmo)
            SpawnPoint spawnPoint = spawn.AddComponent<SpawnPoint>();
            spawnPoint.teamID = 0;
        }

        // Create Team 2 (Counter-Terrorist) spawns
        GameObject team2Parent = new GameObject("Team2_Spawns");
        team2Parent.transform.SetParent(spawnPointsParent.transform);

        for (int i = 0; i < 5; i++)
        {
            GameObject spawn = new GameObject($"CT_Spawn_{i + 1}");
            spawn.tag = "Respawn";
            spawn.transform.SetParent(team2Parent.transform);
            
            // Arrange in a line opposite side
            spawn.transform.position = new Vector3(-10 + (i * 2), 1, 10);
            spawn.transform.rotation = Quaternion.Euler(0, 180, 0);

            // Add visual indicator
            SpawnPoint spawnPoint = spawn.AddComponent<SpawnPoint>();
            spawnPoint.teamID = 1;
        }

        Debug.Log("[SceneInitializer] Created 10 spawn points (5 per team)");

        // Now that spawn points exist, configure BotSpawner
        BotSpawner botSpawner = GameObject.FindObjectOfType<BotSpawner>();
        if (botSpawner != null)
        {
            // Get spawn point arrays
            Transform team1Spawns = team1Parent.transform;
            Transform team2Spawns = team2Parent.transform;

            botSpawner.team1SpawnPoints = new Transform[team1Spawns.childCount];
            for (int i = 0; i < team1Spawns.childCount; i++)
            {
                botSpawner.team1SpawnPoints[i] = team1Spawns.GetChild(i);
            }

            botSpawner.team2SpawnPoints = new Transform[team2Spawns.childCount];
            for (int i = 0; i < team2Spawns.childCount; i++)
            {
                botSpawner.team2SpawnPoints[i] = team2Spawns.GetChild(i);
            }

            Debug.Log("[SceneInitializer] Configured BotSpawner with spawn points");
        }
    }
}

/// <summary>
/// Helper component to mark and visualize spawn points
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    public int teamID = 0;

    private void OnDrawGizmos()
    {
        // Draw spawn point visualization
        Gizmos.color = teamID == 0 ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
    }
}
