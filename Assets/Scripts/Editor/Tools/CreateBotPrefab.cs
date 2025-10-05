using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

/// <summary>
/// Creates a complete Bot prefab with all required components
/// </summary>
public class CreateBotPrefab : EditorWindow
{
    [MenuItem("Tools/CS 1.7/Create Bot Prefab")]
    static void CreateBot()
    {
        // Create bot GameObject
        GameObject bot = new GameObject("Bot");
        
        // Add CharacterController
        CharacterController controller = bot.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = new Vector3(0, 1, 0);
        
        // Add NavMeshAgent
        NavMeshAgent agent = bot.AddComponent<NavMeshAgent>();
        agent.speed = 5f;
        agent.acceleration = 8f;
        agent.angularSpeed = 120f;
        agent.stoppingDistance = 2f;
        agent.height = 2f;
        agent.radius = 0.5f;
        
        // Add all gameplay components
        bot.AddComponent<BotAI>();
        bot.AddComponent<PlayerHealth>();
        bot.AddComponent<PlayerController>();
        bot.AddComponent<PlayerRace>();
        bot.AddComponent<AbilitySystem>();
        
        // Create visual representation
        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        visual.name = "Visual";
        visual.transform.SetParent(bot.transform);
        visual.transform.localPosition = new Vector3(0, 1, 0);
        
        // Remove the collider (CharacterController handles collision)
        Object.DestroyImmediate(visual.GetComponent<Collider>());
        
        // Add colored material
        Material botMat = new Material(Shader.Find("Standard"));
        botMat.color = new Color(1f, 0.5f, 0f); // Orange
        visual.GetComponent<Renderer>().material = botMat;
        
        // Create weapon holder
        GameObject weaponHolder = new GameObject("WeaponHolder");
        weaponHolder.transform.SetParent(bot.transform);
        weaponHolder.transform.localPosition = new Vector3(0.3f, 1.5f, 0.5f);
        
        // Position bot at origin
        bot.transform.position = new Vector3(0, 0, 0);
        
        // Create Prefabs folder if it doesn't exist
        string prefabFolder = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(prefabFolder))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        
        // Save as prefab
        string prefabPath = prefabFolder + "/Bot.prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(bot, prefabPath);
        
        // Select the prefab in project
        Selection.activeObject = prefab;
        EditorGUIUtility.PingObject(prefab);
        
        // Destroy the scene instance (prefab is saved)
        Object.DestroyImmediate(bot);
        
        Debug.Log($"[Bot Prefab] Created and saved at: {prefabPath}");
        
        EditorUtility.DisplayDialog(
            "Bot Prefab Created!", 
            "✅ Bot prefab created successfully!\n\n" +
            "Location: Assets/Prefabs/Bot.prefab\n\n" +
            "Components Added:\n" +
            "• CharacterController (height: 2, radius: 0.5)\n" +
            "• NavMeshAgent (speed: 5, acceleration: 8)\n" +
            "• BotAI\n" +
            "• PlayerHealth\n" +
            "• PlayerController\n" +
            "• PlayerRace\n" +
            "• AbilitySystem\n" +
            "• Visual capsule (orange)\n" +
            "• WeaponHolder\n\n" +
            "The prefab is ready to use with BotSpawner!",
            "Awesome!"
        );
    }
    
    [MenuItem("Tools/CS 1.7/Create Player Prefab")]
    static void CreatePlayerPrefab()
    {
        // Create player GameObject
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        
        // Add CharacterController
        CharacterController controller = player.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = new Vector3(0, 1, 0);
        
        // Add all gameplay components
        player.AddComponent<PlayerController>();
        player.AddComponent<PlayerHealth>();
        player.AddComponent<PlayerRace>();
        player.AddComponent<AbilitySystem>();
        
        // Create camera
        GameObject cam = new GameObject("Main Camera");
        cam.tag = "MainCamera";
        cam.transform.SetParent(player.transform);
        cam.transform.localPosition = new Vector3(0, 1.6f, 0);
        
        Camera camera = cam.AddComponent<Camera>();
        camera.fieldOfView = 90f;
        cam.AddComponent<AudioListener>();
        
        // Create weapon holder
        GameObject weaponHolder = new GameObject("WeaponHolder");
        weaponHolder.transform.SetParent(cam.transform);
        weaponHolder.transform.localPosition = new Vector3(0.3f, -0.3f, 0.5f);
        
        // Position player
        player.transform.position = new Vector3(0, 1, 0);
        
        // Create Prefabs folder if it doesn't exist
        string prefabFolder = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(prefabFolder))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        
        // Save as prefab
        string prefabPath = prefabFolder + "/Player.prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(player, prefabPath);
        
        // Select the prefab in project
        Selection.activeObject = prefab;
        EditorGUIUtility.PingObject(prefab);
        
        // Destroy the scene instance
        Object.DestroyImmediate(player);
        
        Debug.Log($"[Player Prefab] Created and saved at: {prefabPath}");
        
        EditorUtility.DisplayDialog(
            "Player Prefab Created!", 
            "✅ Player prefab created successfully!\n\n" +
            "Location: Assets/Prefabs/Player.prefab\n\n" +
            "Components Added:\n" +
            "• CharacterController\n" +
            "• PlayerController\n" +
            "• PlayerHealth\n" +
            "• PlayerRace\n" +
            "• AbilitySystem\n" +
            "• Camera with AudioListener\n" +
            "• WeaponHolder\n\n" +
            "Ready to spawn in your game!",
            "Perfect!"
        );
    }
}
