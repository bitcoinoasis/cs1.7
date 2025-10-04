# CS 1.7 Quick Start - 3 Steps to Play

## Step 1: Setup Resources (One-Time)
**In Unity Editor:**
1. Go to menu: **Tools → CS 1.7 → Setup Resources Folders**
2. Wait for "Resources folder setup complete!" message
3. Verify: **Tools → CS 1.7 → Verify Resources Setup**

✅ This copies all race/weapon/ability data to Resources folder

---

## Step 2: Create Ground Plane
**In Unity Hierarchy:**
1. Right-click → **3D Object → Plane**
2. Name it "Ground"
3. Inspector: Position (0, 0, 0), Scale (10, 1, 10)

✅ Provides a floor for players to walk on

---

## Step 3: Press Play!
That's it! Everything else is created automatically:

✅ GameManagers (GameModeManager, EconomySystem, BotSpawner)
✅ UI Canvas (Crosshair, AbilityHUD, BuyMenu, Scoreboard, KillFeed)
✅ Player with camera
✅ 10 spawn points (5 per team)

---

## What You'll See in Console
```
[SceneInitializer] Starting automatic scene setup...
[SceneInitializer] GameManagers created...
[SceneInitializer] UI Canvas created...
[SceneInitializer] Player created...
[SceneInitializer] Created 10 spawn points...
[SceneInitializer] Scene setup complete!
```

---

## Next: Spawn Bots
**Option A - In Hierarchy:**
1. Find: **GameManagers → BotSpawner**
2. Right-click → **Spawn All Bots**

**Option B - Debug Command:**
Type `spawnbots` in console (if DebugCommands active)

---

## Game Controls

### Movement
- **WASD** - Move
- **Space** - Jump
- **Shift** - Sprint
- **Mouse** - Look around

### Combat
- **Left Click** - Shoot
- **R** - Reload
- **G** - Drop weapon

### Abilities
- **Q** - Ultimate ability
- **E** - Ability 2
- **F** - Ability 3

### UI
- **B** - Buy menu
- **Tab** - Scoreboard
- **~** - Console (debug commands)

---

## Change Game Mode
**In Hierarchy:**
1. Find: **GameManagers → GameModeManager**
2. Inspector: Change **Current Game Mode**:
   - Bomb Defusal (CS classic)
   - Team Deathmatch (team vs team)
   - Deathmatch (free-for-all)
   - Gun Game (level up weapons)

---

## Troubleshooting

### "No race data found!"
→ Run **Tools → CS 1.7 → Setup Resources Folders**

### Bots don't spawn
→ Right-click BotSpawner → **Spawn All Bots**

### Player falls through ground
→ Add a **Plane** GameObject at position (0, 0, 0)

### Can't see anything
→ Check Player has a **Main Camera** child object

---

## Documentation Files
- **AUTO_SETUP.md** - Complete automatic setup guide
- **UNITY_SETUP_GUIDE.md** - Manual setup (old method)
- **GAME_COMPLETE.md** - Full feature list
- **FINAL_BUILD_REPORT.md** - Technical architecture

---

## 8 Playable Races
Each race has 3 unique abilities:

1. **Orc** - Critical Strike, Bash, Reincarnation
2. **Human** - Devotion Aura, Invisibility, Teleport
3. **Undead** - Vampiric Aura, Levitation, Unholy Aura
4. **Night Elf** - Evasion, Blink, Thorns Aura
5. **Blood Elf** - Mana Shield, Arcane Missiles, Mana Burn
6. **Troll** - Berserker, Regeneration, Axe Mastery
7. **Dwarf** - Stone Skin, Shield Bash, Heroic Strike
8. **Celestial** - Holy Light, Divine Shield, Resurrection

---

## 20 Weapons Available
- **Pistols:** USP-S, Glock-18, Desert Eagle, P250, Five-Seven
- **SMGs:** MP5-SD, UMP-45, P90
- **Rifles:** AK-47, M4A4, M4A1-S, FAMAS, Galil
- **Snipers:** AWP, SSG 08
- **Shotguns:** Nova, XM1014
- **Heavy:** M249, Negev
- **Melee:** Knife

---

## Need Help?
Check the **AUTO_SETUP.md** file for detailed troubleshooting and advanced configuration options.

**Repository:** https://github.com/bitcoinoasis/cs1.7
