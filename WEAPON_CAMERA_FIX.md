# Weapon Setup - Camera Assignment Fix

## The Problem

When setting up a weapon, you might see:
**"Type mismatch: Cannot assign GameObject to Camera"**

This happens when you try to drag the **PlayerCamera GameObject** into the **Player Camera** field on the Weapon component.

---

## ✅ The Solution: Leave It Empty!

**Good news:** The Weapon script automatically finds the camera! You don't need to assign anything.

### Steps:
1. Create your weapon GameObject
2. Add the **Weapon** component
3. Assign your **Weapon Data** ScriptableObject
4. **Leave "Player Camera" EMPTY** ← This is the key!
5. Save as prefab

**That's it!** The script will automatically find the camera when the game starts.

---

## How It Works

The Weapon script has auto-detection code:

```csharp
// Auto-finds the main camera if not assigned
if (playerCamera == null)
{
    playerCamera = Camera.main;
}
```

It looks for:
1. Any camera tagged as **"MainCamera"**
2. If not found, any camera in the scene

---

## If You Really Want to Assign It Manually

If you insist on assigning it yourself, here's how:

### Method 1: Select the Camera Component
1. Select your weapon in Hierarchy
2. Find the **Weapon** component in Inspector
3. Look at the **Player Camera** field
4. Click the **circle icon (⊙)** next to it
5. In the popup window, look for **"PlayerCamera (Camera)"**
6. **NOT** "PlayerCamera (GameObject)" - it must say **(Camera)**!
7. Select the one that says **(Camera)**

### Method 2: Drag the Camera Component
1. In Hierarchy, expand **Player → PlayerCamera**
2. You'll see the Camera component icon in the Inspector
3. Drag the **Camera component** (the little camera icon in Inspector)
4. Drop it into the **Player Camera** field on your weapon

---

## Common Mistakes

❌ **Wrong:** Dragging the PlayerCamera GameObject from Hierarchy
- This gives you a GameObject, not a Camera component

✅ **Correct:** Either leave empty (recommended) or select the Camera component

---

## Troubleshooting

**Camera still not working?**
- Check that PlayerCamera has the **"MainCamera"** tag
- Steps: Select PlayerCamera → Inspector → Tag → MainCamera

**Error: NullReferenceException for playerCamera?**
- Make sure you have a camera in the scene
- The camera should be tagged as "MainCamera"
- Or just add any Camera component to the scene

**Weapon shoots but hits nothing?**
- This is a different issue (not camera-related)
- Check that your bots have colliders
- Make sure weapon range is high enough (100+ recommended)

---

## Quick Reference

### For AK-47 Weapon Setup:
```
1. GameObject → 3D Object → Cube
2. Name: "Weapon_AK47"
3. Scale: (0.1, 0.1, 0.5)
4. Add Component → Weapon
5. Weapon Data: [Drag WeaponData_AK47 here]
6. Player Camera: [Leave EMPTY - auto-detects!]
7. Drag to Project folder to create prefab
```

### For ANY Weapon:
- ✅ Assign Weapon Data
- ✅ Leave Player Camera empty
- ✅ Save as prefab
- ✅ Add to WeaponManager or SimpleBuyMenu

---

## Why Type Mismatch Happens

Unity has different types:
- **GameObject**: The thing in your Hierarchy
- **Camera**: A component attached to a GameObject
- **Transform**: Another component
- **Renderer**: Another component

When a script asks for `Camera playerCamera`, it wants:
- The **Camera component**, not the GameObject

Think of it like this:
- GameObject = A box
- Camera = Something inside the box
- Script wants what's inside the box, not the box itself!

---

## Summary

**Just leave the Player Camera field empty!** 

The weapon will automatically find the camera and everything will work perfectly. No need to worry about type mismatches or manual assignments! 🎯

---

**Your weapon is now properly configured!** 🔫
