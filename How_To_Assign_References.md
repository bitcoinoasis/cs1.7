# 🔗 How to Assign References in Unity - Step-by-Step

This guide shows you exactly how to link GameObjects to script components in Unity.

---

## Method 1: Drag and Drop (Easiest!)

### Step-by-Step Example: Linking WeaponHolder to WeaponManager

1. **Select the Player** in Hierarchy (left panel)
   - Click on "Player" GameObject

2. **Look at Inspector** (right panel)
   - You'll see all components attached to Player
   - Scroll down to find "Weapon Manager (Script)"

3. **Find the empty field**
   - You'll see: `Weapon Holder [None (Transform)]`
   - This field is empty and needs a reference

4. **Drag from Hierarchy to Inspector**
   - In Hierarchy, find WeaponHolder (it's under Player → PlayerCamera → WeaponHolder)
   - **Click and hold** on WeaponHolder
   - **Drag** it to the "Weapon Holder" field in Inspector
   - **Release** mouse button
   - The field should now show: `Weapon Holder [WeaponHolder (Transform)]`

5. ✅ **Done!** The reference is assigned

---

## Method 2: Using the Circle Icon (Picker)

### Alternative Way to Assign References

1. **Select Player** in Hierarchy

2. **In Inspector**, find the field you want to fill
   - Example: "Weapon Holder" field in Weapon Manager

3. **Click the small circle icon** (⊙) on the right side of the field
   - A popup window will appear showing all available objects

4. **Double-click** on the object you want to assign
   - Example: Double-click "WeaponHolder"

5. ✅ **Done!** Reference assigned

---

## Visual Reference Guide

### What You're Looking For:

```
HIERARCHY (Left Side)          INSPECTOR (Right Side)
─────────────────────         ─────────────────────────────
▼ Player                      Player
  ▼ PlayerCamera              ┌───────────────────────────┐
    • WeaponHolder            │ Transform                 │
                              │ Character Controller      │
                              │ Player Controller         │
                              │   Player Camera [None]    │ ← Fill this
                              │ Player Health             │
                              │ Weapon Manager            │
                              │   Weapon Holder [None]    │ ← Fill this
                              └───────────────────────────┘
```

---

## Common Fields to Assign

### 1. PlayerController → Player Camera

**What:** Link the camera so the player can look around

**Steps:**
1. Select **Player** in Hierarchy
2. Find **Player Controller** component in Inspector
3. Find field: **Player Camera**
4. Drag **PlayerCamera** (child of Player) into this field

**Result:** `Player Camera [PlayerCamera (Camera)]`

---

### 2. WeaponManager → Weapon Holder

**What:** Link where weapons will be positioned

**Steps:**
1. Select **Player** in Hierarchy
2. Find **Weapon Manager** component in Inspector
3. Find field: **Weapon Holder**
4. Drag **WeaponHolder** (grandchild: Player → PlayerCamera → WeaponHolder) into this field

**Result:** `Weapon Holder [WeaponHolder (Transform)]`

---

### 3. Weapon → Player Camera

**What:** Link camera to weapon for shooting raycast

**Steps:**
1. Select your **Weapon** prefab or GameObject
2. Find **Weapon** component in Inspector
3. Find field: **Player Camera**
4. Drag **PlayerCamera** into this field

**Result:** `Player Camera [PlayerCamera (Camera)]`

---

### 4. Weapon → Weapon Data

**What:** Link the weapon configuration (ScriptableObject)

**Steps:**
1. Select your **Weapon** GameObject
2. Find **Weapon** component in Inspector
3. Find field: **Weapon Data**
4. From **Project** window (bottom), drag your WeaponData asset into this field

**Result:** `Weapon Data [AK47_Data (WeaponData)]`

---

### 5. BotSpawner → Bot Prefab

**What:** Link the bot prefab to spawn

**Steps:**
1. Select **BotSpawner** in Hierarchy
2. Find **Bot Spawner** component in Inspector
3. Find field: **Bot Prefab**
4. From **Project** window, navigate to Assets/Prefabs
5. Drag your **Bot** prefab into this field

**Result:** `Bot Prefab [TargetBot (GameObject)]`

---

## Understanding the Hierarchy Tree

When dragging objects, you need to understand the parent-child relationship:

```
Player                           ← Root GameObject
├─ PlayerCamera                  ← Child of Player
│  └─ WeaponHolder               ← Child of PlayerCamera (Grandchild of Player)
```

**To select WeaponHolder:**
- Click the ▶ arrow next to Player to expand
- Click the ▶ arrow next to PlayerCamera to expand
- Now you can see and drag WeaponHolder

---

## Field Types and What They Accept

### Transform Fields
- Accept: Any GameObject
- Example: Weapon Holder field

### Camera Fields
- Accept: Only GameObjects with Camera component
- Example: Player Camera field

### GameObject Fields
- Accept: Any GameObject or Prefab
- Example: Bot Prefab field

### ScriptableObject Fields
- Accept: Only ScriptableObject assets (from Project window)
- Example: Weapon Data, Race Data, Ability Data

---

## Troubleshooting

### "The field shows [None] and won't accept my drag"

**Cause:** You're dragging the wrong type of object

**Solution:**
- Check what type the field expects (shown in parentheses)
- Example: `Weapon Holder (Transform)` expects a GameObject
- Example: `Weapon Data (WeaponData)` expects a ScriptableObject asset

---

### "I can't find the object to drag"

**Cause:** Object is hidden in hierarchy

**Solution:**
- Click the ▶ arrows to expand parent objects
- Example: WeaponHolder is inside PlayerCamera, which is inside Player

---

### "The field cleared itself after I assigned it"

**Cause:** The object was deleted or the reference broke

**Solution:**
- Make sure the object still exists in Hierarchy
- Reassign the reference
- For prefabs, make sure you're using the prefab from Project window, not from Hierarchy

---

### "I assigned it but it still says [None]"

**Cause:** You might have assigned to a prefab instance, not the actual prefab

**Solution:**
- If working with prefabs, make sure to:
  1. Assign references
  2. Click "Apply" at the top of Inspector to save to prefab
  3. Or work with the prefab directly in Project window

---

## Quick Visual Test

After assigning, the field should change from:

❌ **Before:** `Weapon Holder [None (Transform)]`

✅ **After:** `Weapon Holder [WeaponHolder (Transform)]`

If you see the object name in the field, it's assigned correctly!

---

## Practice Exercise

Try assigning these to get comfortable:

1. ✅ Player Camera → PlayerController
2. ✅ WeaponHolder → WeaponManager
3. ✅ Bot Prefab → BotSpawner
4. ✅ Weapon Data → Weapon component

After each assignment, the field should show the object name instead of "None"!

---

**Need Help?** 
- Look for fields showing `[None]` - those need to be filled
- Use the circle icon (⊙) if you can't find what to drag
- Check the field type in parentheses to know what it accepts

**You've got this!** 🎮
