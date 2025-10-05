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
            
            var healthDisplay = healthObj.AddComponent<SimpleHealthDisplay>();
            
            RectTransform healthRect = healthObj.GetComponent<RectTransform>();
            healthRect.anchorMin = new Vector2(0, 0);
            healthRect.anchorMax = new Vector2(0, 0);
            healthRect.pivot = new Vector2(0, 0);
            healthRect.anchoredPosition = new Vector2(20, 20);
            healthRect.sizeDelta = new Vector2(200, 100);

            // Money Display (top-left) - will be created by SimpleBuyMenu
            
            // Ammo Display (bottom-right) - will be created by weapon system
        }

        private void SpawnPlayer()
        {
            // Find or create player spawn point
            if (playerSpawnPoint == null)
            {
                GameObject[] spawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
                if (spawns.Length > 0)
                {
                    playerSpawnPoint = spawns[0].transform;
                }
                else
                {
                    // Create default spawn
                    GameObject spawnObj = new GameObject("PlayerSpawn");
                    spawnObj.tag = "PlayerSpawn";
                    playerSpawnPoint = spawnObj.transform;
                    playerSpawnPoint.position = new Vector3(0, 2, -20);
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
                    camObj.AddComponent<AudioListener>();
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
                        weaponData = allWeapons[i],
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
                GameObject[] spawns = GameObject.FindGameObjectsWithTag("BotSpawn");
                if (spawns.Length > 0)
                {
                    botSpawnPoints = new Transform[spawns.Length];
                    for (int i = 0; i < spawns.Length; i++)
                    {
                        botSpawnPoints[i] = spawns[i].transform;
                    }
                }
                else
                {
                    // Create default bot spawns (opposite side of map)
                    botSpawnPoints = new Transform[5];
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject spawnObj = new GameObject($"BotSpawn_{i}");
                        spawnObj.tag = "BotSpawn";
                        spawnObj.transform.position = new Vector3((i - 2) * 3, 2, 20);
                        botSpawnPoints[i] = spawnObj.transform;
                    }
                }
            }

            // Spawn bots with delay
            bots = new GameObject[numberOfBots];
            for (int i = 0; i < numberOfBots; i++)
            {
                int spawnIndex = i % botSpawnPoints.Length;
                Invoke(nameof(SpawnSingleBot) + i, botSpawnDelay * i);
            }

            Debug.Log($"[CompleteGameSetup] ✓ Spawning {numberOfBots} bots...");
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
                var botController = bot.AddComponent<BotController>();
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

        // Dynamic spawn methods for Invoke
        private void SpawnSingleBot0() { bots[0] = CreateBot(0); }
        private void SpawnSingleBot1() { bots[1] = CreateBot(1); }
        private void SpawnSingleBot2() { bots[2] = CreateBot(2); }
        private void SpawnSingleBot3() { bots[3] = CreateBot(3); }
        private void SpawnSingleBot4() { bots[4] = CreateBot(4); }

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
