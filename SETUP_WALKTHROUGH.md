# 🎮 INTERACTIVE SETUP GUIDE - Follow Along!

Let me guide you step-by-step through setting up your CS 1.6 Warcraft mod in Unity.

---

## ✅ STEP 1: Open Unity (Do this first!)

1. **Launch Unity Hub** on your Mac
2. Click **"Open"** or **"Add"**
3. Navigate to: `/Users/syedshah/cs1.7`
4. Click **"Select Folder"** / **"Open"**
5. Wait for Unity to open and compile scripts (this may take 1-2 minutes first time)

**✓ You'll know it's ready when:**
- Unity Editor window is open
- No compilation errors in Console (bottom of screen)
- You see the Assets folder in Project window

---

## ✅ STEP 2: Create a New Scene

1. In Unity, go to: **File → New Scene**
2. Choose **"3D"** template (or "Basic 3D" if available)
3. Click **"Create"**
4. Save immediately: **File → Save As...**
5. Name it: **"DemoScene"**
6. Location: Save in `Assets/Scenes/` folder
7. Click **"Save"**

**✓ You'll know it's ready when:**
- Scene name shows "DemoScene" at the top
- You see a scene with a Main Camera and Directional Light

---

## ✅ STEP 3: Create the Ground

1. **Right-click** in the **Hierarchy** window (left side)
2. Select: **3D Object → Plane**
3. In the **Inspector** (right side), set:
   - **Name:** Ground
   - **Position:** X=0, Y=0, Z=0
   - **Scale:** X=10, Y=1, Z=10

**✓ You'll know it's ready when:**
- You see a large flat plane in the Scene view
- It's centered at world origin

---

## ✅ STEP 4: Create the GameManager

1. **Right-click** in **Hierarchy**
2. Select: **Create Empty**
3. Name it: **"GameManager"**
4. In **Inspector**, click **"Add Component"**
5. Type: **"GameManager"** and select it
6. ⭐ **IMPORTANT:** Check the box for **"Demo Mode"** ✅
7. Click **"Add Component"** again
8. Type: **"DebugCommands"** and select it
9. Check the box for **"Enable Debug Commands"** ✅

**✓ You'll know it's ready when:**
- GameManager appears in Hierarchy
- Both scripts show in Inspector with checkboxes ticked

---

## ✅ STEP 5: Create the Player

### Part A: Create Player Object

