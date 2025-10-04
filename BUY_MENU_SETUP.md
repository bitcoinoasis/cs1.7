# Buy Menu Setup Guide

Quick guide to get the weapon shop working!

## The Problem

The original `BuyMenu.cs` requires complex UI Canvas setup. **SimpleBuyMenu.cs** works immediately with no UI setup needed!

---

## Quick Setup (1 minute)

### Step 1: Add SimpleBuyMenu to GameManager
```
1. Select "GameManager" in Hierarchy
2. Add Component → SimpleBuyMenu
3. Leave "Weapons For Sale" empty for now
```

### Step 2: Create a Weapon First
```
Follow QUICK_SETUP.md "Optional: Add Weapons" section to create:
- WeaponData ScriptableObject
- Weapon GameObject with Weapon component
- Save as Prefab
```

### Step 3: Add Weapon to Shop
```
1. Select GameManager
2. Find SimpleBuyMenu component
3. Under "Weapons For Sale":
   - Size: 1 (or more for multiple weapons)
   - Element 0:
     - Weapon Prefab: [Drag your weapon prefab]
     - Price: 2500 (or whatever you want)
```

### Step 4: Test!
```
1. Press Play
2. Press B to open buy menu
3. Click weapon to buy
4. Press ESC or B to close
5. Weapon should appear in your hands!
```

---

## Controls

- **B** - Open/Close buy menu
- **ESC** - Close buy menu
- **Left Click** - Buy weapon (in menu)
- **R** - Reload weapon

---

## Features

✅ **No Canvas Required** - Uses Unity's OnGUI system
✅ **Automatic Money Display** - Shows your current cash
✅ **Color-Coded Buttons** - Green = can afford, Red = too expensive
✅ **Demo Mode** - Unlimited money when enabled
✅ **Multiple Weapons** - Add as many as you want

---

## Adding Multiple Weapons

Example setup for 4 weapons:

```
GameManager → SimpleBuyMenu:
├─ Weapons For Sale (Size: 4)
│  ├─ Element 0: Pistol ($650)
│  ├─ Element 1: AK-47 ($2500)
│  ├─ Element 2: M4A1 ($3100)
│  └─ Element 3: AWP ($4750)
```

Just increase "Size" and fill in each element!

---

## Troubleshooting

**Menu doesn't open?**
- Check SimpleBuyMenu component is on GameManager
- Press B key (not Tab)
- Make sure Demo Mode is enabled in GameManager

**Menu opens but is empty?**
- Add weapons to "Weapons For Sale" list
- Make sure Size > 0
- Assign weapon prefabs to each element

**Can't afford weapons?**
- Enable Demo Mode in GameManager for unlimited money
- Or use F4 in-game to add money
- Prices are set per weapon in SimpleBuyMenu

**Weapon doesn't appear after buying?**
- Check WeaponManager component on Player
- Make sure Weapon Holder is assigned
- Check Console for error messages

**Menu is too small/big?**
- Edit SimpleBuyMenu.cs line 18:
  ```csharp
  private Rect menuRect = new Rect(
      Screen.width / 2 - 200,  // X position
      Screen.height / 2 - 200, // Y position
      400,                      // Width (change this)
      400                       // Height (change this)
  );
  ```

---

## Visual Example

```
┌────────────────────────────────┐
│    === WEAPON SHOP ===         │
│                                 │
│    Money: $16000               │
│                                 │
│  ┌──────────────────────────┐  │
│  │  AK-47 - $2500           │  │ ← Green (can afford)
│  └──────────────────────────┘  │
│                                 │
│  ┌──────────────────────────┐  │
│  │  M4A1 - $3100            │  │ ← Green (can afford)
│  └──────────────────────────┘  │
│                                 │
│  ┌──────────────────────────┐  │
│  │  AWP - $4750             │  │ ← Green (can afford)
│  └──────────────────────────┘  │
│                                 │
│  ┌──────────────────────────┐  │
│  │  Close (ESC)             │  │ ← Red close button
│  └──────────────────────────┘  │
└────────────────────────────────┘
```

---

## Demo Mode vs Normal Mode

### Demo Mode (Testing):
- ✅ Unlimited money
- ✅ Can buy anytime
- ✅ No restrictions
- **Enable in GameManager component**

### Normal Mode (Actual Gameplay):
- Money is limited
- Can only buy during "Buy Time" at round start
- Must earn money by killing enemies
- **Uncheck Demo Mode in GameManager**

---

## Quick Weapon Price Guide (CS 1.6 Style)

| Weapon        | Suggested Price |
|---------------|-----------------|
| Knife         | Free            |
| Pistol        | $500 - $800     |
| Desert Eagle  | $650            |
| Shotgun       | $1700           |
| SMG           | $1500 - $2500   |
| AK-47         | $2500           |
| M4A1          | $3100           |
| AWP           | $4750           |

---

**You're ready to buy weapons!** 🛒💰
