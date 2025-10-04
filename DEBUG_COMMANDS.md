# Debug Commands Reference

Complete guide to all debug/cheat commands for testing!

## Why Number Keys Instead of F-Keys?

On macOS, F-keys are used for system functions (brightness, volume, etc.). Using number keys is more convenient and doesn't conflict with macOS shortcuts!

---

## All Debug Commands

### XP & Leveling
| Key | Command | Description |
|-----|---------|-------------|
| **1** | Add XP | Adds 1000 XP instantly |
| **2** | Level Up | Levels up once (adds exact XP needed) |
| **3** | Max Level | Instantly reach maximum level |
| **0** | Skill Point | Add 1 skill point for abilities |

### Money
| Key | Command | Description |
|-----|---------|-------------|
| **4** | Add Money | Adds $5000 to your account |
| **B** | Buy Menu | Open weapon shop (unlimited $ in demo mode) |

### Bots
| Key | Command | Description |
|-----|---------|-------------|
| **5** | Spawn Bots | Spawns all bots in pattern |
| **6** | Clear Bots | Removes all bots from scene |

### Health
| Key | Command | Description |
|-----|---------|-------------|
| **7** | Full Health | Restores health and armor to max |
| **8** | God Mode | Toggle invincibility (999999 HP) |

### Abilities
| Key | Command | Description |
|-----|---------|-------------|
| **9** | Reset Cooldowns | All abilities available instantly |

### Weapons
| Key | Command | Description |
|-----|---------|-------------|
| **-** | Refill Ammo | Adds 999 reserve ammo |
| **R** | Reload | Normal reload (not a cheat) |

---

## Quick Reference (In-Game)

When playing, you'll see this in the top-left corner:

```
=== DEBUG COMMANDS ===
1 - Add 1000 XP
2 - Level Up
3 - Max Level
4 - Add $5000
5 - Spawn Bots
6 - Clear Bots
7 - Full Health
8 - Toggle God Mode
9 - Reset Cooldowns
0 - Add Skill Point
- (Minus) - Refill Ammo
B - Buy Menu (Unlimited $)
R - Reload Weapon
```

---

## Enabling/Disabling Commands

### To Disable Debug Commands:
1. Select **GameManager** in Hierarchy
2. Find **DebugCommands** component
3. **Uncheck** "Enable Debug Commands"
4. The on-screen display will disappear

### To Enable Again:
1. Same process, but **check** the box

---

## Common Testing Scenarios

### Testing Races:
```
1. Press 3 (max level)
2. Press Tab (open race selection)
3. Choose a race
4. Notice stat changes (HP, speed, damage)
```

### Testing Weapons:
```
1. Press 4 (add money)
2. Press B (buy menu)
3. Buy weapon
4. Press - (refill ammo if needed)
5. Shoot and test
```

### Testing Combat:
```
1. Press 5 (spawn bots)
2. Press 8 (god mode ON)
3. Test weapons/abilities without dying
4. Press 8 again (god mode OFF)
5. Test with normal health
```

### Testing Abilities:
```
1. Press 3 (max level)
2. Press 0 repeatedly (add skill points)
3. Upgrade abilities
4. Press 9 (reset cooldowns to test instantly)
```

### Speed Testing:
```
1. Press 5 (spawn bots)
2. Press - (infinite ammo)
3. Kill bots to test XP system
4. Press 6 (clear) and 5 (respawn) to repeat
```

---

## Notes for macOS Users

### F-Key Workarounds (if you really want F-keys):
1. Hold **Fn** key + F1-F11
2. Or change macOS settings:
   - System Preferences → Keyboard
   - Check "Use F1, F2, etc. as standard function keys"

### But Number Keys are Better!
- No conflicts with system functions
- Easier to reach while gaming
- Works on both keyboard types (with/without numpad)

---

## Customizing Key Bindings

Want to change the keys? Edit `DebugCommands.cs`:

```csharp
// Example: Change "1" to "P" for Add XP
if (Input.GetKeyDown(KeyCode.P))
{
    AddXP();
}

// Example: Change "5" to "LeftBracket" for Spawn Bots
if (Input.GetKeyDown(KeyCode.LeftBracket))
{
    SpawnBots();
}
```

Common KeyCode options:
- Alpha1-0 (number row)
- Keypad1-0 (numpad)
- LeftBracket, RightBracket
- Semicolon, Quote
- Period, Comma
- P, O, I, U, Y (letter keys)

---

## Demo Mode vs Debug Commands

### Demo Mode (GameManager):
- Unlimited money in buy menu
- Can buy anytime
- No round restrictions

### Debug Commands:
- Quick testing tools
- Instant XP, levels, money
- Bot spawning control
- Health manipulation

**Use both together for maximum testing power!** 🚀

---

## Production Build

### Before releasing your game:
1. **Disable Debug Commands**:
   - Uncheck "Enable Debug Commands" on GameManager
   - Or remove DebugCommands component entirely

2. **Disable Demo Mode**:
   - Uncheck "Demo Mode" on GameManager

3. **Optional - Remove Script**:
   - Delete `DebugCommands.cs` from project
   - This prevents cheating in released game

---

**Happy Testing!** 🎮✨
