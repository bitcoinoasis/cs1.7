# CS 1.6 Warcraft Mod Clone - Unity

A Counter-Strike 1.6 inspired first-person shooter with Warcraft 3 mod mechanics built in Unity.

---

## 🚀 New to the Project? Start Here!

**For Quick Testing/Demo:** See [QUICK_SETUP.md](QUICK_SETUP.md) - Get running in 5 minutes!

**Demo Mode Features:** See [DEMO_MODE.md](DEMO_MODE.md) - Complete guide to practice mode

**Full Documentation:** Continue reading below for complete setup

---

## 🎯 Demo/Practice Mode

**Quick Start for Testing:**

1. Set `Demo Mode = true` in GameManager
2. Enjoy unlimited money ($16,000)
3. Buy weapons anytime (press B)
4. No round timers or restrictions
5. Use debug commands (F1-F11) for instant testing

### Debug Commands (F-Keys):
- **F1** - Add 1000 XP
- **F2** - Instant Level Up
- **F3** - Max Level (Level 10)
- **F5** - Spawn Bots (via BotSpawner)
- **F6** - Clear All Bots
- **F7** - Full Health + Armor
- **F8** - Toggle God Mode
- **F9** - Reset All Ability Cooldowns
- **F10** - Add 1 Skill Point
- **F11** - Refill Ammo

## Features

### Core FPS Mechanics
- **First-Person Controller**: WASD movement, mouse look, jumping, crouching, sprinting
- **Weapon System**: Shooting, reloading, weapon switching, recoil
- **Health System**: Player health, armor, damage, death, and respawn
- **Round-Based Gameplay**: Buy phase, active phase, round end, win conditions

### Warcraft Mod Features
- **Race System**: Choose from 4 unique races (Human, Orc, Undead, Night Elf)
- **Leveling System**: Gain XP from kills, level up to unlock abilities
- **Skill Points**: Allocate points to upgrade abilities
- **Active Abilities**: Each race has unique abilities with cooldowns
  - Teleport
  - Invisibility
  - Speed Boost
  - Healing
  - Shield/Armor
  - Vampire lifesteal
- **Passive Bonuses**: Health, speed, damage multipliers per race

## Project Structure

```
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerController.cs       # FPS movement and camera
│   │   └── PlayerHealth.cs           # Health, armor, death system
│   ├── Weapons/
│   │   ├── WeaponData.cs             # Weapon configuration (ScriptableObject)
│   │   ├── Weapon.cs                 # Weapon shooting mechanics
│   │   └── WeaponManager.cs          # Weapon switching
│   ├── Warcraft/
│   │   ├── RaceData.cs               # Race configuration (ScriptableObject)
│   │   ├── AbilityData.cs            # Ability configuration (ScriptableObject)
│   │   ├── PlayerRace.cs             # Race selection and passive bonuses
│   │   ├── PlayerExperience.cs       # XP and leveling system
│   │   └── PlayerAbilities.cs        # Ability activation and management
│   ├── Game/
│   │   ├── GameManager.cs            # Round management, teams, scoring
│   │   ├── BuyMenu.cs                # Weapon purchasing system
│   │   └── DebugCommands.cs          # F-key debug commands for testing
│   ├── Bot/
│   │   ├── Bot.cs                    # Target bot with health
│   │   └── BotSpawner.cs             # Spawn static bots for aim practice
│   └── UI/
│       ├── GameHUD.cs                # In-game HUD (health, ammo, XP)
│       └── RaceSelectionMenu.cs      # Race selection UI
├── Prefabs/                          # Player, weapons, UI prefabs
├── Scenes/                           # Game scenes
└── Materials/                        # Materials and textures
```

## Setup Instructions

### Quick Demo Setup (For Testing)

1. **Create GameManager**:
   - Create empty GameObject named "GameManager"
   - Attach GameManager script
   - ✅ **Check "Demo Mode" in inspector**
   - This gives unlimited money and disables round timers

2. **Create Player** (simplified setup):
   - Create empty GameObject named "Player", tag as "Player"
   - Add Character Controller, PlayerController, PlayerHealth, PlayerRace, PlayerExperience, PlayerAbilities, WeaponManager
   - Add Camera child at (0, 0.6, 0)
   - Add WeaponHolder child to camera at (0.3, 0.4, 0.5)

