using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace CS17.Editor
{
    /// <summary>
    /// Generates test map with spawn points, cover, and objectives
    /// </summary>
    public static class MapGenerator
    {
        public static void GenerateTestMap()
        {
            Debug.Log("[MapGenerator] Creating test map...");

            // Check if TestMap already exists
            GameObject existingMap = GameObject.Find("TestMap");
            if (existingMap != null)
            {
                Debug.LogWarning("[MapGenerator] TestMap already exists! Deleting old one...");
                Object.DestroyImmediate(existingMap);
            }

            // Create map parent
            GameObject map = new GameObject("TestMap");

            // Generate ground
            CreateGround(map.transform);

            // Generate spawn points
            CreateSpawnPoints(map.transform);

            // Generate cover objects
            CreateCoverObjects(map.transform);

            // Generate bomb sites
            CreateBombSites(map.transform);

            // Generate walls
            CreateWalls(map.transform);

            Debug.Log("[MapGenerator] ✅ Test map created successfully!");

            // Select the map in hierarchy
            Selection.activeGameObject = map;

            EditorUtility.DisplayDialog("Map Generated!", 
                "Test map created successfully!\n\n" +
                "Features:\n" +
                "• 50x50 ground plane\n" +
                "• 10 spawn points (5 per team)\n" +
                "• Cover objects\n" +
                "• Bomb sites A & B\n" +
                "• Boundary walls\n\n" +
                "Next: Bake NavMesh\n" +
                "(Window → AI → Navigation)", 
                "OK");
        }

        private static void CreateGround(Transform parent)
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.SetParent(parent);
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = new Vector3(5, 1, 5); // 50x50 units

            // Apply material
            Material groundMat = new Material(Shader.Find("Standard"));
            groundMat.color = new Color(0.3f, 0.35f, 0.3f);
            ground.GetComponent<Renderer>().material = groundMat;

            // Set layer for NavMesh
            ground.layer = LayerMask.NameToLayer("Default");

            // Make it static for NavMesh baking
            ground.isStatic = true;

            Debug.Log("[MapGenerator] ✓ Created ground (50x50)");
        }

        private static void CreateSpawnPoints(Transform parent)
        {
            GameObject spawnParent = new GameObject("SpawnPoints");
            spawnParent.transform.SetParent(parent);

            // Team 1 (Terrorist) spawns
            GameObject t1Parent = new GameObject("Team1_Spawns");
            t1Parent.transform.SetParent(spawnParent.transform);

            for (int i = 0; i < 5; i++)
            {
                GameObject spawn = new GameObject($"T_Spawn_{i + 1}");
                spawn.transform.SetParent(t1Parent.transform);
                spawn.transform.position = new Vector3(-15 + (i * 2), 1, -20);
                
                // Add gizmo visual
                var gizmo = spawn.AddComponent<SpawnPointGizmo>();
                gizmo.teamColor = new Color(1f, 0.5f, 0f); // Orange
            }

            // Team 2 (Counter-Terrorist) spawns
            GameObject t2Parent = new GameObject("Team2_Spawns");
            t2Parent.transform.SetParent(spawnParent.transform);

            for (int i = 0; i < 5; i++)
            {
                GameObject spawn = new GameObject($"CT_Spawn_{i + 1}");
                spawn.transform.SetParent(t2Parent.transform);
                spawn.transform.position = new Vector3(-15 + (i * 2), 1, 20);
                
                // Add gizmo visual
                var gizmo = spawn.AddComponent<SpawnPointGizmo>();
                gizmo.teamColor = new Color(0f, 0.5f, 1f); // Blue
            }

            Debug.Log("[MapGenerator] ✓ Created 10 spawn points");
        }

        private static void CreateCoverObjects(Transform parent)
        {
            GameObject coverParent = new GameObject("Cover");
            coverParent.transform.SetParent(parent);

            // Create various cover boxes
            CreateCoverBox(coverParent.transform, new Vector3(-10, 0.5f, -5), new Vector3(2, 1, 2), "Cover_1");
            CreateCoverBox(coverParent.transform, new Vector3(10, 0.5f, -5), new Vector3(2, 1, 2), "Cover_2");
            CreateCoverBox(coverParent.transform, new Vector3(-10, 0.5f, 5), new Vector3(2, 1, 2), "Cover_3");
            CreateCoverBox(coverParent.transform, new Vector3(10, 0.5f, 5), new Vector3(2, 1, 2), "Cover_4");
            CreateCoverBox(coverParent.transform, new Vector3(0, 0.75f, 0), new Vector3(3, 1.5f, 3), "Cover_Center");

            // Create some walls for cover
            CreateCoverBox(coverParent.transform, new Vector3(-5, 1, -10), new Vector3(4, 2, 0.5f), "Wall_1");
            CreateCoverBox(coverParent.transform, new Vector3(5, 1, 10), new Vector3(4, 2, 0.5f), "Wall_2");

            Debug.Log("[MapGenerator] ✓ Created cover objects");
        }

        private static void CreateCoverBox(Transform parent, Vector3 position, Vector3 scale, string name)
        {
            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.name = name;
            box.transform.SetParent(parent);
            box.transform.position = position;
            box.transform.localScale = scale;

            // Apply material
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(0.5f, 0.4f, 0.3f);
            box.GetComponent<Renderer>().material = mat;

            // Make static for NavMesh
            box.isStatic = true;
        }

        private static void CreateBombSites(Transform parent)
        {
            GameObject sitesParent = new GameObject("BombSites");
            sitesParent.transform.SetParent(parent);

            // Site A
            GameObject siteA = CreateBombSite(sitesParent.transform, new Vector3(-15, 0.1f, 10), "BombSite_A");
            var siteAComp = siteA.AddComponent<BombSiteMarker>();
            siteAComp.siteName = "A";

            // Site B
            GameObject siteB = CreateBombSite(sitesParent.transform, new Vector3(15, 0.1f, -10), "BombSite_B");
            var siteBComp = siteB.AddComponent<BombSiteMarker>();
            siteBComp.siteName = "B";

            Debug.Log("[MapGenerator] ✓ Created bomb sites A & B");
        }

        private static GameObject CreateBombSite(Transform parent, Vector3 position, string name)
        {
            GameObject site = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            site.name = name;
            site.transform.SetParent(parent);
            site.transform.position = position;
            site.transform.localScale = new Vector3(5, 0.1f, 5);

            // Remove collider, use trigger instead
            Object.DestroyImmediate(site.GetComponent<Collider>());
            
            // Add box collider as trigger
            BoxCollider trigger = site.AddComponent<BoxCollider>();
            trigger.isTrigger = true;
            trigger.size = new Vector3(1, 2, 1);
            trigger.center = new Vector3(0, 1, 0);

            // Apply material
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(1f, 0.3f, 0.3f, 0.5f);
            mat.SetFloat("_Mode", 3); // Transparent
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            site.GetComponent<Renderer>().material = mat;

            return site;
        }

        private static void CreateWalls(Transform parent)
        {
            GameObject wallsParent = new GameObject("Walls");
            wallsParent.transform.SetParent(parent);

            // Boundary walls
            CreateWall(wallsParent.transform, new Vector3(0, 2, 25), new Vector3(50, 4, 1), "Wall_North");
            CreateWall(wallsParent.transform, new Vector3(0, 2, -25), new Vector3(50, 4, 1), "Wall_South");
            CreateWall(wallsParent.transform, new Vector3(25, 2, 0), new Vector3(1, 4, 50), "Wall_East");
            CreateWall(wallsParent.transform, new Vector3(-25, 2, 0), new Vector3(1, 4, 50), "Wall_West");

            Debug.Log("[MapGenerator] ✓ Created boundary walls");
        }

        private static void CreateWall(Transform parent, Vector3 position, Vector3 scale, string name)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = name;
            wall.transform.SetParent(parent);
            wall.transform.position = position;
            wall.transform.localScale = scale;

            // Apply material
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(0.4f, 0.4f, 0.45f);
            wall.GetComponent<Renderer>().material = mat;

            // Make static for NavMesh
            wall.isStatic = true;
        }
    }

    /// <summary>
    /// Component to visualize spawn points in scene
    /// </summary>
    public class SpawnPointGizmo : MonoBehaviour
    {
        public Color teamColor = Color.white;

        private void OnDrawGizmos()
        {
            Gizmos.color = teamColor;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }

    /// <summary>
    /// Component to mark bomb sites
    /// </summary>
    public class BombSiteMarker : MonoBehaviour
    {
        public string siteName = "A";

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.3f);
            Gizmos.DrawCube(transform.position + Vector3.up, new Vector3(5, 2, 5));
        }
    }
}
