# Automatic Scene Setup Guide

## Overview
The game now automatically creates all necessary GameObjects and systems when you run any scene. No manual setup required!

## How It Works

### SceneInitializer.cs
**Location:** `Assets/Scripts/Game/SceneInitializer.cs`

This script runs automatically before any scene loads and creates:

1. **GameManagers** (Parent GameObject)
   - GameModeManager
   - EconomySystem
   - BotSpawner

2. **UI Canvas** with all UI components:
   - Crosshair
   - AbilityHUD
   - BuyMenu
   - Scoreboard
   - KillFeed

3. **Player** GameObject with:
   - CharacterController
   - Main Camera
   - PlayerHealth
   - PlayerController
   - PlayerRace
   - AbilitySystem
   - WeaponHolder

4. **SpawnPoints** (10 spawn points - 5 per team)
   - Team1_Spawns (Terrorists)
   - Team2_Spawns (Counter-Terrorists)

## Quick Start (3 Steps)

### Step 1: Prepare Resources Folder
In Unity Editor:
1. Go to **Tools → CS 1.7 → Setup Resources Folders**
2. Wait for completion (copies all race/weapon/ability data)
3. Go to **Tools → CS 1.7 → Verify Resources Setup**

This copies all ScriptableObject data to the Resources folder so they can be loaded at runtime.

### Step 2: Add Ground Plane
1. Right-click in Hierarchy → **3D Object → Plane**
2. Name it "Ground"
3. Set Position: (0, 0, 0)
4. Set Scale: (10, 1, 10)

### Step 3: Press Play!
That's it! Everything else is created automatically.

## What Happens When You Press Play

```
[SceneInitializer] Starting automatic scene setup...
[SceneInitializer] GameManagers created with GameModeManager, EconomySystem, and BotSpawner
[SceneInitializer] UI Canvas created with all UI components
[SceneInitializer] Player created with camera and components
[SceneInitializer] Created 10 spawn points (5 per team)
[SceneInitializer] Configured BotSpawner with spawn points
[SceneInitializer] Scene setup complete!
```

## How to Use

### To Spawn Bots Manually:
1. Press Play
2. Open Console (Ctrl+Shift+C)
3. Type: `spawnbots` (if DebugCommands is active)

OR

1. Find BotSpawner in Hierarchy: **GameManagers → BotSpawner**
2. In Inspector, click the **⋮** menu
3. Select "Spawn All Bots"

### To Test Different Game Modes:
1. Find GameModeManager: **GameManagers → GameModeManager**
2. In Inspector, change **Current Game Mode**:
   - Bomb Defusal
   - Team Deathmatch
   - Deathmatch
   - Gun Game

### To Adjust Bot Settings:
1. Find BotSpawner: **GameManagers → BotSpawner**
2. In Inspector, adjust:
   - Team 1 Bot Count
   - Team 2 Bot Count
   - Default Difficulty (Easy/Medium/Hard/Expert)
   - Randomize Difficulty (checkbox)

## Advanced: Custom Setup

If you want to customize the auto-setup:

### Disable Auto-Setup
Comment out the attribute in `SceneInitializer.cs`:
```csharp
// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
static void Initialize()
```

### Check for Existing Objects
The script automatically checks if objects already exist before creating them:
```csharp
if (GameObject.Find("GameManagers") != null)
{
    Debug.Log("GameManagers already exists, skipping creation.");
    return;
}
```

So you can manually create any object and the auto-setup will skip it!

## Troubleshooting

### "No race data found in Resources/Data/Races!"
**Solution:** Run **Tools → CS 1.7 → Setup Resources Folders**

### "Bot prefab not assigned!"
**Solution:** The script creates a runtime bot prefab automatically. Check Console for "[BotSpawnerAutoSetup] Created runtime bot prefab"

### "NavMesh not baked"
**Solution:** 
1. Window → AI → Navigation
2. Select "Ground" plane
3. In Navigation window, click "Bake"

### Bots don't spawn
**Check:**
1. Are spawn points created? Look in Hierarchy: **SpawnPoints**
2. Is BotSpawner configured? Check Inspector: **team1SpawnPoints** and **team2SpawnPoints** should have values
3. Try manually calling: Right-click BotSpawner → "Spawn All Bots"

### Player can't move
**Check:**
1. Is there a ground plane?
2. Is Player at position (0, 1, 0)?
3. Does Player have CharacterController component?

## Scripts Reference

### SceneInitializer.cs
Main auto-setup script that creates all game systems.

**Location:** `Assets/Scripts/Game/SceneInitializer.cs`

**When it runs:** Before scene loads (`RuntimeInitializeLoadType.BeforeSceneLoad`)

**What it creates:**
- GameManagers hierarchy
- UI Canvas hierarchy
- Player with camera
- Spawn points

### BotSpawnerAutoSetup.cs
Automatically configures BotSpawner with bot prefab and verifies data availability.

**Location:** `Assets/Scripts/Game/BotSpawnerAutoSetup.cs`

**When it runs:** When BotSpawner starts

**What it does:**
- Loads or creates bot prefab
- Verifies race/weapon data exists
- Creates runtime bot prefab if needed

### DataResourcesMigration.cs
Editor utility to copy data assets to Resources folder.

**Location:** `Assets/Scripts/Editor/DataResourcesMigration.cs`

**Menu:** Tools → CS 1.7 → Setup Resources Folders

**What it does:**
- Creates Resources folder structure
- Copies race, weapon, and ability data
- Maintains folder organization

### SpawnPoint.cs
Helper component for spawn point visualization.

**Location:** Embedded in `SceneInitializer.cs`

**What it does:**
- Marks spawn points with team ID
- Draws gizmos in Scene view (red for T, blue for CT)

## Scene Hierarchy After Auto-Setup

```
Scene
├── GameManagers
│   ├── GameModeManager
│   ├── EconomySystem
│   └── BotSpawner
├── UI Canvas
│   ├── Crosshair
│   ├── AbilityHUD
│   ├── BuyMenu (hidden)
│   ├── Scoreboard (hidden)
│   └── KillFeed
├── Player
│   └── Main Camera
│       └── WeaponHolder
├── SpawnPoints
│   ├── Team1_Spawns
│   │   ├── T_Spawn_1
│   │   ├── T_Spawn_2
│   │   ├── T_Spawn_3
│   │   ├── T_Spawn_4
│   │   └── T_Spawn_5
│   └── Team2_Spawns
│       ├── CT_Spawn_1
│       ├── CT_Spawn_2
│       ├── CT_Spawn_3
│       ├── CT_Spawn_4
│       └── CT_Spawn_5
└── Ground (manually created)
```

## Manual Setup (Old Method)

If you prefer to set up manually, see `UNITY_SETUP_GUIDE.md` for step-by-step instructions.

## Benefits of Auto-Setup

✅ **Instant Testing** - Just add ground and press play
✅ **No Manual Configuration** - Everything is preconfigured
✅ **Consistent Setup** - Same setup every time
✅ **Save Time** - No more 30-minute setup process
✅ **Focus on Development** - Spend time coding, not clicking
✅ **Version Control Friendly** - No scene files to commit

## Next Steps

1. ✅ Run "Setup Resources Folders"
2. ✅ Add ground plane
3. ✅ Press Play
4. ✅ Test gameplay
5. 🚀 Start developing!

## Need Help?

Check these documentation files:
- `GAME_COMPLETE.md` - Full feature list
- `UNITY_SETUP_GUIDE.md` - Manual setup method
- `FINAL_BUILD_REPORT.md` - Architecture overview
- `INTEGRATION_FIX_REPORT.md` - Recent fixes