3. **Add Debug Commands**:
   - On GameManager, add DebugCommands script
   - Check "Enable Debug Commands"
   - Press F1-F11 to test features (see on-screen help)

4. **Create Target Bots**:
   - Create a Capsule primitive, name it "BotTarget"
   - Add Bot.cs script
   - Set health, colors, respawn settings
   - Save as prefab
   - Create empty GameObject "BotSpawner"
   - Add BotSpawner.cs script
   - Assign bot prefab
   - Set spawn pattern (Circle, Line, Grid, etc.)
   - Press F5 to spawn, F6 to clear

5. **Test Everything**:
   - Press Play
   - Press Tab to select race
   - Press B to buy weapons (unlimited money!)
   - Press F1-F3 to level up instantly
   - Press F5 to spawn target bots
   - Shoot bots to gain XP
   - Press E/Q/F to use abilities
   - Press F10 to add skill points for upgrades

### Full Setup Instructions

### 1. Create the Player

1. Create an empty GameObject named "Player"
2. Add the following components:
   - Character Controller (adjust height to 2, radius to 0.5)
   - PlayerController script
   - PlayerHealth script
   - PlayerRace script
   - PlayerExperience script
   - PlayerAbilities script
   - WeaponManager script
3. Create a child Camera object:
   - Position at (0, 0.6, 0) relative to player
   - Tag as "MainCamera"
4. Create a child empty GameObject named "WeaponHolder":
   - Position at (0.3, 0.4, 0.5) relative to camera
5. Tag the Player GameObject as "Player"

### 2. Create Race Data (ScriptableObjects)

Right-click in Assets → Create → Warcraft → Race Data

**Example: Human Race**
- Race Type: Human
- Race Name: "Human"
- Health Multiplier: 1.0
- Speed Multiplier: 1.0
- Damage Multiplier: 1.0
- Armor Bonus: 5
- Health Regen: 0.5/s

**Example: Orc Race**
- Health Multiplier: 1.2
- Speed Multiplier: 0.9
- Damage Multiplier: 1.15
- Armor Bonus: 10

**Example: Undead Race**
- Health Multiplier: 0.9
- Speed Multiplier: 1.1
- Damage Multiplier: 1.0
- Health Regen: 2.0/s

**Example: Night Elf Race**
- Health Multiplier: 0.95
- Speed Multiplier: 1.15
- Damage Multiplier: 1.0
- Armor Bonus: 0

### 3. Create Ability Data (ScriptableObjects)

Right-click in Assets → Create → Warcraft → Ability Data

**Example: Teleport**
- Ability Type: Teleport
- Hotkey: E
- Cooldown: 15s
- Range: 10 units

**Example: Speed Boost**
- Ability Type: SpeedBoost
- Hotkey: Q
- Cooldown: 20s
- Duration: 5s
- Value: 50 (50% speed increase)

**Example: Heal**
- Ability Type: Heal
- Hotkey: F
- Cooldown: 30s
- Value: 50 (HP restored)

### 4. Create Weapon Data (ScriptableObjects)

Right-click in Assets → Create → Weapons → Weapon Data

**Example: AK-47**
- Weapon Name: "AK-47"
- Weapon Type: Rifle
- Damage: 36
- Fire Rate: 0.1
- Magazine Size: 30
- Max Ammo: 90
- Is Automatic: true
- Recoil Amount: 2.5

**Example: Desert Eagle**
- Weapon Type: Pistol
- Damage: 55
- Fire Rate: 0.2
- Magazine Size: 7
- Max Ammo: 35
- Is Automatic: false

### 5. Create Spawn Points

1. Create empty GameObjects for spawns
2. For Terrorist spawns, tag as "TSpawn"
3. For Counter-Terrorist spawns, tag as "CTSpawn"
4. Position them around your map

### 6. Create a Basic Map

1. Create a Plane (scale 10x10) for the ground
2. Add walls using Cubes (scale them appropriately)
3. Add cover objects
4. Position spawn points on both sides
5. Add a Respawn point (tag as "Respawn") for the center

### 7. Setup UI (Canvas)

1. Create Canvas (Screen Space - Overlay)
2. Add UI elements:
   - Health text (bottom left)
   - Armor text (bottom left)
   - Ammo text (bottom right)
   - XP bar (top of screen)
   - Level text
   - Race name text
   - Crosshair (center)
