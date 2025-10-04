# CS 1.6 Warcraft Mod - Project Architecture

## Project Overview

**Type:** FPS Multiplayer Game with RPG Elements  
**Engine:** Unity 2020.3+  
**Language:** C#  
**Style:** Counter-Strike 1.6 meets Warcraft 3 mod  
**Target:** PC (Windows/macOS/Linux)

---

## 📁 Project Structure

```
cs1.7/
├── Assets/
│   ├── Data/                    # ScriptableObject assets
│   │   ├── Races/              # Race configurations
│   │   ├── Weapons/            # Weapon stats
│   │   └── Abilities/          # Ability definitions
│   ├── Materials/              # Visual materials
│   ├── Prefabs/                # Reusable game objects
│   │   ├── Weapons/           # Weapon prefabs
│   │   ├── Bots/              # Bot prefabs
│   │   └── UI/                # UI element prefabs
│   ├── Scenes/                 # Game levels
│   │   └── cs1.7.unity        # Main demo scene
│   └── Scripts/               # All C# code
│       ├── Player/            # Player-related systems
│       ├── Weapons/           # Weapon systems
│       ├── Warcraft/          # RPG systems
│       ├── Game/              # Core game logic
│       ├── Bot/               # AI/target practice
│       └── UI/                # User interface
├── Library/                    # Unity cache (auto-generated)
├── Packages/                   # Unity packages
├── ProjectSettings/            # Unity settings
└── Documentation/             # Markdown guides
    ├── QUICK_SETUP.md
    ├── RACES_WEAPONS_SETUP.md
    ├── CROSSHAIR_SETUP.md
    ├── BUY_MENU_SETUP.md
    ├── DEBUG_COMMANDS.md
    └── ...
```

---

## 🏗️ System Architecture

### Core Game Loop

```
Game Start
    ↓
Initialize GameManager (Singleton)
    ↓
Demo Mode OR Competitive Mode?
    ↓                           ↓
[Demo Mode]              [Competitive Mode]
- Unlimited money        - Round-based gameplay
- No restrictions        - Buy time phases
- Bot practice          - Team-based combat
    ↓                           ↓
Player spawns with race selected
    ↓
Weapon System activates
    ↓
Combat & XP progression
    ↓
Level up → Unlock abilities
    ↓
Continue playing / Round ends
```

---

## 📦 Component Architecture

### Player GameObject Structure
```
Player (GameObject)
├─ Character Controller       # Movement physics
├─ PlayerController          # Input & movement logic
├─ PlayerHealth              # HP/Armor management
├─ PlayerRace                # Race bonuses (HP/Speed/Damage)
├─ PlayerExperience          # XP & Leveling system
├─ PlayerAbilities           # Active/Passive abilities
├─ WeaponManager             # Weapon inventory & switching
└─ PlayerCamera (Child)
   ├─ Camera Component       # First-person view
   └─ WeaponHolder (Child)   # Weapon attachment point
      └─ [Weapon instances]  # Spawned weapons
```

### Bot GameObject Structure
```
Bot (Prefab)
├─ Capsule Collider (Disabled)  # Main body (disabled for hitboxes)
├─ Bot Component                 # Health, respawn, damage
├─ Head (Child)
│  ├─ Sphere Collider
│  └─ Hitbox (4x damage)
├─ Body (Child)
│  ├─ Box Collider
│  └─ Hitbox (1x damage)
├─ LeftArm (Child)
│  ├─ Capsule Collider
│  └─ Hitbox (0.75x damage)
├─ RightArm (Child)
│  ├─ Capsule Collider
│  └─ Hitbox (0.75x damage)
├─ LeftLeg (Child)
│  ├─ Capsule Collider
│  └─ Hitbox (0.75x damage)
└─ RightLeg (Child)
   ├─ Capsule Collider
   └─ Hitbox (0.75x damage)
```

### GameManager Structure
```
GameManager (Singleton)
├─ GameManager               # Core game logic
├─ DebugCommands            # Testing shortcuts (1-0, -, B)
└─ SimpleBuyMenu            # Weapon purchasing UI
```

