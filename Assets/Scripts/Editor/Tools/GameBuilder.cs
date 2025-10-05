using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace CS17.Editor
{
    /// <summary>
    /// Comprehensive Unity Editor tool to build the entire game
    /// Creates weapons, maps, UI, and configures all systems
    /// </summary>
    public class GameBuilder : EditorWindow
    {
        private Vector2 scrollPosition;
        private bool showWeapons = true;
        private bool showMap = true;
        private bool showUI = true;
        private bool showRaces = true;
        
        [MenuItem("Tools/CS 1.7/Game Builder")]
        public static void ShowWindow()
        {
            GameBuilder window = GetWindow<GameBuilder>("CS 1.7 Game Builder");
            window.minSize = new Vector2(400, 600);
            window.Show();
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Space(10);
            GUILayout.Label("CS 1.7 - Game Builder", EditorStyles.boldLabel);
            GUILayout.Label("Build the entire game with one click!", EditorStyles.miniLabel);
            GUILayout.Space(10);

            // Build All Button
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("🚀 BUILD ENTIRE GAME", GUILayout.Height(50)))
            {
                BuildEverything();
            }
            GUI.backgroundColor = Color.white;

            GUILayout.Space(20);
            EditorGUILayout.HelpBox("Or build components individually:", MessageType.Info);
            GUILayout.Space(10);

            // Weapons Section
            showWeapons = EditorGUILayout.Foldout(showWeapons, "🔫 Weapons (20 Total)", true);
            if (showWeapons)
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginVertical("box");
                
                if (GUILayout.Button("Generate All Weapons"))
                {
                    WeaponGenerator.GenerateAllWeapons();
                }
                
                GUILayout.Label("Creates 20 weapon prefabs:", EditorStyles.miniLabel);
                GUILayout.Label("• Pistols (5): Glock, USP, Desert Eagle, P250, Five-Seven", EditorStyles.miniLabel);
                GUILayout.Label("• SMGs (5): MP5, MP7, UMP-45, P90, MAC-10", EditorStyles.miniLabel);
                GUILayout.Label("• Rifles (5): AK-47, M4A4, Galil, FAMAS, AUG", EditorStyles.miniLabel);
                GUILayout.Label("• Snipers (3): AWP, Scout, G3SG1", EditorStyles.miniLabel);
                GUILayout.Label("• Heavy (2): M249, Nova", EditorStyles.miniLabel);
                
                GUILayout.EndVertical();
                EditorGUI.indentLevel--;
                GUILayout.Space(10);
            }

            // Map Section
            showMap = EditorGUILayout.Foldout(showMap, "🗺️ Map & Spawn System", true);
            if (showMap)
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginVertical("box");
                
                if (GUILayout.Button("Generate Test Map"))
                {
                    MapGenerator.GenerateTestMap();
                }
                
                GUILayout.Label("Creates complete test environment:", EditorStyles.miniLabel);
                GUILayout.Label("• Ground plane (50x50)", EditorStyles.miniLabel);
                GUILayout.Label("• 10 spawn points (5 per team)", EditorStyles.miniLabel);
                GUILayout.Label("• Cover objects and walls", EditorStyles.miniLabel);
                GUILayout.Label("• Bomb sites (A & B)", EditorStyles.miniLabel);
                GUILayout.Label("• NavMesh baking configured", EditorStyles.miniLabel);
                
                GUILayout.EndVertical();
                EditorGUI.indentLevel--;
                GUILayout.Space(10);
            }

            // UI Section
            showUI = EditorGUILayout.Foldout(showUI, "🖥️ User Interface", true);
            if (showUI)
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginVertical("box");
                
                if (GUILayout.Button("Build Complete UI"))
                {
                    UIBuilder.BuildCompleteUI();
                }
                
                GUILayout.Label("Creates all UI components:", EditorStyles.miniLabel);
                GUILayout.Label("• Buy Menu with weapon list", EditorStyles.miniLabel);
                GUILayout.Label("• Scoreboard with player stats", EditorStyles.miniLabel);
                GUILayout.Label("• Ability HUD with cooldowns", EditorStyles.miniLabel);
                GUILayout.Label("• Kill Feed", EditorStyles.miniLabel);
                GUILayout.Label("• Health & Money display", EditorStyles.miniLabel);
                
                GUILayout.EndVertical();
                EditorGUI.indentLevel--;
                GUILayout.Space(10);
            }

            // Races Section
            showRaces = EditorGUILayout.Foldout(showRaces, "⚔️ Race System", true);
            if (showRaces)
            {
                EditorGUI.indentLevel++;
                GUILayout.BeginVertical("box");
                
                if (GUILayout.Button("Configure All Races"))
                {
                    RaceConfigurator.ConfigureAllRaces();
                }
                
                GUILayout.Label("Sets up 8 races with abilities:", EditorStyles.miniLabel);
                GUILayout.Label("• Orc, Human, Undead, Night Elf", EditorStyles.miniLabel);
                GUILayout.Label("• Blood Elf, Troll, Dwarf, Celestial", EditorStyles.miniLabel);
                GUILayout.Label("• 3 abilities per race (24 total)", EditorStyles.miniLabel);
                
                GUILayout.EndVertical();
                EditorGUI.indentLevel--;
                GUILayout.Space(10);
            }

            GUILayout.Space(20);
            EditorGUILayout.HelpBox("After building, press Play to test!", MessageType.Info);

            EditorGUILayout.EndScrollView();
        }

        private void BuildEverything()
        {
            if (!EditorUtility.DisplayDialog("Build Entire Game", 
                "This will generate ALL game content:\n\n" +
                "• 20 Weapon Prefabs\n" +
                "• Complete Test Map\n" +
                "• Full UI System\n" +
                "• Race Configuration\n\n" +
                "Continue?", 
                "Yes, Build Everything!", "Cancel"))
            {
                return;
            }

            EditorUtility.DisplayProgressBar("Building Game", "Generating weapons...", 0.2f);
            WeaponGenerator.GenerateAllWeapons();

            EditorUtility.DisplayProgressBar("Building Game", "Creating map...", 0.4f);
            MapGenerator.GenerateTestMap();

            EditorUtility.DisplayProgressBar("Building Game", "Building UI...", 0.6f);
            UIBuilder.BuildCompleteUI();

            EditorUtility.DisplayProgressBar("Building Game", "Configuring races...", 0.8f);
            RaceConfigurator.ConfigureAllRaces();

            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayDialog("Build Complete!", 
                "🎉 Game build successful!\n\n" +
                "Everything is ready to play:\n" +
                "• 20 Weapons generated\n" +
                "• Test map created\n" +
                "• UI system built\n" +
                "• Races configured\n\n" +
                "Press Play to test!", 
                "Awesome!");

            Debug.Log("=== CS 1.7 Game Build Complete! ===");
        }
    }
}