1. **Right-click** in **Hierarchy**
2. Select: **3D Object → Capsule**
3. Name it: **"Player"**
4. At the top of Inspector, set **Tag** dropdown to: **"Player"**
   - (If "Player" tag doesn't exist, click "Add Tag", create it, then assign it)
5. Set **Position:** X=0, Y=1, Z=0

### Part B: Add Player Components

Click **"Add Component"** and add each of these (one at a time):

1. **Character Controller**
   - After adding, set: Height = 2, Radius = 0.5

2. **Player Controller** (type in search)
3. **Player Health**
4. **Player Race**
5. **Player Experience**
6. **Player Abilities**
7. **Weapon Manager**

**✓ You'll know it's ready when:**
- Player capsule is visible in scene
- Inspector shows all 7 components
- Tag is set to "Player"

---

## ✅ STEP 6: Add Camera to Player

1. **Right-click** on **Player** in Hierarchy
2. Select: **Camera**
3. Name it: **"PlayerCamera"**
4. In Inspector, set:
   - **Position:** X=0, Y=0.6, Z=0 (relative to player)
   - **Tag:** MainCamera
5. **Delete** the old "Main Camera" from Hierarchy (select it and press Delete)

**✓ You'll know it's ready when:**
- PlayerCamera is child of Player (indented under it)
- Only one camera exists in scene
- It's tagged as MainCamera

---

## ✅ STEP 7: Add WeaponHolder to Camera

1. **Right-click** on **PlayerCamera** in Hierarchy
2. Select: **Create Empty**
3. Name it: **"WeaponHolder"**
4. In Inspector, set **Position:** X=0.3, Y=0.4, Z=0.5 (relative to camera)

**✓ You'll know it's ready when:**
- WeaponHolder is child of PlayerCamera
- Position is set correctly

---

## ✅ STEP 8: Link References

### Link WeaponManager to WeaponHolder:

1. Select **Player** in Hierarchy
2. In Inspector, find **Weapon Manager** component
3. Find the **"Weapon Holder"** field
4. **Drag** the **WeaponHolder** object from Hierarchy into this field

**✓ You'll know it's ready when:**
- Weapon Holder field shows "WeaponHolder (Transform)"

---

## ✅ STEP 9: Save Everything!

1. Press **Ctrl+S** (Windows) or **Cmd+S** (Mac)
2. Go to: **File → Save**

---

## 🎮 STEP 10: TEST IT! Press Play!

1. Click the **Play button** ▶️ at the top-center of Unity
2. Click in the **Game** window to start

**You should be able to:**
- ✅ Move with **WASD**
- ✅ Look around with **Mouse**
- ✅ See debug commands on screen (top-left)

**Try these commands:**
- **F7** - Full health
- **F3** - Max level (you'll see level up messages in console)

---

## 🎯 STEP 11: Add Target Bots (Optional but Recommended!)

### Part A: Create Bot Prefab

1. **Right-click** in Hierarchy
2. Select: **3D Object → Capsule**
3. Name it: **"TargetBot"**
4. Set **Position:** X=0, Y=1, Z=10 (in front of player)
5. Set **Scale:** X=1, Y=2, Z=1
6. Click **"Add Component"**
7. Type: **"Bot"** and select it
8. In the Bot component, set:
   - Health: 100
   - Max Health: 100
   - ✅ Check "Respawn On Death"
   - Respawn Delay: 3
   - Normal Color: Red (click color box, choose red)
   - Hit Color: Yellow

### Part B: Make it a Prefab

1. In **Project** window (bottom), navigate to **Assets/Prefabs**
2. **Drag** the **TargetBot** from Hierarchy into the Prefabs folder
3. You now have a bot prefab! You can delete the one in Hierarchy or keep it.

### Part C: Create Bot Spawner

1. **Right-click** in Hierarchy
2. Select: **Create Empty**
3. Name it: **"BotSpawner"**
4. Set **Position:** X=0, Y=0, Z=15
5. Click **"Add Component"**
6. Type: **"BotSpawner"** and select it
7. In the BotSpawner component:
   - **Bot Prefab:** Drag your TargetBot prefab from Assets/Prefabs here
   - **Number Of Bots:** 10
   - **Spawn Pattern:** Circle (from dropdown)
   - **Spawn Radius:** 10
   - ✅ Check "Spawn On Start"
   - **Bot Health:** 100
   - ✅ Check "Bots Respawn"

**✓ You'll know it's ready when:**
- Bot prefab is in Prefabs folder
- BotSpawner has the prefab assigned
- Settings are configured

---

## 🎮 STEP 12: TEST WITH BOTS!

1. Click **Play** ▶️ again
2. You should see 10 bots spawn in a circle around you!

**Try this:**
- Press **F5** to spawn bots (if they don't auto-spawn)
- Press **F6** to clear bots
- Walk toward bots (WASD)
- Look at them (Mouse)

**Note:** You can't shoot yet because we need to add weapons!

---

## ⚔️ STEP 13: Add a Basic Weapon (Optional)

### Create Weapon Data

1. In **Project** window, navigate to **Assets/Scripts/Weapons**
2. **Right-click** in empty space
3. Select: **Create → Weapons → Weapon Data**
4. Name it: **"AK47_Data"**
5. Select it and in Inspector, set:
   - Weapon Name: "AK-47"
   - Weapon Type: Rifle
   - Damage: 36
   - Fire Rate: 0.1
   - Range: 100
   - Magazine Size: 30
   - Max Ammo: 90
   - ✅ Is Automatic: true
   - Recoil Amount: 2

### Create Weapon Object

1. **Right-click** in Hierarchy
2. Select: **3D Object → Cube**
3. Name it: **"AK47"**
4. Set **Scale:** X=0.1, Y=0.1, Z=0.5 (to look like a gun shape)
5. Click **"Add Component"**
6. Type: **"Weapon"** and select it
7. In the Weapon component:
   - **Weapon Data:** Drag the AK47_Data you created
   - **Player Camera:** Drag PlayerCamera from Hierarchy

### Make Weapon Prefab

1. In **Project**, go to **Assets/Prefabs**
2. **Drag** the **AK47** from Hierarchy into Prefabs folder
3. Delete AK47 from Hierarchy

### Assign to Player

1. Select **Player** in Hierarchy
2. Find **Weapon Manager** component
3. Click the **+** button on "Weapon Prefabs" list
4. **Drag** the AK47 prefab into the new slot

---

## 🎮 FINAL TEST - Full Gameplay!

1. Click **Play** ▶️
2. Click in Game window

**You can now:**
- ✅ Move (WASD)
- ✅ Look (Mouse)
- ✅ Shoot bots (Left Click)
- ✅ See bots flash yellow when hit
- ✅ Gain XP when they die
- ✅ See XP in console

**Try all debug commands:**
- **F1** - Add XP
- **F2** - Level up
- **F3** - Max level
- **F5** - Spawn more bots
- **F6** - Clear bots
- **F7** - Full health
- **F9** - Reset cooldowns
- **F11** - Refill ammo

---

## 🎨 Make It Look Better (Optional)

### Add Colors

1. In **Project**, go to **Assets/Materials**
2. **Right-click** → **Create → Material**
3. Name it "GroundMaterial"
4. Set color (click white box next to Albedo)
5. Drag material onto Ground plane

Create materials for:
- Ground (green/gray)
- Player (blue)
- Bots (red)

---

## 🐛 Troubleshooting

**Console shows errors?**
- Read the error message
- Usually means a script reference is missing
- Check all components are added

**Can't move?**
- Check Character Controller is on Player
- Check PlayerController script has no errors

**Can't see?**
- Check camera is child of Player
- Check camera is tagged MainCamera

**Bots don't spawn?**
- Check BotSpawner has prefab assigned
- Check "Spawn On Start" is checked
- Press F5 manually

**Can't shoot?**
- Check weapon prefab is in Weapon Manager list
- Check weapon has WeaponData assigned
- Check camera reference on weapon

---

## ✅ Setup Complete Checklist

- [ ] Unity project open
- [ ] Scene created and saved
- [ ] Ground plane created
- [ ] GameManager with Demo Mode ON
- [ ] Player with all components
- [ ] PlayerCamera as child of Player
- [ ] WeaponHolder linked
- [ ] Bot prefab created
- [ ] BotSpawner configured
- [ ] Weapon created and assigned
- [ ] Game plays in Unity Editor
- [ ] Can move and look around
- [ ] Can spawn and shoot bots
- [ ] Debug commands working

---

## 🎉 YOU'RE DONE!

You now have a fully functional CS 1.6 Warcraft mod demo!

**Next Steps:**
1. Read [DEMO_MODE.md](DEMO_MODE.md) for all features
2. Create races (see README.md)
3. Create abilities
4. Build a better map
5. Have fun testing!

---

**Need help?** Check the other documentation files:
- QUICK_SETUP.md - Quick reference
- DEMO_MODE.md - Demo features
- README.md - Complete docs
- BOT_PATTERNS.md - Bot spawning
- FEATURES.md - Feature list

**Enjoy your game!** 🎮✨
