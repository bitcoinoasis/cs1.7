# CS 1.7 Game Builder Guide

## 🚀 One-Click Game Building

The Game Builder system allows you to **generate the entire game with one click** in Unity Editor!

## How to Use

### 1. Open the Game Builder Window

In Unity Editor:
```
Menu: Tools → CS 1.7 → Game Builder
```

### 2. Build Everything

Click the **"BUILD ENTIRE GAME"** button to:
- ✅ Generate all 20 weapons (with 3D models)
- ✅ Create test map with spawns, cover, and objectives
- ✅ Configure UI system (runtime)
- ✅ Set up all 8 races with abilities

### 3. Build Individual Components

Or build one component at a time:

#### **Weapons** (20 total)
- 5 Pistols: Glock-18, USP-S, Desert Eagle, P250, Five-Seven
- 5 SMGs: MP5-SD, MP7, UMP-45, P90, MAC-10
- 5 Rifles: AK-47, M4A4, Galil AR, FAMAS, AUG
- 3 Snipers: AWP, Scout, G3SG1
- 2 Heavy: M249, Nova

Each weapon has:
- ScriptableObject data (damage, price, fire rate, etc.)
- 3D prefab model (primitive shapes, color-coded)
- Saved to: `Assets/Prefabs/Weapons/`

#### **Map & Spawns**
- 50×50 ground plane
- 10 spawn points (5 per team) with colored gizmos
- Cover objects (boxes and walls)
- 2 bomb sites (A & B) with triggers
- Boundary walls
- All objects marked static for NavMesh

#### **UI System**
Runtime components (auto-created when playing):
- Crosshair (center screen)
- Health display (bottom-left)
- Buy menu (B key)
- Scoreboard (Tab key)
- Ability HUD (Q/E/R keys)
- Kill feed (top-right)

#### **Race System**
8 races with 3 abilities each:
- **Orc**: Bash, Critical Strike, Reincarnation
- **Human**: Teleport, Devotion Aura, Invisibility
- **Undead**: Vampiric Aura, Unholy Aura, Sleep
- **Night Elf**: Evasion, Thorns, Trueshot Aura
- **Blood Elf**: Mana Burn, Phoenix, Banish
- **Troll**: Healing Wave, Serpent Ward, Hex
- **Dwarf**: Thunder Clap, Bash, Avatar
- **Celestial**: Divine Shield, Resurrection, Wrath

## What Gets Generated

### File Structure After Building:
```
Assets/
├── Prefabs/
│   ├── Weapons/
│   │   ├── Glock-18.prefab
│   │   ├── AK-47.prefab
│   │   ├── AWP.prefab
│   │   └── ... (17 more)
│   └── (UI prefabs created at runtime)
│
├── Data/
│   ├── Weapons/
│   │   ├── Glock-18.asset
│   │   ├── AK-47.asset
│   │   └── ... (17 more)
│   ├── Races/
│   │   └── (8 race data files)
│   └── Abilities/
│       └── (24 ability data files)
│
└── Scenes/
    └── Game Scene (with generated map)
```

### Scene Hierarchy After Building:
```
Game Scene
├── Environment
│   ├── Ground (50×50 plane)
│   ├── Walls (4 boundary walls)
│   └── Cover Objects (boxes & walls)
│
├── SpawnPoints
│   ├── T_Spawn_1 through T_Spawn_5
│   └── CT_Spawn_1 through CT_Spawn_5
│
└── BombSites
    ├── BombSite_A (with trigger)
    └── BombSite_B (with trigger)
```

## Testing the Generated Content

### 1. After Building, Press Play

The game will automatically:
- Spawn you at a spawn point
- Show UI (crosshair, health, etc.)
- Spawn bots at opposite team spawns
- Enable weapon buying (press B)
- Activate your race abilities (Q/E/R)

### 2. Console Commands

```
setrace orc         - Change to Orc
setrace human       - Change to Human
give ak47          - Give yourself an AK-47
give awp           - Give yourself an AWP
kill               - Kill yourself (test respawn)
restart            - Restart the round
```

### 3. Keyboard Controls

- **WASD** - Move
- **Mouse** - Look
- **Left Click** - Shoot
- **R** - Reload
- **B** - Buy menu
- **Tab** - Scoreboard
- **Q/E/R** - Abilities (race-specific)
- **1-9** - Switch weapons
- **G** - Drop weapon

## Architecture

### Builder System Components

1. **GameBuilder.cs** - Main editor window
   - Central control panel
   - Calls all other generators
   - Progress tracking

2. **WeaponGenerator.cs** - Creates all weapons
   - Generates ScriptableObject data
   - Builds 3D models from primitives
   - Creates prefabs

3. **MapGenerator.cs** - Generates test map
   - Creates terrain and cover
   - Places spawn points
   - Sets up objectives

4. **UIBuilder.cs** - Verifies UI system
   - Confirms UI scripts exist
   - Documents runtime UI

5. **RaceConfigurator.cs** - Verifies races
   - Counts race data files
   - Lists available abilities

### Integration with Game Systems

The generated content integrates with:
- **EventSystem** - Game events (kills, deaths, objectives)
- **ServiceLocator** - System access (economy, abilities)
- **BuyMenu** - Uses generated weapon data
- **PlayerController** - Equips generated weapons
- **RaceSystem** - Applies race abilities
- **BotAI** - Uses generated spawns and weapons

## Customization

### Modify Weapon Stats

Edit `WeaponGenerator.cs` lines with weapon definitions:
```csharp
CreateWeapon("AK-47", WeaponType.Rifle, 
    price: 2700,
    damage: 36f,
    magazineSize: 30,
    reloadTime: 2.5f,
    fireRate: 0.1f,
    weaponColor: Color.yellow
);
```

### Adjust Map Layout

Edit `MapGenerator.cs` spawn positions:
```csharp
CreateSpawnPoint("T_Spawn_1", new Vector3(-20, 0, -20), Color.red);
```

### Add More Weapons

In `WeaponGenerator.cs`, add to `GenerateAllWeapons()`:
```csharp
CreateWeapon("New Weapon", WeaponType.Rifle, 
    price, damage, magazineSize, reloadTime, fireRate, color);
```

## Troubleshooting

### "Build failed" message
- Check Unity Console for errors
- Ensure all required folders exist
- Try building individual components first

### Weapons not appearing
- Check `Assets/Prefabs/Weapons/` folder
- Verify WeaponData assets created
- Rebuild weapons component

### No spawns in scene
- Check Scene Hierarchy for "SpawnPoints"
- Look for colored gizmos (red/blue spheres)
- Rebuild map component

### UI not showing
- UI is created at runtime
- Press Play to see UI
- Check Console for SceneInitializer logs

## Next Steps

After building:
1. ✅ Press Play to test
2. ✅ Buy weapons (press B)
3. ✅ Test abilities (Q/E/R)
4. ✅ Kill bots for XP
5. ✅ Level up your race

## Benefits

- **Instant Prototyping** - Game ready in seconds
- **No Manual Work** - Everything automated
- **Easy Iteration** - Rebuild anytime
- **Consistent Quality** - Standard patterns
- **Version Control** - All code-based

---

**Ready to build?** Open `Tools → CS 1.7 → Game Builder` and click **"BUILD ENTIRE GAME"**!
