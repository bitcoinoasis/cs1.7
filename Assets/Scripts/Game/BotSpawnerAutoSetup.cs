// 2025-10-04 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

/// <summary>
/// Automatically loads and assigns bot prefab and race/weapon data when BotSpawner starts
/// </summary>
public class BotSpawnerAutoSetup : MonoBehaviour
{
    private void Start()
    {
        BotSpawner spawner = GetComponent<BotSpawner>();
        if (spawner == null) return;

        // Auto-assign bot prefab if not set
        if (spawner.botPrefab == null)
        {
            // Try to load from Resources
            GameObject botPrefab = Resources.Load<GameObject>("Prefabs/Bot");
            
            if (botPrefab == null)
            {
                Debug.LogWarning("[BotSpawnerAutoSetup] Bot prefab not found in Resources/Prefabs/Bot. Creating runtime bot prefab...");
                spawner.botPrefab = CreateRuntimeBotPrefab();
            }
            else
            {
                spawner.botPrefab = botPrefab;
                Debug.Log("[BotSpawnerAutoSetup] Bot prefab loaded from Resources");
            }
        }

        // Verify race data is available
        RaceData[] races = Resources.LoadAll<RaceData>("Data/Races");
        if (races.Length > 0)
        {
            Debug.Log($"[BotSpawnerAutoSetup] Found {races.Length} race data assets");
        }
        else
        {
            Debug.LogWarning("[BotSpawnerAutoSetup] No race data found in Resources/Data/Races!");
        }

        // Verify weapon data is available
        WeaponData[] weapons = Resources.LoadAll<WeaponData>("Data/Weapons");
        if (weapons.Length > 0)
        {
            Debug.Log($"[BotSpawnerAutoSetup] Found {weapons.Length} weapon data assets");
        }
        else
        {
            Debug.LogWarning("[BotSpawnerAutoSetup] No weapon data found in Resources/Data/Weapons!");
        }
    }

    private GameObject CreateRuntimeBotPrefab()
    {
        GameObject botPrefab = new GameObject("Bot");

        // Add character controller
        CharacterController controller = botPrefab.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = new Vector3(0, 1, 0);

        // Add AI and gameplay components
        botPrefab.AddComponent<BotAI>();
        botPrefab.AddComponent<PlayerHealth>();
        botPrefab.AddComponent<PlayerController>();
        botPrefab.AddComponent<PlayerRace>();
        botPrefab.AddComponent<AbilitySystem>();

        // Add NavMeshAgent for AI navigation
        UnityEngine.AI.NavMeshAgent navAgent = botPrefab.AddComponent<UnityEngine.AI.NavMeshAgent>();
        navAgent.height = 2f;
        navAgent.radius = 0.5f;
        navAgent.speed = 5f;
        navAgent.acceleration = 8f;

        // Add visual representation (simple capsule)
        GameObject visualObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        visualObj.name = "Visual";
        visualObj.transform.SetParent(botPrefab.transform);
        visualObj.transform.localPosition = new Vector3(0, 1, 0);
        
        // Remove the collider from visual (CharacterController handles collision)
        Destroy(visualObj.GetComponent<Collider>());

        Debug.Log("[BotSpawnerAutoSetup] Created runtime bot prefab");
        return botPrefab;
    }
}