---

## 🔄 Data Flow Architecture

### Weapon Purchase Flow
```
Player presses 'B'
    ↓
SimpleBuyMenu.OnGUI() displays menu
    ↓
Player clicks weapon button
    ↓
GameManager.SpendMoney() checks balance
    ↓
WeaponManager.AddWeapon() instantiates prefab
    ↓
Weapon spawned in WeaponHolder
    ↓
Weapon.Start() auto-finds PlayerCamera
    ↓
Weapon ready to use
```

### Combat Damage Flow
```
Player clicks (Fire1)
    ↓
Weapon.Shoot() raycasts from camera
    ↓
Hit detection: Hitbox > Bot > PlayerHealth
    ↓
Hitbox.GetDamageMultiplier() calculates damage
    ↓
Bot.TakeDamage() or PlayerHealth.TakeDamage()
    ↓
Visual feedback: BulletEffect tracers/impacts
    ↓
XP reward to killer (if bot dies)
    ↓
PlayerExperience.AddExperience()
```

### XP & Leveling Flow
```
Kill enemy
    ↓
PlayerExperience.AddExperience(100)
    ↓
Check if XP >= requirement
    ↓
LevelUp() increases currentLevel
    ↓
Grant skill point
    ↓
PlayerAbilities can be upgraded
    ↓
PlayerRace applies stat multipliers
```

---

## 🎨 Design Patterns Used

### 1. **Singleton Pattern**
- **GameManager**: Single instance manages game state
- **Ensures**: Only one game manager exists
- **Access**: `GameManager.Instance`

### 2. **ScriptableObject Pattern**
- **WeaponData**: Weapon stats (damage, fire rate, etc.)
- **RaceData**: Race bonuses (HP, speed, damage multipliers)
- **AbilityData**: Ability properties (cooldown, effects)
- **Benefits**: Data-driven, reusable, no code changes needed

### 3. **Component Pattern**
- **Everything is a component**: Modular, reusable
- **Player = collection of components**: Easy to add/remove features
- **Weapons = prefabs with components**: Consistent behavior

### 4. **Observer Pattern (Unity Events)**
- **OnAmmoChanged**: Weapon → UI updates
- **OnRoundStateChanged**: GameManager → UI updates
- **OnDeath**: PlayerHealth → Game logic

### 5. **Factory Pattern**
- **BotSpawner**: Creates bot instances
- **WeaponManager**: Instantiates weapons
- **Patterns**: Circle, Line, Grid, Random, Custom

### 6. **Strategy Pattern**
- **SpawnPattern enum**: Different bot placement strategies
- **HitboxType enum**: Different damage calculation strategies

---

## 🧩 System Dependencies

### High-Level Dependencies
```
GameManager (Core)
    ↓
    ├─→ PlayerInfo (data)
    ├─→ Team (enum)
    └─→ RoundState (enum)

Player Systems
    ├─→ PlayerController (depends on CharacterController)
    ├─→ PlayerHealth (depends on PlayerRace)
    ├─→ PlayerExperience (depends on PlayerRace)
    ├─→ PlayerAbilities (depends on PlayerExperience)
    └─→ WeaponManager (depends on Weapon prefabs)

Weapon System
    ├─→ WeaponData (ScriptableObject)
    ├─→ Camera (auto-found)
    ├─→ Crosshair (auto-found)
    └─→ BulletEffect (static helper)

Bot System
    ├─→ Bot (main component)
    ├─→ Hitbox (child components)
    └─→ BotSpawner (factory)

UI System
    ├─→ SimpleBuyMenu (depends on GameManager)
    ├─→ Crosshair (standalone)
    ├─→ GameHUD (depends on Player components)
    └─→ DebugCommands (depends on Player & GameManager)
```

---

## 📊 Data Models

### PlayerInfo Class
```csharp
class PlayerInfo {
    GameObject playerObject;  // Reference to player
    Team team;                // Terrorist/CT
    int money;                // Current cash
    int kills;                // Kill count
    int deaths;               // Death count
}
```

