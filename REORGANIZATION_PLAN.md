# Project Reorganization Plan

## Current Issues
1. ❌ Inconsistent folder structure (some files in root, some in subfolders)
2. ❌ Duplicate organization patterns (Bot/ folder and BotAI.cs in root)
3. ❌ Mixed concerns (Game/ folder contains unrelated scripts)
4. ❌ No clear separation of runtime vs editor code
5. ❌ No test infrastructure
6. ❌ Missing core architecture files

## New Folder Structure (Industry Standard)

```
Assets/
├── Scripts/
│   ├── Core/                      # Core game systems
│   │   ├── GameManager.cs
│   │   ├── EventSystem.cs
│   │   ├── ServiceLocator.cs
│   │   └── SceneController.cs
│   │
│   ├── Gameplay/                  # Gameplay mechanics
│   │   ├── Player/
│   │   │   ├── PlayerController.cs
│   │   │   ├── PlayerHealth.cs
│   │   │   └── PlayerInput.cs
│   │   ├── Bot/
│   │   │   ├── BotAI.cs
│   │   │   ├── BotController.cs
│   │   │   └── BotSpawner.cs
│   │   ├── Combat/
│   │   │   ├── Hitbox.cs
│   │   │   ├── DamageSystem.cs
│   │   │   └── Weapon.cs
│   │   └── GameModes/
│   │       ├── GameModeManager.cs
│   │       └── RoundController.cs
│   │
│   ├── Systems/                   # Game systems
│   │   ├── Economy/
│   │   │   └── EconomySystem.cs
│   │   ├── Abilities/
│   │   │   ├── AbilitySystem.cs
│   │   │   ├── Ability.cs
│   │   │   └── Implementations/
│   │   │       ├── Orc/
│   │   │       ├── Human/
│   │   │       ├── Undead/
│   │   │       └── NightElf/
│   │   └── Races/
│   │       ├── PlayerRace.cs
│   │       └── PlayerExperience.cs
│   │
│   ├── Weapons/                   # Weapon system
│   │   ├── WeaponManager.cs
│   │   ├── Weapon.cs
│   │   ├── WeaponData.cs
│   │   └── BulletEffect.cs
│   │
│   ├── UI/                        # User interface
│   │   ├── Menus/
│   │   │   ├── BuyMenu.cs
│   │   │   ├── RaceSelectionMenu.cs
│   │   │   └── Scoreboard.cs
│   │   ├── HUD/
│   │   │   ├── GameHUD.cs
│   │   │   ├── AbilityHUD.cs
│   │   │   ├── HealthDisplay.cs
│   │   │   └── Crosshair.cs
│   │   └── Feedback/
│   │       └── KillFeed.cs
│   │
│   ├── Data/                      # ScriptableObject data
│   │   ├── Races/
│   │   │   └── RaceData.cs
│   │   ├── Abilities/
│   │   │   └── AbilityData.cs
│   │   └── Weapons/
│   │       └── WeaponData.cs
│   │
│   ├── Utilities/                 # Helper classes
│   │   ├── DebugCommands.cs
│   │   ├── ObjectPooler.cs
│   │   └── Extensions.cs
│   │
│   ├── Initialization/            # Scene setup
│   │   ├── SceneInitializer.cs
│   │   └── BotSpawnerAutoSetup.cs
│   │
│   └── Editor/                    # Editor-only scripts
│       ├── Tools/
│       │   ├── CreateBotPrefab.cs
│       │   ├── MCPConnectionTest.cs
│       │   └── DataResourcesMigration.cs
│       └── Inspectors/
│           └── (Custom inspectors here)
│
├── Tests/                         # Unit and integration tests
│   ├── EditMode/
│   └── PlayMode/
│
├── Prefabs/                       # All prefabs
│   ├── Player/
│   ├── Bots/
│   ├── Weapons/
│   ├── UI/
│   └── Environment/
│
├── Resources/                     # Runtime-loaded assets
│   ├── Data/
│   │   ├── Races/
│   │   ├── Weapons/
│   │   └── Abilities/
│   └── Prefabs/
│
├── Scenes/                        # Game scenes
│   ├── Main.unity
│   ├── TestMap.unity
│   └── MainMenu.unity
│
└── Materials/                     # Materials and shaders
    ├── Player/
    ├── Weapons/
    └── Environment/
```

## Migration Steps

### Phase 1: Create New Structure
1. Create all new folders
2. Keep old files in place initially

### Phase 2: Move Files (with git)
1. Use `git mv` to preserve history
2. Move files to new locations
3. Update all references in code

### Phase 3: Update References
1. Fix all using statements
2. Update prefab references
3. Fix ScriptableObject references

### Phase 4: Clean Up
1. Remove empty folders
2. Delete duplicate files
3. Update .meta files

### Phase 5: Verify
1. Ensure 0 compilation errors
2. Test all systems
3. Run git status

## Benefits

✅ **Clear Organization** - Easy to find any script
✅ **Scalable** - Easy to add new features
✅ **Team-Friendly** - Standard structure everyone knows
✅ **CI/CD Ready** - Clear test separation
✅ **Maintainable** - Grouped by feature, not by type
✅ **Professional** - Industry-standard layout

## Implementation

I'll create a script to automate this reorganization while preserving git history.
