using UnityEngine;
using CS17.Core;

namespace CS17.Core
{
    /// <summary>
    /// Complete game setup - creates all necessary game objects and systems
    /// Attach this to an empty GameObject in the scene
    /// </summary>
    public class CompleteGameSetup : MonoBehaviour
    {
        [Header("Player Settings")]
        public GameObject playerPrefab;
        public Transform playerSpawnPoint;

        [Header("Bot Settings")]
        public GameObject botPrefab;
        public int numberOfBots = 5;
        public Transform[] botSpawnPoints;
        public float botSpawnDelay = 2f;

        [Header("UI Settings")]
        public bool createUIAutomatically = true;

        private GameObject player;
        private GameObject[] bots;

        private void Start()
        {
            Debug.Log("[CompleteGameSetup] Initializing complete game...");

            // Setup in order
            SetupGameManager();
            SetupUI();
            SpawnPlayer();
            SpawnBots();
            SetupBuyZones();
            
            Debug.Log("[CompleteGameSetup] ✅ Complete game setup finished!");
        }

        private void SetupGameManager()
        {
            // GameManager should already exist from SceneInitializer
            if (GameManager.Instance == null)
            {
                GameObject gmObj = new GameObject("GameManager");
                gmObj.AddComponent<GameManager>();
            }

            // Enable demo mode for testing
            if (GameManager.Instance != null)
            {
                GameManager.Instance.demoMode = true;
                Debug.Log("[CompleteGameSetup] ✓ Demo mode enabled");
            }
        }

        private void SetupUI()
        {
            if (!createUIAutomatically) return;

            // Create main UI canvas
            GameObject uiCanvas = GameObject.Find("GameCanvas");
            if (uiCanvas == null)
            {
                uiCanvas = new GameObject("GameCanvas");
                Canvas canvas = uiCanvas.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                uiCanvas.AddComponent<UnityEngine.UI.CanvasScaler>();
                uiCanvas.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            // Create Crosshair
            CreateCrosshair(uiCanvas);

            // Create HUD elements
            CreateHUD(uiCanvas);

            Debug.Log("[CompleteGameSetup] ✓ UI created");
        }

        private void CreateCrosshair(GameObject canvas)
        {
            GameObject crosshairObj = new GameObject("Crosshair");
            crosshairObj.transform.SetParent(canvas.transform);
            
            Crosshair crosshair = crosshairObj.AddComponent<Crosshair>();
            crosshair.crosshairColor = Color.green;
            crosshair.crosshairSize = 8f;
            crosshair.crosshairThickness = 2f;
            crosshair.crosshairGap = 4f;
            crosshair.dynamicCrosshair = true;

            RectTransform rect = crosshairObj.GetComponent<RectTransform>();
            if (rect == null) rect = crosshairObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
        }

        private void CreateHUD(GameObject canvas)
        {
            // Health Display (bottom-left)
            GameObject healthObj = new GameObject("HealthDisplay");
            healthObj.transform.SetParent(canvas.transform);
            
            RectTransform healthRect = healthObj.AddComponent<RectTransform>();
            healthRect.anchorMin = new Vector2(0, 0);
            healthRect.anchorMax = new Vector2(0, 0);
            healthRect.pivot = new Vector2(0, 0);
            healthRect.anchoredPosition = new Vector2(20, 20);
            healthRect.sizeDelta = new Vector2(200, 100);
            
            var healthDisplay = healthObj.AddComponent<SimpleHealthDisplay>();

            // Money Display (top-left) - will be created by SimpleBuyMenu
            
            // Ammo Display (bottom-right) - will be created by weapon system
        }

        private void SpawnPlayer()
        {
            // Find or create player spawn point
            if (playerSpawnPoint == null)
            {
                try
                {
                    GameObject[] spawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
                    if (spawns.Length > 0)
                    {
                        playerSpawnPoint = spawns[0].transform;
                    }
                }
                catch (UnityException)
                {
                    // Tag doesn't exist, create spawn point
                }
                
                if (playerSpawnPoint == null)
                {
                    // Create default spawn
                    GameObject spawnObj = new GameObject("PlayerSpawn");
                    playerSpawnPoint = spawnObj.transform;
                    playerSpawnPoint.position = new Vector3(0, 2, -20);
                    Debug.Log("[CompleteGameSetup] Created default player spawn point");
                }
            }

            // Find or create player prefab
            if (playerPrefab == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                
                if (player == null)
                {
                    // Create basic player
                    player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    player.name = "Player";
                    player.tag = "Player";
                    
                    // Add necessary components
                    player.AddComponent<CharacterController>();
                    player.AddComponent<PlayerController>();
                    player.AddComponent<PlayerHealth>();
                    player.AddComponent<WeaponManager>();
                    player.AddComponent<PlayerRace>();
                    player.AddComponent<PlayerExperience>();
                    player.AddComponent<AbilitySystem>();

                    // Add camera
                    GameObject camObj = new GameObject("PlayerCamera");
                    camObj.transform.SetParent(player.transform);
                    camObj.transform.localPosition = new Vector3(0, 0.5f, 0);
                    Camera cam = camObj.AddComponent<Camera>();
                    
                    // Only add AudioListener if none exists in scene
                    if (FindAnyObjectByType<AudioListener>() == null)
                    {
                        camObj.AddComponent<AudioListener>();
                    }
                    
                    camObj.tag = "MainCamera";

                    player.transform.position = playerSpawnPoint.position;
                }
            }
            else
            {
                player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            }

            // Add buy menu
            if (player.GetComponent<SimpleBuyMenu>() == null)
            {
                SimpleBuyMenu buyMenu = player.AddComponent<SimpleBuyMenu>();
                buyMenu.requireBuyTime = false; // Allow buying anytime in demo mode
                
                // Load weapon shop items from generated weapons
                LoadWeaponShopItems(buyMenu);
            }

            // Add race selection menu
            if (player.GetComponent<SimpleRaceSelection>() == null)
            {
                SimpleRaceSelection raceMenu = player.AddComponent<SimpleRaceSelection>();
                raceMenu.showOnStart = true; // Show on first spawn
            }

            Debug.Log("[CompleteGameSetup] ✓ Player spawned at " + playerSpawnPoint.position);
        }

        private void LoadWeaponShopItems(SimpleBuyMenu buyMenu)
        {
            // Load all generated weapons from Assets/Data/Weapons/
            WeaponData[] allWeapons = Resources.LoadAll<WeaponData>("Weapons");
            if (allWeapons.Length == 0)
            {
                // Try direct path
                allWeapons = UnityEngine.Resources.FindObjectsOfTypeAll<WeaponData>();
            }

            if (allWeapons.Length > 0)
            {
                WeaponShopItem[] shopItems = new WeaponShopItem[allWeapons.Length];
                for (int i = 0; i < allWeapons.Length; i++)
                {
                    shopItems[i] = new WeaponShopItem
                    {
                        weaponPrefab = allWeapons[i].weaponPrefab,
                        price = allWeapons[i].price
                    };
                }
                buyMenu.weaponsForSale = shopItems;
                Debug.Log($"[CompleteGameSetup] ✓ Loaded {shopItems.Length} weapons into buy menu");
            }
            else
            {
                Debug.LogWarning("[CompleteGameSetup] No weapons found! Run Game Builder first.");
            }
        }

        private void SpawnBots()
        {
            // Find bot spawn points
            if (botSpawnPoints == null || botSpawnPoints.Length == 0)
            {
                try
                {
                    GameObject[] spawns = GameObject.FindGameObjectsWithTag("BotSpawn");
                    if (spawns.Length > 0)
                    {
                        botSpawnPoints = new Transform[spawns.Length];
                        for (int i = 0; i < spawns.Length; i++)
                        {
                            botSpawnPoints[i] = spawns[i].transform;
                        }
                    }
                }
                catch (UnityException)
                {
                    // Tag doesn't exist, will create spawn points below
                }
                
                if (botSpawnPoints == null || botSpawnPoints.Length == 0)
                {
                    // Create default bot spawns (opposite side of map)
                    botSpawnPoints = new Transform[5];
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject spawnObj = new GameObject($"BotSpawn_{i}");
                        spawnObj.transform.position = new Vector3((i - 2) * 3, 2, 20);
                        botSpawnPoints[i] = spawnObj.transform;
                    }
                    Debug.Log("[CompleteGameSetup] Created default bot spawn points");
                }
            }

            // Spawn bots with delay
            bots = new GameObject[numberOfBots];
            StartCoroutine(SpawnBotsWithDelay());

            Debug.Log($"[CompleteGameSetup] ✓ Spawning {numberOfBots} bots...");
        }