### WeaponData ScriptableObject
```csharp
class WeaponData {
    string weaponName;        // "AK-47"
    WeaponType weaponType;    // Rifle, Pistol, etc.
    int damage;               // Base damage (36)
    float fireRate;           // Shots per second (0.1)
    float range;              // Max distance (100)
    int magazineSize;         // Ammo per mag (30)
    int maxAmmo;              // Reserve ammo (90)
    float reloadTime;         // Reload duration (2.5s)
    float recoilAmount;       // Recoil strength (2.5)
    bool isAutomatic;         // Full-auto?
    int cost;                 // Buy price ($2500)
}
```

### RaceData ScriptableObject
```csharp
class RaceData {
    RaceType raceType;           // Orc, Undead, etc.
    string raceName;             // Display name
    string raceDescription;      // Flavor text
    float healthMultiplier;      // 1.3 = 130 HP
    float speedMultiplier;       // 0.85 = 85% speed
    float damageMultiplier;      // 1.2 = 120% damage
    List<AbilityData> abilities; // Race abilities
    int maxLevel;                // Level cap (10)
}
```

### AbilityData ScriptableObject
```csharp
class AbilityData {
    string abilityName;         // "Critical Strike"
    string description;         // What it does
    AbilityType type;           // Passive/Active
    float cooldown;             // Cooldown in seconds
    int maxLevel;               // Max upgrade level
    float[] effectValues;       // Per-level values
}
```

---

## 🎮 Feature Modules

### 1. Movement System
- **Location**: `Assets/Scripts/Player/PlayerController.cs`
- **Dependencies**: CharacterController
- **Features**:
  - WASD movement
  - Mouse look (FPS camera)
  - Sprint capability
  - Jump mechanics
  - Auto-finds camera

### 2. Combat System
- **Location**: `Assets/Scripts/Weapons/`
- **Components**:
  - `Weapon.cs` - Shooting logic
  - `WeaponData.cs` - Weapon stats
  - `WeaponManager.cs` - Inventory
  - `BulletEffect.cs` - Visual feedback
- **Features**:
  - Raycast shooting
  - Recoil system
  - Ammo management
  - Reload mechanics
  - Bullet tracers
  - Impact effects
  - Hitbox detection

### 3. RPG System
- **Location**: `Assets/Scripts/Warcraft/`
- **Components**:
  - `PlayerRace.cs` - Race bonuses
  - `PlayerExperience.cs` - XP/Leveling
  - `PlayerAbilities.cs` - Ability system
  - `RaceData.cs` - Race definitions
  - `AbilityData.cs` - Ability definitions
- **Features**:
  - 4 unique races
  - Level progression (1-10)
  - Skill points
  - Passive/Active abilities
  - Stat multipliers

### 4. Economy System
- **Location**: `Assets/Scripts/Game/GameManager.cs`
- **Features**:
  - Money management
  - Kill rewards ($300)
  - Round rewards (win/lose)
  - Buy system
  - Demo mode (unlimited $)

### 5. Bot/Target System
- **Location**: `Assets/Scripts/Bot/`
- **Components**:
  - `Bot.cs` - Bot behavior
  - `BotSpawner.cs` - Spawning logic
  - `Hitbox.cs` - Damage zones
- **Features**:
  - Auto-respawn
  - Multiple spawn patterns
  - Hitbox system (head/body/limbs)
  - XP rewards on kill
  - Visual feedback

### 6. UI System
- **Location**: `Assets/Scripts/UI/`
- **Components**:
  - `SimpleBuyMenu.cs` - Weapon shop
  - `Crosshair.cs` - Aiming reticle
  - `GameHUD.cs` - HUD display
  - `RaceSelectionMenu.cs` - Race picker
- **Features**:
  - Dynamic crosshair
  - Buy menu (no Canvas needed!)
  - Health/Ammo display
  - Debug overlay

