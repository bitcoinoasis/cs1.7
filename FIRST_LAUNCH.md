# First Launch Instructions

## Prerequisites
- Unity installed (2020.3 or newer recommended)
- This project opened in Unity

## Create Your First Test Scene

### 1. Create New Scene
- File → New Scene
- Choose "3D" template
- Save as "TestScene" in Assets/Scenes/

### 2. Create Ground
- GameObject → 3D Object → Plane
- Position: (0, 0, 0)
- Scale: (10, 1, 10)

### 3. Create GameManager
- GameObject → Create Empty
- Name: "GameManager"
- Add Component → GameManager
  - ✅ Check "Demo Mode"
- Add Component → DebugCommands
  - ✅ Check "Enable Debug Commands"

### 4. Create Player (Simplified)
- GameObject → 3D Object → Capsule
- Name: "Player"
- Tag: "Player"
- Position: (0, 1, 0)
- Add these components:
  - Character Controller (height: 2, radius: 0.5)
  - PlayerController
  - PlayerHealth
  - PlayerRace
  - PlayerExperience
  - PlayerAbilities
  - WeaponManager

### 5. Add Camera to Player
- Right-click Player → Camera
- Name: "PlayerCamera"
- Position: (0, 0.6, 0) relative to player
- Tag: "MainCamera"
- Delete the old Main Camera in scene

### 6. Create WeaponHolder
- Right-click PlayerCamera → Create Empty
- Name: "WeaponHolder"
- Position: (0.3, 0.4, 0.5) relative to camera

### 7. Save Scene
- File → Save Scene
- Ctrl+S (Cmd+S on Mac)

## Launch!

Press the **Play button** ▶️ at the top of Unity Editor

## What to Expect

✅ You can move (WASD)
✅ You can look around (Mouse)
✅ Debug commands shown on screen
✅ Press F keys to test features

## If You See Errors

**"No camera available"**
- Make sure camera is tagged "MainCamera"

**"Can't find Player"**
- Make sure player is tagged "Player"

**"Component missing"**
- Add all required scripts to player

## Next Steps After Launch

1. Press F7 - Get full health
2. Movement test - WASD + Mouse
3. Press F3 - Max level
4. Press Tab - Try to select race (if you've created RaceData)

For bots, you'll need to:
1. Create a bot prefab (see QUICK_SETUP.md)
2. Add BotSpawner
3. Press F5 to spawn

## Can't Launch?

Check:
- [ ] Unity project opened
- [ ] Scene saved
- [ ] Player tagged correctly
- [ ] Camera tagged correctly
- [ ] All scripts compiled (no errors in Console)

---

**Once you have Unity open, follow these steps and hit Play!** ▶️
