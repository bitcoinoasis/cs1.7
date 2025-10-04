# Demo Mode Features Summary

## Overview

The CS 1.6 Warcraft mod now includes a **Demo/Practice Mode** perfect for testing all features without gameplay restrictions.

## What's New for Demo Mode

### 1. Static Target Bots (`Bot.cs`)
- **Health system**: Configurable HP, takes damage, dies
- **Visual feedback**: Changes color when hit
- **Auto-respawn**: Respawns after delay
- **XP rewards**: Awards XP to player on kill
- **Customizable**: Set health, colors, respawn delay

### 2. Bot Spawner System (`BotSpawner.cs`)
- **Multiple spawn patterns**:
  - Circle: Bots in a circle around spawner
  - Line: Bots in a straight line
  - Grid: Bots in a grid formation
  - Random: Random positions within radius
  - Custom Points: Use custom transform positions
- **Dynamic spawning**: F5 to spawn, F6 to clear
- **Configurable**: Number of bots, spacing, health
- **Visual gizmos**: See spawn area in editor

### 3. Demo Mode in GameManager
- **Unlimited money**: Always have $16,000
- **No round timers**: No buy phase or round limits
- **No round end**: Practice indefinitely
- **Always active**: Buy menu always available
- **Toggle easily**: Single checkbox in inspector

### 4. Debug Commands System (`DebugCommands.cs`)
Complete F-key command system for instant testing:

#### XP & Leveling
- **F1**: Add 1000 XP
- **F2**: Level up once (adds XP needed for next level)
- **F3**: Instant max level (level 10)
- **F10**: Add 1 skill point

#### Health & Survival
- **F7**: Full health + max armor
- **F8**: Toggle god mode (999,999 HP)

#### Abilities
- **F9**: Reset all ability cooldowns instantly

#### Weapons
- **F11**: Refill current weapon ammo (adds 999)

#### Bot Control
- **F5**: Spawn bots (via BotSpawner)
- **F6**: Clear all bots

#### On-Screen Display
- Shows all commands on screen (top-left)
- Yellow text, always visible
- Easy reference while playing

### 5. Updated Buy Menu
- Works anytime in demo mode (not just buy phase)
- Shows unlimited money
- No purchase restrictions
- Press B to toggle

## How Demo Mode Works

### GameManager Changes
```csharp
public bool demoMode = true;  // Enable demo features
```

When demo mode is enabled:
1. **Start()**: No round system starts, game goes straight to Active state
2. **Update()**: Round timer disabled
3. **GetPlayerMoney()**: Always returns max money
4. **SpendMoney()**: Always returns true (free purchases)
5. **CheckRoundEnd()**: Skipped (no round ends)
6. **RegisterPlayer()**: Starts with unlimited money

### BuyMenu Changes
```csharp
// Can buy anytime in demo mode
bool canBuy = GameManager.Instance.demoMode || 
              GameManager.Instance.currentState == RoundState.BuyTime;
```

### Weapon System Updates
```csharp
// Now detects and damages both players and bots
Bot targetBot = hit.collider.GetComponent<Bot>();
if (targetBot != null)
{
    targetBot.TakeDamage(weaponData.damage, gameObject);
}
```

## Usage Scenarios

### Scenario 1: Test Race Abilities
1. Enable demo mode
2. Choose race (Tab)
3. Press F3 to max level
4. Press F10 multiple times for skill points
5. Upgrade abilities in race menu
6. Press F9 to reset cooldowns
7. Test abilities repeatedly

### Scenario 2: Practice Aim
1. Enable demo mode
2. Press F5 to spawn bots in circle
3. Buy weapons (B menu)
4. Practice shooting
5. Bots respawn automatically
6. Gain real XP from kills

### Scenario 3: Test Weapon Balance
1. Enable demo mode
2. Buy multiple weapons (B menu, unlimited money)
3. Test damage on bots (100 HP each)
4. Switch weapons (1-4 keys)
5. Press F11 to refill ammo instantly
6. Compare fire rate, recoil, damage

### Scenario 4: Demo All Features
1. Enable demo mode
2. Press F3 (max level)
3. Press F10 x10 (get skill points)
4. Press B (buy all weapons)
5. Press F5 (spawn targets)
6. Press F9 (reset cooldowns)
7. Show off full game features!

## Files Added

```
Assets/Scripts/
├── Bot/
│   ├── Bot.cs              # Target bot with health
│   └── BotSpawner.cs       # Spawn bots in patterns
└── Game/
    └── DebugCommands.cs    # F-key debug commands
```

## Files Modified

- `GameManager.cs`: Added demo mode flag and logic
- `BuyMenu.cs`: Allow buying in demo mode
- `Weapon.cs`: Added bot damage detection

## Configuration

### GameManager Settings
```
Demo Mode: ✅ (checked for practice)
Demo Mode Unlimited Money: 16000
```

### DebugCommands Settings
```
Enable Debug Commands: ✅
XP Per Command: 1000
Levels Per Command: 1
```

### BotSpawner Settings
```
Bot Prefab: [Assign your bot prefab]
Number Of Bots: 10
Spawn Pattern: Circle/Line/Grid/Random/CustomPoints
Spawn Radius: 10
Bot Spacing: 2
Bot Health: 100
Bots Respawn: ✅
Spawn On Start: ✅ (optional)
```

### Bot Settings
```
Health: 100
Max Health: 100
Respawn On Death: ✅
Respawn Delay: 3
Normal Color: Red
Hit Color: Yellow
Hit Flash Duration: 0.1
```

## Benefits

1. **Fast Testing**: No need to play full rounds
2. **Unlimited Resources**: Test everything without grinding
3. **Quick Iteration**: Instant level ups, money, ammo
4. **Aim Practice**: Static targets with respawn
5. **Ability Testing**: Reset cooldowns instantly
6. **Demo Ready**: Show off all features quickly
7. **Balance Testing**: Test damage, health, abilities
8. **No Restrictions**: Break the rules for testing

## Switching Modes

### Demo Mode → Normal Game Mode
1. Uncheck "Demo Mode" in GameManager
2. Game will start round system
3. Limited money ($800 start)
4. Round timers active
5. Buy phase restrictions apply

### Normal → Demo Mode
1. Check "Demo Mode" in GameManager
2. Unlimited money
3. No timers
4. Buy anytime
5. Debug commands available

## Tips for Demo Sessions

1. **Start with F3**: Max level immediately
2. **Use F10**: Get skill points for abilities
3. **F5 for targets**: Spawn bots to shoot
4. **F9 between tests**: Reset ability cooldowns
5. **B for weapons**: Try all weapons quickly
6. **F8 if dying**: God mode for uninterrupted testing
7. **F6 to reset**: Clear bots and reposition

## Integration with Existing Systems

Demo mode works seamlessly with:
- ✅ Race system (all races, abilities)
- ✅ XP and leveling
- ✅ Weapon system
- ✅ Health and damage
- ✅ Ability cooldowns
- ✅ Buy menu
- ✅ All UI elements

No conflicts, just adds convenience!

---

**Perfect for: Demos, Testing, Practice, Balancing, Showcases** 🎯
