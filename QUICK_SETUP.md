# Quick Demo Setup Guide

Get up and running in 5 minutes for testing!

## Step 1: Create GameManager (30 seconds)

1. Create empty GameObject: `GameObject → Create Empty`
2. Name it "GameManager"
3. Add scripts:
   - `Add Component → GameManager`
   - `Add Component → DebugCommands`
   - `Add Component → SimpleBuyMenu`
4. In GameManager component:
   - ✅ Check **"Demo Mode"** (this is KEY!)
5. In DebugCommands component:
   - ✅ Check **"Enable Debug Commands"**
6. In SimpleBuyMenu component:
   - Leave "Weapons For Sale" empty for now (we'll add weapons later)

## Step 2: Create Player (2 minutes)

1. Create Player GameObject:
   ```
   GameObject → 3D Object → Capsule
   Name: "Player"
   Tag: "Player"
   ```

2. Add Components to Player:
   - Character Controller (height: 2, radius: 0.5)
   - PlayerController
   - PlayerHealth
   - PlayerRace
   - PlayerExperience
   - PlayerAbilities
   - WeaponManager

3. Create Camera Child:
   ```
   Right-click Player → Camera
   Name: "PlayerCamera"
   Position: (0, 0.6, 0)
   Tag: "MainCamera" ← IMPORTANT!
   ```
   
   **Make sure the Tag is set to "MainCamera":**
   - Select PlayerCamera in Hierarchy
   - Look at top of Inspector
   - Tag dropdown → MainCamera
   - This ensures weapons find the correct camera!

4. Create WeaponHolder Child:
   ```
   Right-click PlayerCamera → Create Empty
   Name: "WeaponHolder"
   Position: (0.3, 0.4, 0.5)
   ```

5. **Link WeaponHolder to WeaponManager:**
   - Select the **Player** GameObject in Hierarchy
   - In **Inspector**, find the **Weapon Manager** component
   - Find the field called "Weapon Holder"
   - **Drag** the **WeaponHolder** (it's under PlayerCamera) into this field
   - OR click the circle icon and select WeaponHolder

   **Note:** PlayerController automatically finds the camera - no need to assign it!

   **Visual Guide:**
   ```
   Inspector → Weapon Manager Component
   ┌─────────────────────────────┐
   │ Weapon Manager (Script)     │
   │ ─────────────────────────   │
   │ Weapon Holder  [None]  ⊙   │ ← Drag WeaponHolder here
   │ Weapon Prefabs  List        │
   └─────────────────────────────┘
   ```

## Step 3: Create Target Bots (1 minute)

1. Create Bot Prefab:
   ```
   GameObject → 3D Object → Capsule
   Name: "Bot"
   Scale: (1, 2, 1)
   Add Component → Bot
   ```
   - Set Health: 100
   - Set Max Health: 100
   - ✅ Respawn On Death: true
   - Drag to Project folder to create prefab

2. Create Bot Spawner:
   ```
   GameObject → Create Empty
   Name: "BotSpawner"
   Add Component → BotSpawner
   ```
   - Assign Bot prefab to "Bot Prefab" slot
   - Number Of Bots: 10
   - Spawn Pattern: Circle
   - Spawn Radius: 10
   - ✅ Spawn On Start: true
   - Bots Respawn: true

## Step 4: Create Basic Scene (1 minute)

1. Create Ground:
   ```
   GameObject → 3D Object → Plane
   Scale: (10, 1, 10)
   Position: (0, 0, 0)
   ```

2. Position Player:
   ```
   Player Position: (0, 1, 0)
   ```

3. Position Bot Spawner:
   ```
   BotSpawner Position: (0, 0, 15)
   ```
   This puts bots 15 units in front of player

## Step 5: Test! (30 seconds)

1. Press Play
2. You'll see debug commands on screen (top-left)
3. Try these:
   - **Move**: WASD
   - **Look**: Mouse
   - **1**: Add 1000 XP
   - **2**: Level up once
   - **3**: Max level instantly
   - **4**: Add $5000
   - **5**: Spawn bots in circle
   - **6**: Clear all bots
   - **7**: Full health
   - **8**: Toggle god mode
   - **9**: Reset ability cooldowns
   - **0**: Add skill point
   - **- (Minus)**: Refill ammo
   - **B**: Open buy menu (unlimited money!)
   - **R**: Reload weapon
   - **Tab**: Choose race (if you have races set up)

## Optional: Add Races (5 minutes)

1. Create Race ScriptableObject:
   ```
   Right-click in Project → Create → Warcraft → Race Data
   ```

2. Configure Race (Example - Orc):
   - Race Type: Orc
   - Race Name: "Orc"
   - Health Multiplier: 1.2
   - Speed Multiplier: 0.9
   - Damage Multiplier: 1.15

3. Assign to Player:
   - Select Player GameObject
   - Find PlayerRace component
   - Assign race to "Current Race"

## Optional: Add Weapons (5 minutes)

1. Create Weapon Data:
   ```
   Right-click in Project → Create → Weapons → Weapon Data
   ```

2. Configure (Example - AK47):
   - Weapon Name: "AK-47"
   - Weapon Type: Rifle
   - Damage: 36
   - Fire Rate: 0.1
   - Magazine Size: 30
   - Max Ammo: 90
   - Is Automatic: ✅ true

3. Create Weapon GameObject:
   ```
   GameObject → 3D Object → Cube (simple weapon mesh)
   Scale: (0.1, 0.1, 0.5)
   Add Component → Weapon
   ```
   - Assign Weapon Data
   - **Player Camera: Leave EMPTY** (auto-detects!)
   - Save as Prefab in Assets/Prefabs/

4. Assign to Player:
   - Select Player
   - In WeaponManager → Weapon Prefabs
   - Click + to add slot
   - Drag your weapon prefab into the slot

5. **Add to Buy Menu** (Important!):
   - Select GameManager
   - Find SimpleBuyMenu component
   - Under "Weapons For Sale" → Click + to add slot
   - Set Size to match number of weapons you want (e.g., 1)
   - Expand Element 0:
     - Weapon Prefab: Drag your weapon prefab here
     - Price: 2500 (or any price you want)

**Now press B in-game to open the buy menu!**

## Troubleshooting

**Player not moving?**
- Check Character Controller is attached
- Check PlayerController script is enabled

**Can't shoot bots?**
- Make sure Bot has a Collider
- Check weapon range in WeaponData

**Debug commands not showing?**
- Check "Enable Debug Commands" on DebugCommands script

**Buy menu not opening?**
- Press B key
- Check GameManager has SimpleBuyMenu component
- Check GameManager has "Demo Mode" enabled
- Make sure you've added at least one weapon to "Weapons For Sale"

**Buy menu shows but no weapons?**
- Select GameManager → SimpleBuyMenu
- Add weapons to "Weapons For Sale" list
- Make sure weapon prefabs are assigned

**Race selection not working?**
- Create RaceData ScriptableObjects first
- Add to RaceSelectionMenu's available races

**Weapon shooting in wrong direction?**
- Make sure PlayerCamera has **Tag: "MainCamera"**
- Steps: Select PlayerCamera → Inspector → Tag → MainCamera
- The weapon needs to find the player's camera, not another camera

**Multiple cameras in scene causing issues?**
- Only ONE camera should be tagged "MainCamera"
- Check all cameras in scene (Window → Hierarchy)
- Remove "MainCamera" tag from any extra cameras

## Quick Test Checklist

- ✅ Can move around (WASD + Mouse)
- ✅ Can spawn bots (F5)
- ✅ Can shoot (Left Click)
- ✅ Bots take damage and respawn
- ✅ Gain XP when killing bots
- ✅ Can level up (F1, F2, F3)
- ✅ Can open buy menu (B)
- ✅ Debug commands visible on screen

## What to Do Next

1. Create more weapon types
2. Add abilities to races
3. Create multiple race options
4. Build a proper map
5. Add UI elements
6. Test all abilities
7. Balance races and weapons
8. Add visual effects

---

**You're ready to test!** 🎮

Remember: This is Demo Mode for testing. To play actual rounds:
- Uncheck "Demo Mode" in GameManager
- Create spawn points with tags "TSpawn" and "CTSpawn"
- Register players properly
