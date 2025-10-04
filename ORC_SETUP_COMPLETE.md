# Orc Race Setup - COMPLETE ✅

## Assets Created

### Race Data
- **Race_Orc.asset** - Main race configuration
  - Health: 115% (extra tankiness)
  - Speed: 95% (slight penalty)
  - Damage: 105% (damage boost)

### Abilities Created

#### 1. Orc_CriticalStrike.asset (Ultimate - Q Key)
**Type:** Passive
**Effect:** Chance for massive damage
- **Level 1:** 8% chance, 1.5x damage
- **Level 2:** 11% chance, 1.75x damage
- **Level 3:** 14% chance, 2.0x damage
- **Level 4:** 17% chance, 2.25x damage
- **Level 5:** 20% chance, 2.5x damage

#### 2. Orc_Bash.asset (Ability 2 - E Key)
**Type:** Passive Trigger
**Effect:** Chance to stun on hit
- **Level 1:** 5% chance, 0.3s stun
- **Level 2:** 7.5% chance, 0.45s stun
- **Level 3:** 10% chance, 0.6s stun
- **Level 4:** 12.5% chance, 0.75s stun
- **Level 5:** 15% chance, 0.9s stun
- **Per-target cooldown:** 3 seconds

#### 3. Orc_Reincarnation.asset (Ability 3 - F Key)
**Type:** Passive Trigger
**Effect:** Respawn on death (once per round)
- **Level 1:** 25% HP, 60s cooldown
- **Level 2:** 35% HP, 50s cooldown
- **Level 3:** 45% HP, 40s cooldown
- **Level 4:** 55% HP, 30s cooldown
- **Level 5:** 65% HP, 20s cooldown
- **Respawn delay:** 2 seconds (vulnerable)
- **Counters:** Headshots prevent reincarnation
- **Penalty:** Lose 50% ammo

---

## Next Steps: Configure Your Player

### Option 1: In Unity Editor
1. Open Unity
2. Find your Player prefab in Project window
3. Select it to open Inspector
4. Click "Add Component"
5. Search for "Ability System"
6. Add the component
7. In AbilitySystem component:
   - Drag **Race_Orc** to "Current Race" field
   - Set "Current Level" to 1
8. Save the prefab

### Option 2: Test in a Scene
1. Open your game scene
2. Find Player GameObject in Hierarchy
3. Add AbilitySystem component (if not present)
4. Assign Race_Orc to "Current Race"
5. Press Play to test!

---

## Testing Your Abilities

### In Play Mode:
- **Critical Strike:** Just shoot normally - you'll see yellow damage numbers when it procs
- **Bash:** Shoot enemies - they'll be stunned (can't move) when it procs
- **Reincarnation:** Die - you'll respawn at death location after 2 seconds

### Debug Commands (if enabled):
- **1** - Add XP
- **2** - Level Up (increases ability power!)
- **C** - Reset all cooldowns

### Console Messages to Watch:
```
[AbilitySystem] Initialized race: Orc at level 1
[CriticalStrike] Initialized. Chance: 8%, Multiplier: 1.5x
[Bash] Initialized. Chance: 5%, Duration: 0.3s
[Reincarnation] Initialized. Respawn HP: 25%
```

---

## Ability Interactions

### Critical Strike
- Works with **all weapons**
- Multiplies **final damage** (after armor)
- Visible yellow numbers on crit
- Stacks with race damage bonus (105%)

### Bash
- Only procs on **hit confirmation**
- 3-second cooldown **per target** (can stun multiple enemies)
- Stunned enemies can still shoot, just can't move
- Great for AWP follow-up shots

### Reincarnation
- **Countered by headshots** - no respawn if killed by headshot
- **Once per round** - hasUsedThisRound flag
- **2-second delay** - vulnerable during animation
- **50% ammo loss** - strategic penalty
- Respawns at **exact death position**

---

## Balance Philosophy

**Orc = Aggressive Powerhouse**
- High HP (115%) = Can take more damage in fights
- Damage boost (105%) = Rewards aggressive play
- Slight speed penalty (95%) = Can't run away easily
- Critical Strike = RNG burst damage for kills
- Bash = Utility for securing kills
- Reincarnation = Second chance, but heavily punished by headshots

**Counters:**
- Night Elf Evasion (dodge crits)
- Night Elf Thorns (reflect damage)
- Headshots (prevent Reincarnation)
- Kiting (exploit low speed)

---

## Ready to Create More Races?

You now have the template! To create the other 7 races:
- Undead (Vampiric, Levitation, Unholy)
- Human (Devotion, Invisibility, Teleport)
- Night Elf (Evasion, Blink, Thorns)
- Blood Elf (Mana Shield, Arcane Missiles, Mana Burn)
- Troll (Berserker, Regeneration, Axe Mastery)
- Dwarf (Stone Skin, Shield Bash, Heroic Strike)
- Celestial (Holy Light, Divine Shield, Resurrection)

Let me know if you want me to create all races at once! 🚀