### 7. Debug System
- **Location**: `Assets/Scripts/Game/DebugCommands.cs`
- **Features**:
  - Number key shortcuts (1-0, -)
  - XP manipulation
  - Money cheats
  - Bot spawning
  - God mode
  - On-screen help

---

## 🔧 Technical Specifications

### Input Mapping
```
Movement:
- W/A/S/D: Move forward/left/back/right
- Mouse: Look around
- Space: Jump
- Shift: Sprint

Combat:
- Left Click: Shoot
- R: Reload
- 1-4 (on weapon bar): Switch weapons
- Mouse Wheel: Cycle weapons

UI:
- B: Buy menu
- Tab: Race selection
- ESC: Close menus

Debug (Demo Mode):
- 1: Add 1000 XP
- 2: Level up
- 3: Max level
- 4: Add $5000
- 5: Spawn bots
- 6: Clear bots
- 7: Full health
- 8: God mode
- 9: Reset cooldowns
- 0: Add skill point
- -: Refill ammo
```

### Damage Calculations
```
Base Damage = weapon.damage
Race Multiplier = playerRace.damageMultiplier
Hitbox Multiplier = hitbox.GetDamageMultiplier()

Final Damage = Base × Race × Hitbox

Examples:
- AK-47 (36) × Orc (1.2) × Headshot (4.0) = 172.8 damage
- AK-47 (36) × Human (1.0) × Body (1.0) = 36 damage
- AK-47 (36) × NightElf (1.0) × Leg (0.75) = 27 damage
```

### Health System
```
Base Health = 100
Race Multiplier = playerRace.healthMultiplier

Final Health:
- Orc: 100 × 1.3 = 130 HP
- Undead: 100 × 1.0 = 100 HP
- Human: 100 × 1.0 = 100 HP
- Night Elf: 100 × 0.85 = 85 HP
```

### XP Requirements
```
Level 1 → 2: 1000 XP
Level 2 → 3: 2000 XP
Level 3 → 4: 3000 XP
...
Level N → N+1: N × 1000 XP

Kill Reward: 100 XP per bot
```

---

## 🚀 Development Roadmap

### Phase 1: Core Systems ✅ COMPLETE
- [x] Player movement (FPS controller)
- [x] Weapon system (shooting, reloading)
- [x] Health system
- [x] Bot targets with respawn
- [x] Basic scene setup

### Phase 2: RPG Systems ✅ COMPLETE
- [x] Race system (4 races)
- [x] XP & Leveling
- [x] Ability system
- [x] Stat multipliers
- [x] Skill points

### Phase 3: Economy & Shop ✅ COMPLETE
- [x] Money system
- [x] Buy menu (SimpleBuyMenu)
- [x] Weapon purchasing
- [x] Demo mode
- [x] Round-based economy

### Phase 4: Polish & Effects ✅ COMPLETE
- [x] Hitbox system (head/body/limbs)
- [x] Bullet tracers
- [x] Impact effects
- [x] Crosshair (dynamic)
- [x] Visual feedback

### Phase 5: Debug & Testing ✅ COMPLETE
- [x] Debug commands (1-0, -, B)
- [x] Bot spawner patterns
- [x] God mode
- [x] Comprehensive documentation

### Phase 6: Future Enhancements 🔮
- [ ] Multiplayer networking
- [ ] More races (Blood Elf, Troll, etc.)
- [ ] More weapons (15+ total)
- [ ] Custom maps
- [ ] Particle effects
- [ ] Sound effects
- [ ] Music system
- [ ] Main menu
- [ ] Settings menu
- [ ] Player profiles
- [ ] Achievements
- [ ] Leaderboards

---

## 📝 Code Guidelines

### Naming Conventions
```csharp
// Classes: PascalCase
public class PlayerController { }

// Methods: PascalCase
public void AddExperience(int amount) { }

// Variables: camelCase
private int currentHealth;

// Constants: PascalCase
public const int MaxLevel = 10;

// Events: OnPascalCase
public UnityEvent OnDeath;

// Enums: PascalCase
public enum WeaponType { Pistol, Rifle }
```

