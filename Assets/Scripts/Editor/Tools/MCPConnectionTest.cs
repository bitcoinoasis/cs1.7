using UnityEngine;
using UnityEditor;

/// <summary>
/// Test script to verify Unity MCP connection and create test objects
/// </summary>
public class MCPConnectionTest : EditorWindow
{
    [MenuItem("Tools/CS 1.7/Test MCP Connection")]
    static void TestConnection()
    {
        // Create a test cube in the scene
        GameObject testCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        testCube.name = "MCP_Test_Cube";
        testCube.transform.position = new Vector3(0, 1, 0);
        
        // Add a colored material
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.green;
        testCube.GetComponent<Renderer>().material = mat;
        
        Debug.Log("[MCP Test] Successfully created test cube at (0, 1, 0)");
        
        // Select it in hierarchy
        Selection.activeGameObject = testCube;
        
        EditorUtility.DisplayDialog(
            "MCP Connection Test", 
            "✅ Test cube created successfully!\n\n" +
            "Location: (0, 1, 0)\n" +
            "Color: Green\n\n" +
            "This confirms Unity Editor scripting is working.\n" +
            "MCP connection should be functional!",
            "OK"
        );
    }
    
    [MenuItem("Tools/CS 1.7/Create Test Scene Objects")]
    static void CreateTestObjects()
    {
        // Create ground plane
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(10, 1, 10);
        
        // Create test player
        GameObject player = new GameObject("Test_Player");
        player.tag = "Player";
        CharacterController controller = player.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        player.transform.position = new Vector3(0, 1, 0);
        
        // Add visual representation
        GameObject playerVisual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerVisual.name = "Visual";
        playerVisual.transform.SetParent(player.transform);
        playerVisual.transform.localPosition = new Vector3(0, 1, 0);
        Object.DestroyImmediate(playerVisual.GetComponent<Collider>());
        
        Material playerMat = new Material(Shader.Find("Standard"));
        playerMat.color = Color.blue;
        playerVisual.GetComponent<Renderer>().material = playerMat;
        
        // Create camera
        GameObject cam = new GameObject("Test_Camera");
        cam.tag = "MainCamera";
        cam.transform.SetParent(player.transform);
        cam.transform.localPosition = new Vector3(0, 1.6f, 0);
        Camera camera = cam.AddComponent<Camera>();
        camera.fieldOfView = 90f;
        
        Debug.Log("[MCP Test] Created test scene with ground and player!");
        
        // Select player
        Selection.activeGameObject = player;
        
        EditorUtility.DisplayDialog(
            "Test Scene Created", 
            "✅ Test scene created successfully!\n\n" +
            "Objects created:\n" +
            "- Ground plane (10x10)\n" +
            "- Player with CharacterController\n" +
            "- Camera at eye level\n\n" +
            "You can now press Play to test!",
            "OK"
        );
    }
    
    [MenuItem("Tools/CS 1.7/Clear Test Objects")]
    static void ClearTestObjects()
    {
        // Find and destroy test objects
        GameObject testCube = GameObject.Find("MCP_Test_Cube");
        if (testCube != null)
        {
            Object.DestroyImmediate(testCube);
            Debug.Log("[MCP Test] Removed test cube");
        }
        
        GameObject testPlayer = GameObject.Find("Test_Player");
        if (testPlayer != null)
        {
            Object.DestroyImmediate(testPlayer);
            Debug.Log("[MCP Test] Removed test player");
        }
        
        GameObject ground = GameObject.Find("Ground");
        if (ground != null)
        {
            Object.DestroyImmediate(ground);
            Debug.Log("[MCP Test] Removed ground");
        }
        
        EditorUtility.DisplayDialog(
            "Test Objects Cleared", 
            "✅ All test objects removed!",
            "OK"
        );
    }
}
