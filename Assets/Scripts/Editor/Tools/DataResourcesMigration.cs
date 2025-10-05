// 2025-10-04 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Editor utility to ensure all ScriptableObject data is in Resources folders for runtime access
/// </summary>
public class DataResourcesMigration : Editor
{
    [MenuItem("Tools/CS 1.7/Setup Resources Folders")]
    public static void SetupResourcesFolders()
    {
        Debug.Log("[DataResourcesMigration] Starting Resources folder setup...");

        // Create Resources folder structure if it doesn't exist
        EnsureResourcesFolders();

        // Copy or verify data assets are in Resources
        MigrateRaceData();
        MigrateWeaponData();
        MigrateAbilityData();

        AssetDatabase.Refresh();
        Debug.Log("[DataResourcesMigration] Resources folder setup complete!");
    }

    private static void EnsureResourcesFolders()
    {
        string[] folders = new string[]
        {
            "Assets/Resources",
            "Assets/Resources/Data",
            "Assets/Resources/Data/Races",
            "Assets/Resources/Data/Weapons",
            "Assets/Resources/Data/Abilities",
            "Assets/Resources/Data/Abilities/Orc",
            "Assets/Resources/Data/Abilities/Human",
            "Assets/Resources/Data/Abilities/Undead",
            "Assets/Resources/Data/Abilities/NightElf",
            "Assets/Resources/Data/Abilities/BloodElf",
            "Assets/Resources/Data/Abilities/Troll",
            "Assets/Resources/Data/Abilities/Dwarf",
            "Assets/Resources/Data/Abilities/Celestial",
            "Assets/Resources/Prefabs"
        };

        foreach (string folder in folders)
        {
            if (!AssetDatabase.IsValidFolder(folder))
            {
                string parentFolder = Path.GetDirectoryName(folder).Replace("\\", "/");
                string newFolderName = Path.GetFileName(folder);
                AssetDatabase.CreateFolder(parentFolder, newFolderName);
                Debug.Log($"[DataResourcesMigration] Created folder: {folder}");
            }
        }
    }

    private static void MigrateRaceData()
    {
        string sourceFolder = "Assets/Data/Races";
        string targetFolder = "Assets/Resources/Data/Races";

        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogWarning($"[DataResourcesMigration] Source folder not found: {sourceFolder}");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:RaceData", new[] { sourceFolder });
        foreach (string guid in guids)
        {
            string sourcePath = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(targetFolder, fileName);

            if (!File.Exists(targetPath))
            {
                AssetDatabase.CopyAsset(sourcePath, targetPath);
                Debug.Log($"[DataResourcesMigration] Copied: {fileName} to Resources");
            }
        }

        Debug.Log($"[DataResourcesMigration] Processed {guids.Length} race data assets");
    }

    private static void MigrateWeaponData()
    {
        string sourceFolder = "Assets/Data/Weapons";
        string targetFolder = "Assets/Resources/Data/Weapons";

        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogWarning($"[DataResourcesMigration] Source folder not found: {sourceFolder}");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:WeaponData", new[] { sourceFolder });
        foreach (string guid in guids)
        {
            string sourcePath = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(targetFolder, fileName);

            if (!File.Exists(targetPath))
            {
                AssetDatabase.CopyAsset(sourcePath, targetPath);
                Debug.Log($"[DataResourcesMigration] Copied: {fileName} to Resources");
            }
        }

        Debug.Log($"[DataResourcesMigration] Processed {guids.Length} weapon data assets");
    }

    private static void MigrateAbilityData()
    {
        string sourceFolder = "Assets/Data/Abilities";
        string targetFolder = "Assets/Resources/Data/Abilities";

        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogWarning($"[DataResourcesMigration] Source folder not found: {sourceFolder}");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:AbilityData", new[] { sourceFolder });
        foreach (string guid in guids)
        {
            string sourcePath = AssetDatabase.GUIDToAssetPath(guid);
            string relativePath = sourcePath.Replace(sourceFolder + "/", "");
            string targetPath = Path.Combine(targetFolder, relativePath);
            
            string targetDir = Path.GetDirectoryName(targetPath);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            if (!File.Exists(targetPath))
            {
                AssetDatabase.CopyAsset(sourcePath, targetPath);
                Debug.Log($"[DataResourcesMigration] Copied: {relativePath} to Resources");
            }
        }

        Debug.Log($"[DataResourcesMigration] Processed {guids.Length} ability data assets");
    }

    [MenuItem("Tools/CS 1.7/Verify Resources Setup")]
    public static void VerifyResourcesSetup()
    {
        Debug.Log("[DataResourcesMigration] Verifying Resources setup...");

        // Check races
        RaceData[] races = Resources.LoadAll<RaceData>("Data/Races");
        Debug.Log($"✓ Found {races.Length} race data assets in Resources");

        // Check weapons
        WeaponData[] weapons = Resources.LoadAll<WeaponData>("Data/Weapons");
        Debug.Log($"✓ Found {weapons.Length} weapon data assets in Resources");

        // Check abilities
        AbilityData[] abilities = Resources.LoadAll<AbilityData>("Data/Abilities");
        Debug.Log($"✓ Found {abilities.Length} ability data assets in Resources");

        if (races.Length == 0 || weapons.Length == 0 || abilities.Length == 0)
        {
            Debug.LogWarning("[DataResourcesMigration] Some data is missing! Run 'Setup Resources Folders' first.");
        }
        else
        {
            Debug.Log("[DataResourcesMigration] ✓ All resources verified successfully!");
        }
    }
}
#endif