3. Attach GameHUD script to Canvas
4. Link all UI elements in inspector

### 8. Setup Race Selection Menu

1. Create another Canvas for race selection
2. Add panel with race buttons
3. Add race info display area
4. Attach RaceSelectionMenu script
5. Assign race data assets

### 9. Setup Game Manager

1. Create empty GameObject named "GameManager"
2. Attach GameManager script
3. Configure settings:
   - **Demo Mode**: Check this for practice/testing
   - Round settings (if demo mode off)
   - Money settings

### 10. Add Debug Commands (Optional but Recommended)

1. On GameManager GameObject, add DebugCommands script
2. Check "Enable Debug Commands"
3. Use F-keys for instant testing (see Debug Commands section)

### 11. Setup Target Bots for Practice

1. **Create Bot Prefab**:
   - Create Capsule primitive (scale 1, 2, 1)
   - Add Bot.cs script
   - Set health: 100
   - Set colors: Normal (red), Hit (yellow)
   - Check "Respawn On Death"
   - Add Collider if not present
   - Save as Prefab

2. **Create Bot Spawner**:
   - Create empty GameObject "BotSpawner"
   - Add BotSpawner.cs script
   - Assign bot prefab
   - Configure:
     - Number of Bots: 10
     - Spawn Pattern: Circle/Line/Grid
     - Spawn Radius: 10
     - Bot Health: 100
     - Bots Respawn: true
   - Press F5 in play mode to spawn
   - Press F6 to clear all bots

## Bot Spawn Patterns

- **Circle**: Bots arranged in a circle around spawner
- **Line**: Bots in a straight line
- **Grid**: Bots in a grid formation
- **Random**: Random positions within radius
- **Custom Points**: Use custom transform array

## Controls

### Movement
- **WASD**: Move
- **Space**: Jump
- **C**: Crouch/Stand
- **Left Shift**: Sprint
- **Mouse**: Look around

### Combat
- **Left Click**: Shoot
- **R**: Reload
- **1-4**: Switch weapons
- **Mouse Wheel**: Cycle weapons

### Warcraft Abilities
- **E, Q, F**: Activate abilities (configurable per race)
- **Tab**: Open race selection menu
- **B**: Open buy menu (during buy time)

## Race Abilities Examples

### Human
- Teleport (E): Short-range teleport in look direction
- Heal (F): Restore health

### Orc
- Speed Boost (Q): Temporary movement speed increase
- Shield (E): Add armor

### Undead
- Vampire (Q): Lifesteal on hits
- Invisibility (E): Become nearly invisible

### Night Elf
- Speed Boost (Q): Very fast movement
- Teleport (E): Long-range teleport

## How XP & Leveling Works

1. Kill enemies to gain 100 XP per kill
2. Gain levels (max level 10)
3. Each level grants 1 skill point
4. Use skill points to upgrade abilities
5. Upgrading abilities:
   - Increases effect value
   - Reduces cooldown
   - Max 3 levels per ability

## Buy System

- Each round starts with buy time (15 seconds)
- Press **B** to open buy menu
- Purchase weapons with money
- Money earned from:
  - Round wins: $3500
  - Round losses: $1400
  - Kills: $300 each
- Starting money: $800

## Game Rules

- First team to 16 rounds wins
- Eliminate all enemies to win round
- Time runs out = CT wins
- Buy weapons during buy phase
- Earn XP to level up your race

## Dependencies

- TextMeshPro (for UI text)
- Unity Input System (uses legacy Input)

## Tips for Development

1. **Testing**: Start with a simple box map to test mechanics
2. **Race Balance**: Adjust multipliers in RaceData ScriptableObjects
3. **Abilities**: Create more abilities by adding to AbilityType enum
4. **Weapons**: Create multiple WeaponData assets for variety
5. **UI**: Customize the HUD and menus to your preference

## Future Enhancements

- [ ] Networking/Multiplayer
- [ ] More races and abilities
- [ ] Items/Equipment system
- [ ] Better visual effects
- [ ] Sound effects and music
- [ ] Multiple maps
- [ ] Bot AI
- [ ] Scoreboard
- [ ] Kill feed
- [ ] Minimap

## License

This is a learning project inspired by CS 1.6 and Warcraft 3 mod.

---

Enjoy building your CS Warcraft mod! 🎮
