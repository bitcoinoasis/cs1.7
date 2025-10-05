using UnityEngine;
using UnityEditor;

namespace CS17.Editor
{
    /// <summary>
    /// Utility to clean up duplicate objects in the scene
    /// </summary>
    public static class SceneCleanup
    {
        [MenuItem("CS1.7/Utils/Clean Up Duplicates")]
        public static void CleanUpDuplicates()
        {
            int removed = 0;

            // Find and remove duplicate TestMaps (keep only the first one)
            GameObject[] testMaps = GameObject.FindObjectsOfType<GameObject>();
            GameObject firstTestMap = null;
            
            foreach (GameObject obj in testMaps)
            {
                if (obj.name == "TestMap")
                {
                    if (firstTestMap == null)
                    {
                        firstTestMap = obj;
                        Debug.Log($"[SceneCleanup] Keeping first TestMap: {obj.name}");
                    }
                    else
                    {
                        Debug.Log($"[SceneCleanup] Removing duplicate: {obj.name}");
                        Object.DestroyImmediate(obj);
                        removed++;
                    }
                }
            }

            // Find and remove duplicate GameSetup objects
            GameObject[] gameSetups = GameObject.FindObjectsOfType<GameObject>();
            GameObject firstGameSetup = null;
            
            foreach (GameObject obj in gameSetups)
            {
                if (obj.name == "GameSetup")
                {
                    if (firstGameSetup == null)
                    {
                        firstGameSetup = obj;
                        Debug.Log($"[SceneCleanup] Keeping first GameSetup: {obj.name}");
                    }
                    else
                    {
                        Debug.Log($"[SceneCleanup] Removing duplicate: {obj.name}");
                        Object.DestroyImmediate(obj);
                        removed++;
                    }
                }
            }

            // Find and remove duplicate Canvases
            Canvas[] canvases = Object.FindObjectsOfType<Canvas>();
            if (canvases.Length > 1)
            {
                for (int i = 1; i < canvases.Length; i++)
                {
                    Debug.Log($"[SceneCleanup] Removing duplicate Canvas: {canvases[i].name}");
                    Object.DestroyImmediate(canvases[i].gameObject);
                    removed++;
                }
            }

            // Find and remove duplicate Main Cameras
            Camera[] cameras = Object.FindObjectsOfType<Camera>();
            Camera mainCam = null;
            
            foreach (Camera cam in cameras)
            {
                if (cam.CompareTag("MainCamera"))
                {
                    if (mainCam == null)
                    {
                        mainCam = cam;
                    }
                    else
                    {
                        Debug.Log($"[SceneCleanup] Removing duplicate Main Camera: {cam.name}");
                        Object.DestroyImmediate(cam.gameObject);
                        removed++;
                    }
                }
            }

            EditorUtility.DisplayDialog("Scene Cleanup Complete", 
                $"Removed {removed} duplicate objects.\n\n" +
                "Your scene is now clean!\n" +
                "Save the scene to keep changes.", 
                "OK");

            Debug.Log($"[SceneCleanup] ✅ Cleanup complete! Removed {removed} duplicates.");
        }

        [MenuItem("CS1.7/Utils/List All Scene Objects")]
        public static void ListAllSceneObjects()
        {
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
            
            Debug.Log($"[SceneCleanup] === Scene Objects ({allObjects.Length} total) ===");
            
            // Group by name
            System.Collections.Generic.Dictionary<string, int> objectCounts = 
                new System.Collections.Generic.Dictionary<string, int>();
            
            foreach (GameObject obj in allObjects)
            {
                // Only count root objects (no parent or parent is not a prefab)
                if (obj.transform.parent == null || obj.transform.parent.name == "")
                {
                    string name = obj.name;
                    if (objectCounts.ContainsKey(name))
                    {
                        objectCounts[name]++;
                    }
                    else
                    {
                        objectCounts[name] = 1;
                    }
                }
            }

            foreach (var kvp in objectCounts)
            {
                if (kvp.Value > 1)
                {
                    Debug.LogWarning($"[SceneCleanup] ⚠️ {kvp.Key}: {kvp.Value} copies (DUPLICATE!)");
                }
                else
                {
                    Debug.Log($"[SceneCleanup] ✓ {kvp.Key}: {kvp.Value}");
                }
            }
        }
    }
}