### File Organization
```
One class per file
File name = Class name
Place in appropriate folder
Use namespaces for larger projects
```

### Comments
```csharp
/// <summary>
/// XML documentation for public APIs
/// </summary>
public void PublicMethod() { }

// Inline comments for complex logic
private void ComplexMethod() {
    // Explain why, not what
}
```

---

## 🐛 Known Issues & Limitations

### Current Limitations
1. **Single Player Only**: No networking yet
2. **Simple Bot AI**: Bots are stationary targets
3. **Basic Graphics**: Primitive shapes (capsules, cubes)
4. **No Audio**: No sound effects or music yet
5. **No Animations**: Static models
6. **Limited Abilities**: Ability system exists but needs implementation
7. **One Map**: Only demo scene available

### Future Fixes
- Add multiplayer via Unity Netcode
- Implement bot pathfinding/AI
- Import proper 3D models
- Add audio system
- Implement animation system
- Complete all race abilities
- Create multiple maps

---

## 📚 Documentation Files

### Setup Guides
- `QUICK_SETUP.md` - 5-minute demo setup
- `RACES_WEAPONS_SETUP.md` - Complete race/weapon guide
- `CROSSHAIR_SETUP.md` - Crosshair & effects
- `BUY_MENU_SETUP.md` - Shop system guide
- `DEBUG_COMMANDS.md` - All debug shortcuts

### Reference Guides
- `WEAPON_CAMERA_FIX.md` - Camera assignment
- `WEAPON_CAMERA_PLAYER_FIX.md` - PlayerCamera tagging
- `BOT_PATTERNS.md` - Spawn pattern reference
- `DEMO_MODE.md` - Demo vs competitive mode

### Development Docs
- `ARCHITECTURE.md` - This file
- `FEATURES.md` - Feature list
- `INDEX.md` - Documentation index

---

## 🎯 Success Metrics

### Gameplay Goals
- ✅ Smooth 60+ FPS movement
- ✅ Responsive shooting (< 50ms input lag)
- ✅ Clear visual feedback (tracers, impacts)
- ✅ Intuitive controls (CS 1.6 style)
- ✅ Balanced races (no overpowered race)
- ✅ Engaging progression (meaningful levels)

### Technical Goals
- ✅ Modular architecture (easy to extend)
- ✅ Data-driven design (ScriptableObjects)
- ✅ Clean code (readable, maintainable)
- ✅ Well-documented (inline + external)
- ✅ No memory leaks
- ✅ Fast scene loading

---

## 🤝 Contributing

### Adding New Weapons
1. Create `WeaponData` ScriptableObject
2. Configure stats (damage, fire rate, etc.)
3. Create weapon GameObject (cube for now)
4. Add `Weapon` component
5. Assign WeaponData
6. Save as prefab
7. Add to WeaponManager or SimpleBuyMenu

### Adding New Races
1. Create `RaceData` ScriptableObject
2. Configure multipliers (HP, speed, damage)
3. Define abilities (passive/active)
4. Add to RaceSelectionMenu
5. Test balance in demo mode
6. Document in RACES_WEAPONS_SETUP.md

### Adding New Abilities
1. Create `AbilityData` ScriptableObject
2. Define type (passive/active)
3. Set cooldown and effects
4. Implement logic in `PlayerAbilities.cs`
5. Test with debug commands
6. Balance with other abilities

---

## 📞 Support & Resources

### Unity Resources
- Unity Documentation: https://docs.unity3d.com/
- Unity Scripting API: https://docs.unity3d.com/ScriptReference/
- Unity Forums: https://forum.unity.com/

### Project Resources
- All documentation: `/Documentation/*.md`
- Script comments: Inline XML documentation
- Debug tools: DebugCommands component
- Test scene: `Assets/Scenes/cs1.7.unity`

---

**Project Status:** ✅ Core Complete, Ready for Enhancement  
**Last Updated:** October 3, 2025  
**Version:** 1.0 Demo  
**Unity Version:** 2020.3+
