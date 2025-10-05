using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace CS17.Editor
{
    /// <summary>
    /// Generates all weapon prefabs with primitive models
    /// </summary>
    public static class WeaponGenerator
    {
        private static string prefabPath = "Assets/Prefabs/Weapons/";
        private static string dataPath = "Assets/Data/Weapons/";

        public static void GenerateAllWeapons()
        {
            Debug.Log("[WeaponGenerator] Starting weapon generation...");

            // Ensure directories exist
            EnsureDirectoriesExist();

            // Generate weapons by category
            GeneratePistols();
            GenerateSMGs();
            GenerateRifles();
            GenerateSnipers();
            GenerateHeavy();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("[WeaponGenerator] ✅ All 20 weapons generated successfully!");
        }

        private static void EnsureDirectoriesExist()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/Weapons"))
            {
                AssetDatabase.CreateFolder("Assets/Prefabs", "Weapons");
            }
            if (!AssetDatabase.IsValidFolder("Assets/Data/Weapons"))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Data"))
                {
                    AssetDatabase.CreateFolder("Assets", "Data");
                }
                AssetDatabase.CreateFolder("Assets/Data", "Weapons");
            }
        }

        #region Pistols

        private static void GeneratePistols()
        {
            CreateWeapon("Glock-18", WeaponType.Pistol, 200, 26, 20, 2.0f, 0.88f, new Color(0.2f, 0.2f, 0.2f));
            CreateWeapon("USP-S", WeaponType.Pistol, 200, 35, 12, 2.5f, 1.2f, new Color(0.3f, 0.3f, 0.35f));
            CreateWeapon("Desert Eagle", WeaponType.Pistol, 700, 63, 7, 6.0f, 0.4f, new Color(0.7f, 0.6f, 0.2f));
            CreateWeapon("P250", WeaponType.Pistol, 300, 35, 13, 2.5f, 1.0f, new Color(0.25f, 0.25f, 0.3f));
            CreateWeapon("Five-Seven", WeaponType.Pistol, 500, 32, 20, 2.0f, 1.1f, new Color(0.1f, 0.1f, 0.15f));
        }

        #endregion

        #region SMGs

        private static void GenerateSMGs()
        {
            CreateWeapon("MP5-SD", WeaponType.SMG, 1500, 27, 30, 2.12f, 0.80f, new Color(0.15f, 0.15f, 0.2f));
            CreateWeapon("MP7", WeaponType.SMG, 1500, 29, 30, 2.63f, 0.80f, new Color(0.2f, 0.25f, 0.2f));
            CreateWeapon("UMP-45", WeaponType.SMG, 1200, 35, 25, 2.5f, 0.67f, new Color(0.3f, 0.3f, 0.25f));
            CreateWeapon("P90", WeaponType.SMG, 2350, 26, 50, 3.0f, 0.86f, new Color(0.25f, 0.3f, 0.35f));
            CreateWeapon("MAC-10", WeaponType.SMG, 1050, 29, 30, 3.0f, 0.75f, new Color(0.15f, 0.15f, 0.15f));
        }

        #endregion

        #region Rifles

        private static void GenerateRifles()
        {
            CreateWeapon("AK-47", WeaponType.Rifle, 2700, 36, 30, 2.45f, 0.67f, new Color(0.4f, 0.3f, 0.2f));
            CreateWeapon("M4A4", WeaponType.Rifle, 3100, 33, 30, 3.1f, 0.67f, new Color(0.2f, 0.2f, 0.25f));
            CreateWeapon("Galil AR", WeaponType.Rifle, 2000, 30, 35, 2.5f, 0.67f, new Color(0.35f, 0.3f, 0.25f));
            CreateWeapon("FAMAS", WeaponType.Rifle, 2250, 30, 25, 3.3f, 0.90f, new Color(0.25f, 0.3f, 0.25f));
            CreateWeapon("AUG", WeaponType.Rifle, 3300, 33, 30, 3.0f, 0.67f, new Color(0.35f, 0.4f, 0.35f));
        }

        #endregion

        #region Snipers

        private static void GenerateSnipers()
        {
            CreateWeapon("AWP", WeaponType.Sniper, 4750, 115, 10, 1.0f, 0.41f, new Color(0.2f, 0.4f, 0.3f));
            CreateWeapon("Scout", WeaponType.Sniper, 1700, 75, 10, 1.3f, 0.56f, new Color(0.3f, 0.35f, 0.25f));
            CreateWeapon("G3SG1", WeaponType.Sniper, 5000, 80, 20, 1.25f, 0.24f, new Color(0.25f, 0.25f, 0.2f));
        }

        #endregion

        #region Heavy

        private static void GenerateHeavy()
        {
            CreateWeapon("M249", WeaponType.Shotgun, 5200, 32, 100, 5.7f, 0.75f, new Color(0.4f, 0.35f, 0.25f));
            CreateWeapon("Nova", WeaponType.Shotgun, 1050, 26, 8, 1.0f, 0.88f, new Color(0.3f, 0.25f, 0.2f));
        }

        #endregion

        private static void CreateWeapon(string weaponName, WeaponType type, int price, float damage, 
            int magazineSize, float reloadTime, float fireRate, Color weaponColor)
        {
            // Create WeaponData ScriptableObject
            WeaponData data = ScriptableObject.CreateInstance<WeaponData>();
            data.weaponName = weaponName;
            data.weaponType = type;
            data.damage = damage;
            data.headshotMultiplier = 4f;
            data.fireRate = fireRate;
            data.magazineSize = magazineSize;
            data.reserveAmmo = magazineSize * 3;
            data.reloadTime = reloadTime;
            data.range = type == WeaponType.Sniper ? 500f : type == WeaponType.Rifle ? 300f : 200f;
            data.recoilAmount = damage / 30f;
            data.price = price;
            data.killReward = 300;

            // Save WeaponData
            string dataFilePath = dataPath + weaponName.Replace("-", "_").Replace(" ", "_") + ".asset";
            AssetDatabase.CreateAsset(data, dataFilePath);

            // Create weapon prefab
            GameObject weaponObj = new GameObject(weaponName);
            
            // Add Weapon component
            var weapon = weaponObj.AddComponent<Weapon>();
            
            // Create visual model from primitives
            GameObject model = CreateWeaponModel(weaponName, type, weaponColor);
            model.transform.SetParent(weaponObj.transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;

            // Create muzzle flash point
            GameObject muzzle = new GameObject("MuzzlePoint");
            muzzle.transform.SetParent(weaponObj.transform);
            muzzle.transform.localPosition = GetMuzzlePosition(type);

            // Save as prefab
            string prefabFilePath = prefabPath + weaponName.Replace("-", "_").Replace(" ", "_") + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(weaponObj, prefabFilePath);
            
            // Clean up scene object
            Object.DestroyImmediate(weaponObj);

            Debug.Log($"[WeaponGenerator] ✓ Created {weaponName} ({type})");
        }

        private static GameObject CreateWeaponModel(string name, WeaponType type, Color color)
        {
            GameObject model = new GameObject("Model");

            switch (type)
            {
                case WeaponType.Pistol:
                    CreatePistolModel(model, color);
                    break;
                case WeaponType.SMG:
                    CreateSMGModel(model, color);
                    break;
                case WeaponType.Rifle:
                    CreateRifleModel(model, color);
                    break;
                case WeaponType.Sniper:
                    CreateSniperModel(model, color);
                    break;
                case WeaponType.Shotgun:
                    CreateHeavyModel(model, color);
                    break;
            }

            return model;
        }

        private static void CreatePistolModel(GameObject parent, Color color)
        {
            // Body
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0, 0.2f), 
                new Vector3(0.08f, 0.12f, 0.3f), color);
            // Barrel
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, 0.04f, 0.45f), 
                new Vector3(0.03f, 0.15f, 0.03f), color * 0.8f, new Vector3(90, 0, 0));
            // Grip
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, -0.05f, -0.05f), 
                new Vector3(0.05f, 0.15f, 0.08f), color * 0.6f);
        }

        private static void CreateSMGModel(GameObject parent, Color color)
        {
            // Body
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0, 0.2f), 
                new Vector3(0.1f, 0.15f, 0.5f), color);
            // Barrel
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, 0.05f, 0.6f), 
                new Vector3(0.035f, 0.2f, 0.035f), color * 0.8f, new Vector3(90, 0, 0));
            // Stock
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0, -0.15f), 
                new Vector3(0.08f, 0.12f, 0.15f), color * 0.9f);
            // Magazine
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, -0.12f, 0.1f), 
                new Vector3(0.05f, 0.12f, 0.08f), color * 0.7f);
        }

        private static void CreateRifleModel(GameObject parent, Color color)
        {
            // Body
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0, 0.3f), 
                new Vector3(0.12f, 0.16f, 0.7f), color);
            // Barrel
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, 0.05f, 0.85f), 
                new Vector3(0.04f, 0.25f, 0.04f), color * 0.8f, new Vector3(90, 0, 0));
            // Stock
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0.02f, -0.2f), 
                new Vector3(0.1f, 0.14f, 0.3f), color * 0.9f);
            // Magazine
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, -0.14f, 0.2f), 
                new Vector3(0.06f, 0.15f, 0.1f), color * 0.7f);
            // Handguard
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, -0.04f, 0.6f), 
                new Vector3(0.09f, 0.08f, 0.3f), color * 0.85f);
        }

        private static void CreateSniperModel(GameObject parent, Color color)
        {
            // Body
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0, 0.4f), 
                new Vector3(0.12f, 0.18f, 0.9f), color);
            // Long Barrel
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, 0.06f, 1.1f), 
                new Vector3(0.045f, 0.35f, 0.045f), color * 0.8f, new Vector3(90, 0, 0));
            // Scope
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, 0.15f, 0.4f), 
                new Vector3(0.05f, 0.2f, 0.05f), new Color(0.1f, 0.1f, 0.15f), new Vector3(0, 0, 90));
            // Stock
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0.02f, -0.3f), 
                new Vector3(0.1f, 0.15f, 0.4f), color * 0.9f);
            // Bipod
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, -0.12f, 0.7f), 
                new Vector3(0.15f, 0.02f, 0.08f), color * 0.6f);
        }

        private static void CreateHeavyModel(GameObject parent, Color color)
        {
            // Large body
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0, 0.3f), 
                new Vector3(0.15f, 0.2f, 0.8f), color);
            // Thick barrel
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, 0.08f, 0.9f), 
                new Vector3(0.06f, 0.3f, 0.06f), color * 0.8f, new Vector3(90, 0, 0));
            // Stock
            CreatePrimitive(parent, PrimitiveType.Cube, new Vector3(0, 0.03f, -0.25f), 
                new Vector3(0.12f, 0.16f, 0.35f), color * 0.9f);
            // Large magazine/drum
            CreatePrimitive(parent, PrimitiveType.Cylinder, new Vector3(0, -0.15f, 0.2f), 
                new Vector3(0.12f, 0.08f, 0.12f), color * 0.7f, new Vector3(90, 0, 0));
        }

        private static void CreatePrimitive(GameObject parent, PrimitiveType type, Vector3 position, 
            Vector3 scale, Color color, Vector3 rotation = default)
        {
            GameObject obj = GameObject.CreatePrimitive(type);
            obj.transform.SetParent(parent.transform);
            obj.transform.localPosition = position;
            obj.transform.localScale = scale;
            if (rotation != default)
            {
                obj.transform.localRotation = Quaternion.Euler(rotation);
            }

            // Remove collider (weapon uses raycast, not collision)
            Object.DestroyImmediate(obj.GetComponent<Collider>());

            // Apply material
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = color;
            obj.GetComponent<Renderer>().material = mat;
        }

        private static Vector3 GetMuzzlePosition(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.Pistol: return new Vector3(0, 0.04f, 0.6f);
                case WeaponType.SMG: return new Vector3(0, 0.05f, 0.8f);
                case WeaponType.Rifle: return new Vector3(0, 0.05f, 1.1f);
                case WeaponType.Sniper: return new Vector3(0, 0.06f, 1.45f);
                case WeaponType.Heavy: return new Vector3(0, 0.08f, 1.2f);
                default: return Vector3.forward;
            }
        }
    }
}
