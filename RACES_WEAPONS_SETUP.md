# Complete Race & Weapon Setup Guide

This guide shows you how to create all the races, weapons, and hitboxes for your Warcraft mod.

## Part 1: Create Races (5 minutes)

### 1. Orc - The Tank
```
Right-click in Project → Create → Warcraft → Race Data
Name: "Race_Orc"
```
**Settings:**
- Race Type: Orc
- Race Name: "Orc"
- Race Description: "High health and damage, but slower movement"
- Health Multiplier: 1.3 (130 HP)
- Speed Multiplier: 0.85 (15% slower)
- Damage Multiplier: 1.2 (20% more damage)
- Max Level: 10

**Abilities:** (We'll create these in Part 3)
- Critical Strike (15% chance, 2x damage)
- Bash (20% chance to stun)
- Reincarnation (respawn with 50% HP)

---

### 2. Undead - The Life Stealer
```
Right-click in Project → Create → Warcraft → Race Data
Name: "Race_Undead"
```
**Settings:**
- Race Type: Undead
- Race Name: "Undead"
- Race Description: "Vampiric aura and unholy presence"
- Health Multiplier: 1.0 (100 HP)
- Speed Multiplier: 1.0 (normal)
- Damage Multiplier: 1.0 (normal)
- Max Level: 10

**Abilities:**
- Vampiric Aura (heal 20% of damage dealt)
- Levitation (reduced fall damage, high jumps)
- Unholy Aura (nearby enemies take damage over time)

---

### 3. Human - The Balanced
```
Right-click in Project → Create → Warcraft → Race Data
Name: "Race_Human"
```
**Settings:**
- Race Type: Human
- Race Name: "Human"
- Race Description: "Well-rounded with economic advantages"
- Health Multiplier: 1.0 (100 HP)
- Speed Multiplier: 1.0 (normal)
- Damage Multiplier: 1.0 (normal)
- Max Level: 10

**Abilities:**
- Devotion Aura (+10% armor)
- Invisibility (become invisible for 5 seconds)
- Bash (stun chance on hit)

---

### 4. Night Elf - The Assassin
```
Right-click in Project → Create → Warcraft → Race Data
Name: "Race_NightElf"
```
**Settings:**
- Race Type: NightElf
- Race Name: "Night Elf"
- Race Description: "High speed and evasion, lower health"
- Health Multiplier: 0.85 (85 HP)
- Speed Multiplier: 1.2 (20% faster)
- Damage Multiplier: 1.0 (normal)
- Max Level: 10

**Abilities:**
- Evasion (20% chance to dodge attacks)
- Blink (teleport forward 10 units)
- Thorns Aura (reflect damage back to attacker)

---

## Part 2: Create Weapons (10 minutes)

### 1. AK-47 - Assault Rifle
```
Right-click in Project → Create → Weapons → Weapon Data
Name: "Weapon_AK47"
```
**Settings:**
- Weapon Name: "AK-47"
- Weapon Type: Rifle
- Damage: 36
- Fire Rate: 0.1 (10 shots/sec)
- Range: 100
- Magazine Size: 30
- Max Ammo: 90
- Reload Time: 2.5
- Recoil Amount: 2.5
- Recoil Return Speed: 5
- Is Automatic: ✅ true
- Cost: 2500

---

### 2. M4A1 - Silenced Rifle
```
Right-click in Project → Create → Weapons → Weapon Data
Name: "Weapon_M4A1"
```
**Settings:**
- Weapon Name: "M4A1"
- Weapon Type: Rifle
- Damage: 33
- Fire Rate: 0.09 (slightly faster)
- Range: 100
- Magazine Size: 30
- Max Ammo: 90
- Reload Time: 3.1
- Recoil Amount: 2.0 (less recoil)
- Recoil Return Speed: 6
- Is Automatic: ✅ true
- Cost: 3100

---

### 3. AWP - Sniper Rifle
```
Right-click in Project → Create → Weapons → Weapon Data
Name: "Weapon_AWP"
```
**Settings:**
- Weapon Name: "AWP"
- Weapon Type: SniperRifle
- Damage: 115 (one-shot kill to body)
- Fire Rate: 1.5 (slow)
- Range: 300
- Magazine Size: 10
- Max Ammo: 30
- Reload Time: 3.7
- Recoil Amount: 10
- Recoil Return Speed: 3
- Is Automatic: ❌ false
- Cost: 4750

---

### 4. Desert Eagle - Pistol
```
Right-click in Project → Create → Weapons → Weapon Data
Name: "Weapon_Deagle"
```
**Settings:**
- Weapon Name: "Desert Eagle"
- Weapon Type: Pistol
- Damage: 54
- Fire Rate: 0.25
- Range: 80
- Magazine Size: 7
- Max Ammo: 35
- Reload Time: 2.2
- Recoil Amount: 3
- Recoil Return Speed: 5
- Is Automatic: ❌ false
- Cost: 650

---

### 5. Knife - Melee
```
Right-click in Project → Create → Weapons → Weapon Data
Name: "Weapon_Knife"
```
**Settings:**
- Weapon Name: "Knife"
- Weapon Type: Melee
- Damage: 50 (slash), 180 (backstab)
- Fire Rate: 0.4
- Range: 2 (melee range)
- Magazine Size: 999
- Max Ammo: 999
- Reload Time: 0
- Recoil Amount: 0
- Recoil Return Speed: 0
- Is Automatic: ❌ false
- Cost: 0 (starter weapon)

---

## Part 3: Create Bot with Hitboxes (IMPORTANT!)

### Step 1: Create Main Bot
```
GameObject → 3D Object → Capsule
Name: "Bot"
Scale: (1, 2, 1)
Position: (0, 0, 0)
Add Component → Bot
```
**Bot Settings:**
- Health: 100
- Max Health: 100
- ✅ Respawn On Death: true
- Respawn Delay: 3

### Step 2: Create Head Hitbox
```
Right-click Bot → 3D Object → Sphere
Name: "Head"
Scale: (0.4, 0.4, 0.4)
Position: (0, 0.7, 0)
Add Component → Hitbox
Add Component → Sphere Collider
```
**Head Hitbox Settings:**
- Hitbox Type: Head
- Head Multiplier: 4 (400% damage)

**Sphere Collider:**
- Is Trigger: ❌ false
- Radius: 0.5

### Step 3: Create Body Hitbox
```
Right-click Bot → 3D Object → Cube
Name: "Body"
Scale: (0.8, 1, 0.5)
Position: (0, 0, 0)
Add Component → Hitbox
Add Component → Box Collider
```
**Body Hitbox Settings:**
- Hitbox Type: Body
- Body Multiplier: 1 (100% damage)

**Box Collider:**
- Is Trigger: ❌ false
- Size: (1, 1, 1)

### Step 4: Create Arm Hitboxes (Left)
```
Right-click Bot → 3D Object → Capsule
Name: "LeftArm"
Scale: (0.2, 0.6, 0.2)
Position: (-0.5, 0, 0)
Rotation: (0, 0, 90)
Add Component → Hitbox
Add Component → Capsule Collider
```
**Left Arm Hitbox:**
- Hitbox Type: Arms
- Arm Multiplier: 0.75 (75% damage)

### Step 5: Create Arm Hitboxes (Right)
```
Right-click Bot → 3D Object → Capsule
Name: "RightArm"
Scale: (0.2, 0.6, 0.2)
Position: (0.5, 0, 0)
Rotation: (0, 0, 90)
Add Component → Hitbox
Add Component → Capsule Collider
```
**Right Arm Hitbox:**
- Hitbox Type: Arms
- Arm Multiplier: 0.75 (75% damage)

### Step 6: Create Leg Hitboxes (Left)
```
Right-click Bot → 3D Object → Capsule
Name: "LeftLeg"
Scale: (0.2, 0.8, 0.2)
Position: (-0.25, -0.8, 0)
Add Component → Hitbox
Add Component → Capsule Collider
```
**Left Leg Hitbox:**
- Hitbox Type: Legs
- Leg Multiplier: 0.75 (75% damage)

### Step 7: Create Leg Hitboxes (Right)
```
Right-click Bot → 3D Object → Capsule
Name: "RightLeg"
Scale: (0.2, 0.8, 0.2)
Position: (0.25, -0.8, 0)
Add Component → Hitbox
Add Component → Capsule Collider
```
**Right Leg Hitbox:**
- Hitbox Type: Legs
- Leg Multiplier: 0.75 (75% damage)

### Step 8: IMPORTANT - Disable Main Capsule Collider
```
Select the main "Bot" GameObject
In Inspector → Find "Capsule Collider" component
✅ UNCHECK the checkbox next to "Capsule Collider" to DISABLE it
```
**Why?** We want bullets to hit the child hitboxes (head, body, etc.) instead of the main collider.

### Step 9: Save as Prefab
```
Drag the "Bot" GameObject from Hierarchy to Project/Assets/Prefabs/
```

---

## Part 4: Assign Everything to Player

### 1. Assign Race to Player
```
Select Player GameObject in Hierarchy
Find "Player Race" component
Click the circle next to "Current Race"
Select "Race_Orc" (or any race you want to start with)
```

### 2. Assign Weapons to WeaponManager
```
Select Player GameObject
Find "Weapon Manager" component
Click the + button under "Weapon Prefabs" list
```
For each weapon prefab you created:
1. Create a simple GameObject with your weapon model
2. Add Weapon component
3. Assign the WeaponData ScriptableObject
4. Assign Player Camera reference
5. Save as prefab
6. Add to WeaponManager's Weapon Prefabs list

---

## Part 5: Test Everything!

### Damage Test:
1. Press Play
2. Press F5 to spawn bots
3. Get a weapon (B key → buy AK-47)
4. Shoot bot in different places:
   - **Head**: ~144 damage (36 × 4)
   - **Body**: ~36 damage (36 × 1)
   - **Arms/Legs**: ~27 damage (36 × 0.75)

### Race Test:
1. Press Tab to open race selection
2. Choose Orc
3. Notice increased health (130 HP)
4. Notice slower movement
5. Damage output is 20% higher

### Weapon Test:
1. Open buy menu (B key)
2. Try different weapons:
   - AK-47: Full auto, good all-around
   - M4A1: Less recoil, accurate
   - AWP: One-shot kills to body
   - Deagle: High pistol damage

---

## Damage Multiplier Reference

| Body Part | Multiplier | Example (AK-47 @ 36 dmg) |
|-----------|------------|--------------------------|
| Head      | 4.0x       | 144 damage               |
| Body      | 1.0x       | 36 damage                |
| Arms      | 0.75x      | 27 damage                |
| Legs      | 0.75x      | 27 damage                |

---

## Race Stats Summary

| Race       | HP   | Speed | Damage | Special                |
|------------|------|-------|--------|------------------------|
| Orc        | 130  | 85%   | 120%   | High damage tank       |
| Undead     | 100  | 100%  | 100%   | Life steal vampire     |
| Human      | 100  | 100%  | 100%   | Balanced all-rounder   |
| Night Elf  | 85   | 120%  | 100%   | Fast evasive assassin  |

---

## Weapon Stats Summary

| Weapon        | Damage | Fire Rate | Mag | Type      | Cost |
|---------------|--------|-----------|-----|-----------|------|
| Knife         | 50     | 0.4s      | ∞   | Melee     | Free |
| Desert Eagle  | 54     | 0.25s     | 7   | Pistol    | 650  |
| AK-47         | 36     | 0.1s      | 30  | Rifle     | 2500 |
| M4A1          | 33     | 0.09s     | 30  | Rifle     | 3100 |
| AWP           | 115    | 1.5s      | 10  | Sniper    | 4750 |

---

**You're all set! Enjoy the full Warcraft mod experience!** 🎮⚔️