        private System.Collections.IEnumerator SpawnBotsWithDelay()
        {
            for (int i = 0; i < numberOfBots; i++)
            {
                yield return new WaitForSeconds(botSpawnDelay);
                bots[i] = CreateBot(i);
            }
        }

        private GameObject CreateBot(int index)
        {
            int spawnIndex = index % botSpawnPoints.Length;
            Vector3 spawnPos = botSpawnPoints[spawnIndex].position;

            GameObject bot;
            if (botPrefab != null)
            {
                bot = Instantiate(botPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                // Create basic bot
                bot = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                bot.name = $"Bot_{index}";
                bot.tag = "Bot";
                bot.layer = LayerMask.NameToLayer("Default");

                // Set bot color to red
                Renderer renderer = bot.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.red;
                }

                // Add AI components
                bot.AddComponent<CharacterController>();
                var botComponent = bot.AddComponent<Bot>();
                var botAI = bot.AddComponent<BotAI>();
                bot.AddComponent<PlayerHealth>();
                bot.AddComponent<WeaponManager>();

                // Give bot a weapon
                WeaponData[] weapons = Resources.FindObjectsOfTypeAll<WeaponData>();
                if (weapons.Length > 0)
                {
                    // Give random weapon
                    WeaponData randomWeapon = weapons[Random.Range(0, weapons.Length)];
                    var weaponMgr = bot.GetComponent<WeaponManager>();
                    if (weaponMgr != null)
                    {
                        // weaponMgr.EquipWeapon(randomWeapon);
                    }
                }

                bot.transform.position = spawnPos;
                bot.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            return bot;
        }

        private void SetupBuyZones()
        {
            // Create buy zones at player spawn areas
            GameObject buyZone = GameObject.Find("BuyZone");
            if (buyZone == null && playerSpawnPoint != null)
            {
                buyZone = new GameObject("BuyZone");
                buyZone.transform.position = playerSpawnPoint.position;
                
                BoxCollider trigger = buyZone.AddComponent<BoxCollider>();
                trigger.isTrigger = true;
                trigger.size = new Vector3(15, 5, 15);
                
                buyZone.layer = LayerMask.NameToLayer("Default");
            }

            Debug.Log("[CompleteGameSetup] ✓ Buy zones created");
        }

        private void OnDrawGizmos()
        {
            // Draw spawn points
            if (playerSpawnPoint != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(playerSpawnPoint.position, 1f);
            }

            if (botSpawnPoints != null)
            {
                Gizmos.color = Color.red;
                foreach (Transform spawn in botSpawnPoints)
                {
                    if (spawn != null)
                    {
                        Gizmos.DrawWireSphere(spawn.position, 1f);
                    }
                }
            }
        }
    }
}
