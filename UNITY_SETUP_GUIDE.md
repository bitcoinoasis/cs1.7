# Unity Setup Guide - CS 1.6 Warcraft Mod

## 🎯 Quick Start (5 Minutes)

### **Step 1: Scene Setup**

1. **Open Unity** and load your `cs1.7.unity` scene
2. **Create Manager GameObjects:**
   ```
   Hierarchy:
   ├── GameManagers (Empty GameObject)
   │   ├── GameModeManager
   │   ├── EconomySystem
   │   └── BotSpawner
   ```

3. **Add Components:**
   - GameModeManager → Add `GameModeManager.cs`
   - EconomySystem → Add `EconomySystem.cs`

---

### **Step 2: Player Setup**

1. **Select Player GameObject** (tag it as "Player")
2. **Add Required Components:**
   - `PlayerRace.cs`
   - `AbilitySystem.cs`
   
3. **Verify Existing Components:**
   - ✅ `PlayerHealth.cs` (already exists)
   - ✅ `PlayerController.cs` (already exists)
   - ✅ `Weapon.cs` (already exists)

---

### **Step 3: Bot Setup**

1. **Create Bot Prefab:**
   - Duplicate Player GameObject
   - Rename to "Bot"
   - Remove PlayerController scripts (keep health/weapon)
   - **Add:** `BotAI.cs`
   - **Add:** `UnityEngine.AI.NavMeshAgent`

2. **Configure BotAI:**
   - Difficulty: Medium (or Easy for testing)
   - Team ID: 0 (or 1 for CT)
   - Bot Name: "Bot 1", "Bot 2", etc.

3. **Bake NavMesh:**
   - Window → AI → Navigation
   - Select ground/floor objects
   - Mark as "Navigation Static"
   - Click "Bake"

---

### **Step 4: UI Setup**

#### **Create Canvas Hierarchy:**
```
Canvas (Screen Space - Overlay)
├── RaceSelectionPanel (Assign to RaceSelectionUI)
├── GameHUD
│   ├── HealthBar
│   ├── AmmoDisplay
│   ├── AbilityHUD (3 ability slots)
│   └── KillFeed
├── BuyMenu (Assign to BuyMenuUI)
└── Scoreboard (Assign to ScoreboardUI)
```

#### **Add UI Scripts:**
1. **Create UI Manager GameObject:**
   - Add `RaceSelectionUI.cs`
   - Add `AbilityHUD.cs`
   - Add `BuyMenuUI.cs`
   - Add `ScoreboardUI.cs`
   - Add `KillFeedUI.cs`

2. **Drag References in Inspector:**
   - RaceSelectionUI → Assign allRaces[] (8 race assets)
   - BuyMenuUI → Assign weapon arrays (pistols, SMGs, etc.)
   - AbilityHUD → Assign ability panel references

---

### **Step 5: Data Asset Assignment**

#### **Race Data:**
1. Select `RaceSelectionUI` in Hierarchy
2. Inspector → All Races field
3. **Drag all 8 race assets** from `Assets/Data/Races/`:
   - Race_Orc.asset
   - Race_Undead.asset
   - Race_Human.asset
   - Race_NightElf.asset
   - Race_BloodElf.asset
   - Race_Troll.asset
   - Race_Dwarf.asset
   - Race_Celestial.asset

#### **Weapon Data:**
1. Select `BuyMenuUI` in Hierarchy
2. Inspector → Weapon arrays
3. **Assign weapons by category:**
   - **Pistols[]** → USP, Glock, Deagle, P250
   - **SMGs[]** → MP5, MP7, P90, MAC-10
   - **Rifles[]** → AK47, M4A4, M4A1S, Galil, FAMAS, AUG
   - **Snipers[]** → AWP, Scout, G3SG1
   - **Shotguns[]** → Nova, XM1014

---

### **Step 6: GameModeManager Configuration**

1. **Select GameModeManager GameObject**
2. **Inspector Settings:**
   - Current Game Mode: Bomb Defusal (or Deathmatch for testing)
   - Round Time: 115
   - Buy Time: 20
   - Max Rounds: 30
   - Rounds To Win: 16

3. **If Bomb Defusal:**
   - Create 2 GameObjects for bomb sites (A and B)
   - Drag to "Bomb Sites" array

4. **If Gun Game:**
   - Assign weapon progression in "Gun Game Weapons[]"

---

### **Step 7: Test Play**

#### **Quick Test Checklist:**
1. **Press Play**
2. **Race Selection should appear** (if RaceSelectionUI is active)
   - Select Orc or Undead
   - Click Confirm
   
3. **Press 'B'** to open Buy Menu
   - Purchase a weapon (should have $800)
   - Close menu
   
4. **Press 'Tab'** to see Scoreboard
   - Should show player name and stats
   
5. **Spawn Bots:**
   - Create simple bot spawner script OR
   - Manually place 4-5 bot prefabs in scene

---

## 🔧 Common Issues & Fixes

### **Issue: "NullReferenceException in BotAI"**
**Fix:** Make sure bot has:
- NavMeshAgent component
- PlayerHealth component
- Ground has baked NavMesh

### **Issue: "Buy Menu doesn't show weapons"**
**Fix:** 
- Verify weapon data assets are assigned in Inspector
- Check BuyMenuUI → Weapon arrays are not empty

### **Issue: "Abilities don't trigger"**
**Fix:**
- Check PlayerRace.cs has race assigned
- Verify AbilitySystem.cs is on player
- Ensure ability data assets are linked to race

### **Issue: "Money doesn't update"**
**Fix:**
- Ensure EconomySystem is in scene
- Call `EconomySystem.Instance.RegisterPlayer(playerGameObject)` on spawn

### **Issue: "Scoreboard is blank"**
**Fix:**
- Make sure players have BotAI component (for team assignment)
- Verify EconomySystem is tracking player stats

---

## 🎨 UI Prefab Creation (Optional but Recommended)

### **Create Reusable UI Prefabs:**

1. **Weapon Button Prefab:**
   ```
   WeaponButton (Button)
   ├── Icon (Image)
   ├── WeaponName (TextMeshProUGUI)
   ├── Price (TextMeshProUGUI)
   └── Stats (TextMeshProUGUI)
   ```

2. **Ability Slot Prefab:**
   ```
   AbilitySlot (Image - background)
   ├── Icon (Image)
   ├── Name (TextMeshProUGUI)
   ├── Level (TextMeshProUGUI)
   ├── Cooldown (TextMeshProUGUI)
   └── CooldownOverlay (Image - radial fill)
   ```

3. **Player Row Prefab (Scoreboard):**
   ```
   PlayerRow (Horizontal Layout)
   ├── Name (TextMeshProUGUI)
   ├── Race (TextMeshProUGUI)
   ├── Kills (TextMeshProUGUI)
   ├── Deaths (TextMeshProUGUI)
   ├── KD (TextMeshProUGUI)
   ├── Money (TextMeshProUGUI)
   └── Ping (TextMeshProUGUI)
   ```

4. **Kill Feed Entry Prefab:**
   ```
   KillFeedEntry (Horizontal Layout)
   ├── Killer (TextMeshProUGUI)
   ├── Weapon (TextMeshProUGUI)
   ├── HeadshotIcon (Image - optional)
   └── Victim (TextMeshProUGUI)
   ```

---

## 🚀 Advanced: Bot Spawner Script

Create `Assets/Scripts/BotSpawner.cs`:

```csharp
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab;
    public Transform[] spawnPoints;
    public int botsPerTeam = 4;
    
    void Start()
    {
        SpawnBots();
    }
    
    void SpawnBots()
    {
        for (int i = 0; i < botsPerTeam * 2; i++)
        {
            Transform spawn = spawnPoints[i % spawnPoints.Length];
            GameObject bot = Instantiate(botPrefab, spawn.position, spawn.rotation);
            
            BotAI botAI = bot.GetComponent<BotAI>();
            botAI.teamID = i < botsPerTeam ? 0 : 1;
            botAI.botName = $"Bot {i + 1}";
            botAI.difficulty = (BotAI.BotDifficulty)(i % 4); // Vary difficulty
            
            // Register with economy
            if (EconomySystem.Instance != null)
            {
                EconomySystem.Instance.RegisterPlayer(bot);
            }
        }
    }
}
```

---

## 📋 Testing Workflow

### **Basic Testing (No Bots):**
1. Solo race selection test
2. Buy menu functionality
3. Ability cooldown display
4. Economy money tracking

### **With Bots:**
1. Bot navigation and pathfinding
2. Bot combat and shooting
3. Bot buy phase behavior
4. Kill feed updates
5. Scoreboard updates

### **Full Game Test:**
1. Start Bomb Defusal round
2. Buy weapons (bots should too)
3. Engage in combat
4. Check kill rewards
5. Verify round win/loss
6. Test consecutive rounds

---

## ✅ Final Checklist

Before declaring game "playable":

- [ ] Player can select race
- [ ] Abilities display in HUD
- [ ] Can purchase weapons in buy phase
- [ ] Bots spawn and navigate
- [ ] Bots shoot at enemies
- [ ] Economy tracks money correctly
- [ ] Kill feed shows kills
- [ ] Scoreboard displays stats
- [ ] Rounds start and end
- [ ] Win/loss bonuses awarded

---

## 🎯 Next: Visual Polish (Phase 7)

After basic functionality works:
1. Add particle effects to abilities
2. Create weapon muzzle flashes
3. Add sound effects
4. Implement UI animations
5. Polish visual feedback

---

**Estimated Setup Time:** 30-45 minutes (including UI creation)

Good luck! 🚀 The foundation is solid - just needs Unity wiring!
