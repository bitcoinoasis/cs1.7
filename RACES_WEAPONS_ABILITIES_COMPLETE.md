# Complete Races, Weapons & Abilities Guide

**CS 1.6 Warcraft Mod - Perfectly Balanced**

Inspired by the original Warcraft 3 mod for Counter-Strike 1.6, this guide provides balanced races, weapons, and abilities for competitive gameplay.

---

## 📊 Balance Philosophy

### Core Principles:
1. **No Overpowered Race** - Each race has strengths and weaknesses
2. **Skill-Based** - Abilities enhance skill, don't replace it
3. **Counter-Play** - Every strength has a counter
4. **Progressive Power** - Higher levels feel rewarding but not broken
5. **CS 1.6 Authenticity** - Weapons match original stats

---

## 🏰 Complete Race System (8 Races)

### Tier System:
- **Starter Races** (Levels 1-5): Easy to learn, forgiving
- **Intermediate Races** (Levels 6-10): Require strategy
- **Advanced Races** (All levels): High skill ceiling
- **Expert Races** (Levels 8-10): Complex mechanics

---

## 🔥 RACE 1: ORC (Starter - Tank)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 1.30 (130 HP)
  Speed Multiplier: 0.85 (85% speed)
  Damage Multiplier: 1.15 (115% damage)
  Max Level: 10
  
Playstyle: Aggressive tank, trades speed for durability
Best For: New players, aggressive playstyle, holding positions
Counters: Fast races, snipers, long-range combat
```

### Abilities:

#### **Ultimate: Critical Strike (Passive)**
```yaml
Type: Passive (chance-based)
Description: "Your attacks have a chance to deal massive bonus damage"

Level 1: 8% chance, 1.5x damage
Level 2: 12% chance, 1.75x damage
Level 3: 15% chance, 2.0x damage
Level 4: 18% chance, 2.25x damage
Level 5: 20% chance, 2.5x damage

Balance Notes:
- Unreliable but rewarding
- Works on headshots (stacks multiplicatively)
- Visual effect: Orange damage numbers
- Sound cue for enemy awareness
```

#### **Ability 2: Bash (Passive)**
```yaml
Type: Passive (chance on hit)
Description: "Chance to stun enemies for a brief moment"

Level 1: 5% chance, 0.3s stun
Level 2: 8% chance, 0.4s stun
Level 3: 10% chance, 0.5s stun
Level 4: 12% chance, 0.6s stun
Level 5: 15% chance, 0.75s stun

Balance Notes:
- Stun freezes enemy movement but not shooting
- Visual: Screen shake for stunned player
- Cannot stun same target twice in 3 seconds
- Useful for rushing or escaping
```

#### **Ability 3: Reincarnation (Active/Passive)**
```yaml
Type: Active - Triggers on death
Description: "Respawn at death location with partial HP (once per round)"

Level 1: 25% HP respawn, 60s cooldown
Level 2: 35% HP respawn, 50s cooldown
Level 3: 45% HP respawn, 40s cooldown
Level 4: 55% HP respawn, 30s cooldown
Level 5: 65% HP respawn, 20s cooldown

Balance Notes:
- Only works once per round
- 2-second revival delay (vulnerable)
- Cannot be used if killed by headshot
- Weapons kept but ammo at 50%
- Visual: Green glow during revival
```

---

## 💀 RACE 2: UNDEAD (Intermediate - Vampire)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 1.00 (100 HP)
  Speed Multiplier: 1.00 (100% speed)
  Damage Multiplier: 1.00 (100% damage)
  Max Level: 10
  
Playstyle: Sustain fighter, life steal rewards aggression
Best For: Players who can land shots consistently
Counters: Burst damage, snipers, long-range poke
```

### Abilities:

#### **Ultimate: Vampiric Aura (Passive)**
```yaml
Type: Passive (life steal)
Description: "Heal for a percentage of damage dealt"

Level 1: 10% life steal
Level 2: 15% life steal
Level 3: 20% life steal
Level 4: 25% life steal
Level 5: 30% life steal

Balance Notes:
- Only works on player damage (not bots in demo)
- Healing capped at 5 HP per shot
- Visual: Red particles when healing
- Encourages close combat
- Countered by burst damage
```

#### **Ability 2: Levitation (Passive)**
```yaml
Type: Passive (movement)
Description: "Reduced gravity, higher jumps, no fall damage"

Level 1: 0.9x gravity, 1.2x jump height
Level 2: 0.85x gravity, 1.3x jump height
Level 3: 0.8x gravity, 1.4x jump height
Level 4: 0.75x gravity, 1.5x jump height
Level 5: 0.7x gravity, 1.6x jump height

Balance Notes:
- Easier to hit while in air (trade-off)
- Useful for unexpected angles
- Great for map navigation
- Can be countered by tracking aim
```

#### **Ability 3: Unholy Aura (Active/Passive)**
```yaml
Type: Passive (proximity damage)
Description: "Enemies near you take damage over time"

Level 1: 2 dmg/sec, 3m radius
Level 2: 3 dmg/sec, 3.5m radius
Level 3: 4 dmg/sec, 4m radius
Level 4: 5 dmg/sec, 4.5m radius
Level 5: 6 dmg/sec, 5m radius

Balance Notes:
- Damage ticks every 0.5 seconds
- Visible purple aura (enemy can see)
- Doesn't go through walls
- Stacks with weapon damage
- Forces enemies to keep distance
```

---

## 🛡️ RACE 3: HUMAN (Starter - Balanced)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 1.00 (100 HP)
  Speed Multiplier: 1.00 (100% speed)
  Damage Multiplier: 1.00 (100% damage)
  Max Level: 10
  
Playstyle: Versatile, adaptable to any situation
Best For: Beginners, all-around gameplay, learning game
Counters: Specialized strategies can overpower
```

### Abilities:

#### **Ultimate: Devotion Aura (Passive)**
```yaml
Type: Passive (damage reduction)
Description: "Reduce all incoming damage"

Level 1: 5% damage reduction
Level 2: 8% damage reduction
Level 3: 10% damage reduction
Level 4: 12% damage reduction
Level 5: 15% damage reduction

Balance Notes:
- Effectively increases HP by reduction %
- Stacks with armor (multiplicative)
- Best race for defensive play
- Visual: Blue shield shimmer when hit
```

#### **Ability 2: Invisibility (Active)**
```yaml
Type: Active (stealth)
Description: "Become invisible for a short duration"

Level 1: 2s duration, 45s cooldown, 40% visibility
Level 2: 3s duration, 40s cooldown, 30% visibility
Level 3: 4s duration, 35s cooldown, 20% visibility
Level 4: 5s duration, 30s cooldown, 15% visibility
Level 5: 6s duration, 25s cooldown, 10% visibility

Balance Notes:
- Shooting/taking damage breaks invisibility
- Footsteps still audible
- Slight shimmer visible to attentive players
- Great for flanking or escaping
- Can be spotted with careful observation
```

#### **Ability 3: Teleport (Active)**
```yaml
Type: Active (mobility)
Description: "Teleport forward in the direction you're facing"

Level 1: 5m distance, 30s cooldown
Level 2: 7m distance, 27s cooldown
Level 3: 9m distance, 24s cooldown
Level 4: 11m distance, 21s cooldown
Level 5: 13m distance, 18s cooldown

Balance Notes:
- Cannot teleport through walls
- 0.3s vulnerability after teleport
- Visual/audio cue for enemies
- Useful for dodging or closing gaps
- Can teleport vertically if looking up/down
```

---

## 🌙 RACE 4: NIGHT ELF (Advanced - Assassin)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 0.85 (85 HP)
  Speed Multiplier: 1.20 (120% speed)
  Damage Multiplier: 1.00 (100% damage)
  Max Level: 10
  
Playstyle: Hit-and-run, high mobility glass cannon
Best For: Skilled players with good aim and positioning
Counters: AoE, traps, prediction shots, spray weapons
```

### Abilities:

#### **Ultimate: Evasion (Passive)**
```yaml
Type: Passive (dodge chance)
Description: "Chance to completely avoid damage"

Level 1: 10% dodge chance
Level 2: 13% dodge chance
Level 3: 16% dodge chance
Level 4: 19% dodge chance
Level 5: 22% dodge chance

Balance Notes:
- Only works on bullet damage
- Visual: Blue afterimage when dodging
- Audio cue: "Whoosh" sound
- Doesn't work on explosive damage
- Can dodge multiple shots in succession (lucky streaks)
```

#### **Ability 2: Blink (Active)**
```yaml
Type: Active (dash)
Description: "Instantly dash forward, avoiding all damage"

Level 1: 8m dash, 25s cooldown
Level 2: 9m dash, 23s cooldown
Level 3: 10m dash, 21s cooldown
Level 4: 11m dash, 19s cooldown
Level 5: 12m dash, 17s cooldown

Balance Notes:
- Invulnerable during dash (0.2s)
- Can dash through enemies
- Cannot dash through walls
- Leave trail of after-images
- Useful for dodging AWP shots
```

#### **Ability 3: Thorns Aura (Passive)**
```yaml
Type: Passive (reflect damage)
Description: "Reflect a portion of damage taken back to attacker"

Level 1: 10% damage reflected
Level 2: 15% damage reflected
Level 3: 20% damage reflected
Level 4: 25% damage reflected
Level 5: 30% damage reflected

Balance Notes:
- Works on all damage types
- Reflected damage ignores armor
- Visual: Spikes appear when hit
- Encourages enemies to use burst damage
- Synergizes with Evasion
```

---

## ⚡ RACE 5: BLOOD ELF (Intermediate - Mage)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 0.90 (90 HP)
  Speed Multiplier: 1.05 (105% speed)
  Damage Multiplier: 1.10 (110% damage)
  Max Level: 10
  
Playstyle: Magical damage, ability-focused combat
Best For: Players who rely on abilities over raw aim
Counters: Rush strategies, silence effects (future)
```

### Abilities:

#### **Ultimate: Mana Shield (Active/Passive)**
```yaml
Type: Passive + Active
Description: "Damage is absorbed by mana before health. Activate to restore mana."

Passive:
  Level 1: 20 mana pool, blocks 1 dmg per 1 mana
  Level 2: 30 mana pool, blocks 1 dmg per 1 mana
  Level 3: 40 mana pool, blocks 1 dmg per 1 mana
  Level 4: 50 mana pool, blocks 1 dmg per 1 mana
  Level 5: 60 mana pool, blocks 1 dmg per 1 mana

Active (Press Ultimate Key):
  Restore 50% mana instantly, 35s cooldown

Balance Notes:
- Mana regenerates 2 per second naturally
- Taking damage pauses regen for 3s
- Visual: Blue shimmer when shield active
- Skill-based resource management
```

#### **Ability 2: Arcane Blast (Active)**
```yaml
Type: Active (damage)
Description: "Fire a magical projectile that deals damage"

Level 1: 40 damage, 12s cooldown, 15 mana cost
Level 2: 50 damage, 11s cooldown, 15 mana cost
Level 3: 60 damage, 10s cooldown, 15 mana cost
Level 4: 70 damage, 9s cooldown, 15 mana cost
Level 5: 80 damage, 8s cooldown, 15 mana cost

Balance Notes:
- Projectile travels at 50 m/s
- Can be dodged by strafing
- Passes through multiple enemies
- Costs mana (from Mana Shield)
- Visual: Purple projectile
```

#### **Ability 3: Mana Burn (Active)**
```yaml
Type: Active (utility)
Description: "Drain enemy mana/stamina, dealing damage equal to amount drained"

Level 1: Drain 20, 20s cooldown, 10 mana cost
Level 2: Drain 30, 19s cooldown, 10 mana cost
Level 3: Drain 40, 18s cooldown, 10 mana cost
Level 4: Drain 50, 17s cooldown, 10 mana cost
Level 5: Drain 60, 16s cooldown, 10 mana cost

Balance Notes:
- Targeted ability (crosshair on enemy)
- 15m range
- If enemy has no mana, deals half damage
- Useful against other Blood Elves
- Visual: Red beam effect
```

---

## 🐺 RACE 6: TROLL (Advanced - Berserker)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 1.10 (110 HP)
  Speed Multiplier: 0.95 (95% speed)
  Damage Multiplier: 0.90 (90% damage)
  Max Level: 10
  
Playstyle: Berserker - stronger when wounded
Best For: Aggressive players who thrive under pressure
Counters: One-shot weapons (AWP), burst damage
```

### Abilities:

#### **Ultimate: Berserker Rage (Passive/Active)**
```yaml
Type: Passive with Active trigger
Description: "Gain attack speed and damage as HP decreases"

Passive Bonuses (based on missing HP):
  0-25% HP missing: No bonus
  26-50% HP missing: +10% attack speed, +5% damage
  51-75% HP missing: +20% attack speed, +10% damage
  76-99% HP missing: +40% attack speed, +20% damage
  
Active (Press Ultimate Key):
  Level 1: Instant 30 HP heal, 45s cooldown
  Level 2: Instant 40 HP heal, 40s cooldown
  Level 3: Instant 50 HP heal, 35s cooldown
  Level 4: Instant 60 HP heal, 30s cooldown
  Level 5: Instant 70 HP heal, 25s cooldown

Balance Notes:
- High risk, high reward playstyle
- Visual: Red aura intensity increases with rage
- Healing removes rage bonus
- Must manage HP carefully
```

#### **Ability 2: Throwing Axes (Active)**
```yaml
Type: Active (ranged attack)
Description: "Throw axes that deal damage and slow enemies"

Level 1: 35 dmg, 2 axes, 15s cooldown, 30% slow for 2s
Level 2: 40 dmg, 2 axes, 14s cooldown, 35% slow for 2.5s
Level 3: 45 dmg, 3 axes, 13s cooldown, 40% slow for 3s
Level 4: 50 dmg, 3 axes, 12s cooldown, 45% slow for 3.5s
Level 5: 55 dmg, 4 axes, 11s cooldown, 50% slow for 4s

Balance Notes:
- Axes travel in spread pattern
- Can hit multiple enemies
- Useful for finishing low HP targets
- Synergizes with Berserker Rage
```

#### **Ability 3: Regeneration (Passive)**
```yaml
Type: Passive (healing)
Description: "Constantly regenerate health over time"

Level 1: 1 HP per 3 seconds
Level 2: 1 HP per 2.5 seconds
Level 3: 1 HP per 2 seconds
Level 4: 2 HP per 2 seconds
Level 5: 2 HP per 1.5 seconds

Balance Notes:
- Works even during combat
- Pauses for 5s if hit by headshot
- Stacks with life steal items
- Keeps you in Berserker Rage zone
- Visual: Green pulse when healing
```

---

## 🗿 RACE 7: DWARF (Starter - Defender)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 1.25 (125 HP)
  Speed Multiplier: 0.80 (80% speed)
  Damage Multiplier: 1.05 (105% damage)
  Max Level: 10
  
Playstyle: Defensive anchor, holds positions
Best For: Players who like defensive play, sniping
Counters: Flankers, fast rushes, mobility
```

### Abilities:

#### **Ultimate: Iron Skin (Passive)**
```yaml
Type: Passive (armor)
Description: "Gain natural armor that reduces damage"

Level 1: 10 armor (blocks 10 damage before HP)
Level 2: 15 armor (blocks 15 damage before HP)
Level 3: 20 armor (blocks 20 damage before HP)
Level 4: 25 armor (blocks 25 damage before HP)
Level 5: 30 armor (blocks 30 damage before HP)

Balance Notes:
- Armor regenerates 1 per second (out of combat)
- Headshots bypass 50% of armor
- Visual: Gray metallic skin
- Stacks with bought armor (additive)
- Makes Dwarf extremely tanky
```

#### **Ability 2: Entrenchment (Active)**
```yaml
Type: Active (stance)
Description: "Root yourself for massive damage reduction"

Level 1: 30% damage reduction, can't move, 20s duration, 40s CD
Level 2: 35% damage reduction, can't move, 22s duration, 38s CD
Level 3: 40% damage reduction, can't move, 24s duration, 36s CD
Level 4: 45% damage reduction, can't move, 26s duration, 34s CD
Level 5: 50% damage reduction, can't move, 28s duration, 32s CD

Balance Notes:
- Can still shoot and look around
- Press ability again to cancel early
- Visual: Stone texture on body
- Great for holding choke points
- Vulnerable to explosives and flanks
```

#### **Ability 3: Ground Slam (Active)**
```yaml
Type: Active (AoE stun)
Description: "Slam the ground, stunning nearby enemies"

Level 1: 5m radius, 0.5s stun, 30s cooldown
Level 2: 6m radius, 0.75s stun, 28s cooldown
Level 3: 7m radius, 1.0s stun, 26s cooldown
Level 4: 8m radius, 1.25s stun, 24s cooldown
Level 5: 9m radius, 1.5s stun, 22s cooldown

Balance Notes:
- Visible wind-up animation (0.5s)
- Can be dodged by jumping
- Works through walls (vibrations)
- Visual: Brown shockwave
- Audio: Loud slam sound
```

---

## 🌟 RACE 8: CELESTIAL (Expert - Support)

### Race Stats:
```yaml
Base Stats:
  Health Multiplier: 0.95 (95 HP)
  Speed Multiplier: 1.08 (108% speed)
  Damage Multiplier: 0.95 (95% damage)
  Max Level: 10
  
Playstyle: Support/team-based, buffs and healing
Best For: Team players, support role, coordinated play
Counters: Solo play, uncoordinated teams
```

### Abilities:

#### **Ultimate: Divine Shield (Active)**
```yaml
Type: Active (shield)
Description: "Grant self or ally temporary invulnerability"

Level 1: 1.5s immunity, 60s cooldown, 10m cast range
Level 2: 2.0s immunity, 55s cooldown, 12m cast range
Level 3: 2.5s immunity, 50s cooldown, 14m cast range
Level 4: 3.0s immunity, 45s cooldown, 16m cast range
Level 5: 3.5s immunity, 40s cooldown, 18m cast range

Balance Notes:
- Can target self or aim at ally
- Shielded player glows golden
- Cannot attack while shielded
- Can move normally
- Can be dispelled (future mechanic)
```

#### **Ability 2: Healing Wave (Active)**
```yaml
Type: Active (heal)
Description: "Heal yourself or an ally"

Level 1: 30 HP heal, 15s cooldown, 8m range
Level 2: 40 HP heal, 14s cooldown, 10m range
Level 3: 50 HP heal, 13s cooldown, 12m range
Level 4: 60 HP heal, 12s cooldown, 14m range
Level 5: 70 HP heal, 11s cooldown, 16m range

Balance Notes:
- Aim at ally to heal them
- Heal travels as projectile (can miss)
- Cannot heal through walls
- Visual: Green wave
- Overheal grants 20% temp HP (decays over time)
```

#### **Ability 3: Aura of Light (Passive)**
```yaml
Type: Passive (team buff)
Description: "Nearby allies gain bonus stats"

Level 1: 8m radius, +3% damage, +5% speed
Level 2: 9m radius, +5% damage, +7% speed
Level 3: 10m radius, +7% damage, +9% speed
Level 4: 11m radius, +9% damage, +11% speed
Level 5: 12m radius, +12% damage, +15% speed

Balance Notes:
- Always active
- Visible golden glow (enemies can see)
- Affects all allies in range
- Stacks with race bonuses
- Encourages grouping
```

---

## 🔫 Complete Weapon System

### Weapon Tiers:
- **Pistols** - Starting weapons, eco round options
- **SMGs** - Mobile, anti-eco weapons
- **Rifles** - Standard combat, all-purpose
- **Snipers** - Long-range, one-shot potential
- **Shotguns** - Close-range devastation

---

## 💰 PISTOLS

### 1. **USP-S** (Starting CT Pistol)
```yaml
Stats:
  Damage: 35
  Fire Rate: 0.15s (6.67 shots/sec)
  Magazine: 12
  Reserve Ammo: 100
  Reload Time: 2.2s
  Accuracy: Very High
  Armor Penetration: 50%
  Movement Speed: 250 (100%)
  Range: 1000 units
  Price: $0 (starting weapon)
  
Headshot Multiplier: 4x (140 damage)
Body Multiplier: 1x (35 damage)
Leg Multiplier: 0.75x (26 damage)

Notes:
- Silenced (doesn't appear on radar)
- Best pistol accuracy
- Rewards precise aim
- Can remove silencer for +5 damage
```

### 2. **Glock-18** (Starting T Pistol)
```yaml
Stats:
  Damage: 28
  Fire Rate: 0.12s (8.33 shots/sec)
  Magazine: 20
  Reserve Ammo: 120
  Reload Time: 2.2s
  Accuracy: Medium
  Armor Penetration: 47%
  Movement Speed: 250 (100%)
  Range: 1000 units
  Price: $0 (starting weapon)
  
Special:
  Burst Mode: 3-round burst, 0.05s between shots
  
Notes:
- High capacity
- Fast fire rate
- Burst mode for close range
- Lower damage than USP
```

### 3. **Desert Eagle**
```yaml
Stats:
  Damage: 54
  Fire Rate: 0.25s (4 shots/sec)
  Magazine: 7
  Reserve Ammo: 35
  Reload Time: 2.2s
  Accuracy: High
  Armor Penetration: 93%
  Movement Speed: 250 (100%)
  Range: 4000 units
  Price: $650
  
Headshot Multiplier: 4x (216 damage - instakill)
Body Multiplier: 1x (54 damage)

Notes:
- Most powerful pistol
- One-shot headshot (no helmet)
- High recoil
- Slow fire rate
- Eco round game-changer
```

### 4. **P250**
```yaml
Stats:
  Damage: 38
  Fire Rate: 0.15s (6.67 shots/sec)
  Magazine: 13
  Reserve Ammo: 52
  Reload Time: 2.2s
  Accuracy: High
  Armor Penetration: 64%
  Movement Speed: 250 (100%)
  Range: 2000 units
  Price: $300
  
Notes:
- Good armor penetration
- Cheap eco option
- Balanced stats
- Reliable backup weapon
```

---

## 🔫 SMGs (Sub-Machine Guns)

### 1. **MP5-SD**
```yaml
Stats:
  Damage: 27
  Fire Rate: 0.08s (12.5 shots/sec)
  Magazine: 30
  Reserve Ammo: 120
  Reload Time: 2.4s
  Accuracy: Medium
  Armor Penetration: 57%
  Movement Speed: 250 (100%)
  Range: 2000 units
  Price: $1500
  Kill Reward: $600 (+$300 bonus)
  
Notes:
- Integrated silencer
- Fast movement speed
- Anti-eco weapon
- High kill reward
- Low armor penetration
```

### 2. **P90**
```yaml
Stats:
  Damage: 26
  Fire Rate: 0.07s (14.3 shots/sec)
  Magazine: 50
  Reserve Ammo: 100
  Reload Time: 3.3s
  Accuracy: Medium-Low
  Armor Penetration: 65%
  Movement Speed: 245 (98%)
  Range: 2000 units
  Price: $2350
  Kill Reward: $300
  
Notes:
- Highest capacity SMG
- "Run and gun" weapon
- Spray-friendly
- Expensive for SMG
- Good against unarmored
```

---

## 🎯 RIFLES

### 1. **AK-47** (Terrorist Rifle)
```yaml
Stats:
  Damage: 36
  Fire Rate: 0.1s (10 shots/sec)
  Magazine: 30
  Reserve Ammo: 90
  Reload Time: 2.5s
  Accuracy: Medium
  Armor Penetration: 77.5%
  Movement Speed: 215 (86%)
  Range: 8192 units
  Price: $2500
  Kill Reward: $300
  
Headshot Multiplier: 4x (144 damage - instakill)
Body Multiplier: 1x (36 damage)

Spray Pattern:
  First 3 shots: Nearly straight
  Shots 4-10: Up and slightly left
  Shots 11-30: Spread increases dramatically
  
Notes:
- One-tap headshot potential
- High damage
- High first-shot accuracy
- Difficult spray pattern
- Iconic T weapon
```

### 2. **M4A4** (CT Rifle)
```yaml
Stats:
  Damage: 33
  Fire Rate: 0.09s (11.1 shots/sec)
  Magazine: 30
  Reserve Ammo: 90
  Reload Time: 3.1s
  Accuracy: High
  Armor Penetration: 70%
  Movement Speed: 225 (90%)
  Range: 8192 units
  Price: $3100
  Kill Reward: $300
  
Headshot Multiplier: 4x (132 damage - NOT instakill)
Body Multiplier: 1x (33 damage)

Spray Pattern:
  First 5 shots: Very straight
  Shots 6-15: Up and left
  Shots 16-30: Tighter than AK
  
Notes:
- Easier to control than AK
- Requires 2 headshots for kill
- Faster fire rate
- Better spray accuracy
- Higher price
```

### 3. **M4A1-S** (CT Rifle - Silenced)
```yaml
Stats:
  Damage: 33
  Fire Rate: 0.09s (11.1 shots/sec)
  Magazine: 20
  Reserve Ammo: 40
  Reload Time: 3.0s
  Accuracy: Very High
  Armor Penetration: 70%
  Movement Speed: 225 (90%)
  Range: 8192 units
  Price: $2900
  Kill Reward: $300
  
Notes:
- Silenced (no tracers, quiet)
- Better accuracy than M4A4
- Lower ammo capacity
- Stealth advantage
- Popular for precise players
```

### 4. **AUG** (CT Assault Rifle)
```yaml
Stats:
  Damage: 28
  Fire Rate: 0.09s (11.1 shots/sec)
  Magazine: 30
  Reserve Ammo: 90
  Reload Time: 3.8s
  Accuracy: High
  Armor Penetration: 90%
  Movement Speed: 220 (88%)
  Range: 8192 units
  Price: $3300
  Kill Reward: $300
  
Special: Scope (Right-click for 1.5x zoom)
  
Notes:
- Scoped rifle
- One-shot headshot at close range
- High armor penetration
- Expensive
- Versatile
```

### 5. **SG 553** (T Assault Rifle)
```yaml
Stats:
  Damage: 30
  Fire Rate: 0.09s (11.1 shots/sec)
  Magazine: 30
  Reserve Ammo: 90
  Reload Time: 3.0s
  Accuracy: High
  Armor Penetration: 100%
  Movement Speed: 210 (84%)
  Range: 8192 units
  Price: $3000
  Kill Reward: $300
  
Special: Scope (Right-click for 2x zoom)
  
Notes:
- Scoped rifle
- Perfect armor penetration
- One-shot headshot
- Slow movement
- High skill ceiling
```

### 6. **Galil AR** (T Budget Rifle)
```yaml
Stats:
  Damage: 30
  Fire Rate: 0.09s (11.1 shots/sec)
  Magazine: 35
  Reserve Ammo: 90
  Reload Time: 2.5s
  Accuracy: Medium
  Armor Penetration: 77%
  Movement Speed: 215 (86%)
  Range: 8192 units
  Price: $1800
  Kill Reward: $300
  
Notes:
- Budget rifle option
- High capacity
- Lower accuracy than AK
- Good force-buy weapon
- Cheap but effective
```

### 7. **FAMAS** (CT Budget Rifle)
```yaml
Stats:
  Damage: 30
  Fire Rate: 0.09s (11.1 shots/sec)
  Magazine: 25
  Reserve Ammo: 90
  Reload Time: 3.3s
  Accuracy: Medium
  Armor Penetration: 70%
  Movement Speed: 220 (88%)
  Range: 8192 units
  Price: $2050
  Kill Reward: $300
  
Special: Burst Mode (3-round burst, 0.05s between shots)
  
Notes:
- Budget CT rifle
- Burst mode for accuracy
- Lower capacity
- Good eco/force weapon
- Fast movement
```

---

## 🎯 SNIPER RIFLES

### 1. **AWP** (Arctic Warfare Police)
```yaml
Stats:
  Damage: 115
  Fire Rate: 1.5s bolt-action
  Magazine: 10
  Reserve Ammo: 30
  Reload Time: 3.7s
  Accuracy: Perfect (scoped)
  Armor Penetration: 97.5%
  Movement Speed: 200 (80%)
  Scope: 2x and 4x zoom
  Range: 16384 units
  Price: $4750
  Kill Reward: $100
  
Damage:
  Chest/Head: 115+ (instakill)
  Stomach: 115 (instakill)
  Legs: 86 (NOT instakill)
  Arms: 86 (NOT instakill)
  
Notes:
- One-shot kill (body or head)
- Extremely slow
- High price
- Low kill reward
- Defines high-level play
- Can wallbang through surfaces
```

### 2. **Scout (SSG 08)**
```yaml
Stats:
  Damage: 88
  Fire Rate: 1.25s bolt-action
  Magazine: 10
  Reserve Ammo: 90
  Reload Time: 2.0s
  Accuracy: Very High (scoped)
  Armor Penetration: 85%
  Movement Speed: 230 (92%)
  Scope: 2x zoom only
  Range: 8192 units
  Price: $1700
  Kill Reward: $300
  
Damage:
  Head: 352 (instakill with headshot)
  Chest: 88
  Stomach: 110
  Legs: 66
  
Notes:
- Fast movement for sniper
- One-shot headshot
- Two-shot body kill
- Eco sniper option
- Jump-shot accurate
- Popular for aggressive sniping
```

---

## 💥 SHOTGUNS

### 1. **Nova**
```yaml
Stats:
  Damage: 26 per pellet × 9 pellets = 234 max
  Fire Rate: 0.9s pump-action
  Magazine: 8
  Reserve Ammo: 32
  Reload Time: 0.6s per shell
  Accuracy: Wide spread
  Armor Penetration: 50%
  Movement Speed: 220 (88%)
  Range: 2000 units (effective)
  Price: $1050
  Kill Reward: $900 (+$600 bonus)
  
Notes:
- High kill reward
- Close-range only
- Inconsistent damage
- Good for holding corners
- Armor greatly reduces damage
```

### 2. **XM1014** (Auto-Shotgun)
```yaml
Stats:
  Damage: 20 per pellet × 6 pellets = 120 max
  Fire Rate: 0.35s semi-auto
  Magazine: 7
  Reserve Ammo: 32
  Reload Time: 0.4s per shell
  Accuracy: Medium spread
  Armor Penetration: 80%
  Movement Speed: 215 (86%)
  Range: 2000 units (effective)
  Price: $2000
  Kill Reward: $900 (+$600 bonus)
  
Notes:
- Fast follow-up shots
- Better armor penetration
- More expensive
- Reliable close-range
- Can spray multiple enemies
```

---

## 🔪 MELEE

### **Knife**
```yaml
Stats:
  Primary Attack (Slash):
    Damage: 34 (front), 65 (back)
    Range: 1.5m
    Fire Rate: 0.4s
    
  Secondary Attack (Stab):
    Damage: 65 (front), 180 (back)
    Range: 1.2m
    Fire Rate: 1.1s
    
Movement Speed: 250 (100%)
Price: $0 (always equipped)

Notes:
- Backstab = instant kill
- Always available
- Fastest movement
- Last resort weapon
- Used for running
```

---

## ⚖️ Race Balance Matrix

### Win Rate Targets (Ideal: 50%)
```
Starter Races: 48-52% (forgiving for new players)
Intermediate: 49-51% (competitive)
Advanced: 47-53% (skill-dependent)
Expert: 45-55% (team-dependent)
```

### Counter Matrix
```
         Orc  Undead  Human  NElf  BElf  Troll  Dwarf  Celest
Orc      -    Weak    Even   Weak  Even  Strong Even   Weak
Undead   Strong -     Even   Even  Weak  Even   Strong Weak
Human    Even  Even    -     Even  Even  Even   Even   Even
NElf     Strong Even   Even   -    Strong Weak  Strong Even
BElf     Even  Strong  Even   Weak  -    Even   Even   Strong
Troll    Weak  Even    Even   Strong Even  -    Weak   Even
Dwarf    Even  Weak    Even   Weak  Even  Strong -     Even
Celest   Strong Strong Even   Even  Weak  Even   Even   -

Strong = 60/40 matchup
Even = 50/50 matchup
Weak = 40/60 matchup
```

### Weapon Synergies

#### **Best Weapons by Race:**

**Orc:**
- Primary: AK-47, Nova (close-range tank)
- Secondary: Desert Eagle
- Avoid: Scout (doesn't need mobility)

**Undead:**
- Primary: P90, MP5 (sustain through volume)
- Secondary: M4A1-S
- Avoid: AWP (life steal doesn't help)

**Human:**
- Primary: M4A4, AK-47 (versatile)
- Secondary: Desert Eagle
- Best All-Arounder

**Night Elf:**
- Primary: Scout, P90 (hit-and-run)
- Secondary: Desert Eagle
- Avoid: AWP (too slow)

**Blood Elf:**
- Primary: M4A1-S, AUG (ability-focused)
- Secondary: USP-S
- Avoid: P90 (relies on raw damage)

**Troll:**
- Primary: AK-47, P90 (berserker aggression)
- Secondary: Glock burst
- Excellent with low HP

**Dwarf:**
- Primary: AWP, AUG (defensive positions)
- Secondary: M4A4
- Avoid: SMGs (wasted mobility)

**Celestial:**
- Primary: M4A4 (team support positioning)
- Secondary: P250
- Flexible for team needs

---

## 📈 Progression & XP System

### XP Formula:
```python
XP_per_kill = 100
XP_per_assist = 25
XP_per_objective = 50 (plant/defuse)
XP_per_round_win = 200
XP_per_round_loss = 50

# Level up formula
def xp_needed_for_level(level):
    return 500 + (level * 250)
    
# Example:
# Level 1→2: 750 XP
# Level 2→3: 1000 XP
# Level 3→4: 1250 XP
# Level 9→10: 2750 XP
```

### Total XP to Max Level 10:
```
Total: 14,750 XP
Average kills needed: ~150 kills
Average matches: 20-30 matches
```

---

## 🎮 Ability Implementation Guide

### Ability Activation Keys:
```
Ultimate: Q
Ability 2: E
Ability 3: F (or 3)
```

### Cooldown Display:
- On-screen HUD shows ability icons
- Grayed out during cooldown
- Number displays remaining seconds
- Visual fill animation when ready

### Visual Effects Requirements:
```
Orc:
  - Orange damage numbers (Critical Strike)
  - Screen shake (Bash)
  - Green resurrection glow (Reincarnation)

Undead:
  - Red healing particles (Vampiric Aura)
  - Transparent player model (Levitation)
  - Purple damage aura (Unholy Aura)
  
Human:
  - Blue shield shimmer (Devotion Aura)
  - Transparency shader (Invisibility)
  - Blue flash teleport (Teleport)

Night Elf:
  - Blue afterimage trail (Evasion)
  - Rapid dash blur (Blink)
  - Green thorns particles (Thorns Aura)

Blood Elf:
  - Blue mana shield glow (Mana Shield)
  - Purple projectile (Arcane Blast)
  - Red mana drain beam (Mana Burn)

Troll:
  - Red rage aura (Berserker Rage)
  - Flying axe projectiles (Throwing Axes)
  - Green healing pulse (Regeneration)

Dwarf:
  - Gray metallic skin (Iron Skin)
  - Stone texture (Entrenchment)
  - Brown ground shockwave (Ground Slam)

Celestial:
  - Golden invulnerability glow (Divine Shield)
  - Green healing wave projectile (Healing Wave)
  - Golden team aura (Aura of Light)
```

---

## 💡 Strategy Guide

### Team Compositions (5v5):

#### **Balanced Composition:**
```
1x Human (Versatile)
1x Orc (Tank/Entry)
1x Night Elf (AWPer)
1x Undead (Aggressive)
1x Celestial (Support)

Strategy: Well-rounded for all situations
```

#### **Aggressive Rush:**
```
2x Orc (Tanks)
2x Undead (Life steal sustain)
1x Troll (Berserker damage)

Strategy: Fast pushes, overwhelm defense
```

#### **Defensive Hold:**
```
2x Dwarf (Anchors)
1x Human (Mid control)
1x Celestial (Healing)
1x Night Elf (AWP hold)

Strategy: Hold sites, trade efficiently
```

#### **Ability-Heavy:**
```
2x Blood Elf (Mage damage)
1x Human (Utility)
1x Celestial (Support)
1x Night Elf (Mobility)

Strategy: Cooldown rotation, outplay
```

---

## 🎯 Advanced Balance Calculations

### Effective HP Calculation:
```python
# Orc Example
base_hp = 100
orc_multiplier = 1.30
orc_hp = base_hp * orc_multiplier  # 130 HP

# Dwarf with Iron Skin Level 5
dwarf_hp = 100 * 1.25  # 125 HP
dwarf_armor = 30
effective_hp = dwarf_hp + dwarf_armor  # 155 effective HP

# Human with Devotion Aura Level 5
human_hp = 100
damage_reduction = 0.15  # 15%
effective_hp = human_hp / (1 - damage_reduction)  # 117.6 effective HP
```

### DPS Calculations:
```python
# AK-47 Base
damage_per_shot = 36
fire_rate = 0.1  # seconds between shots
base_dps = damage_per_shot / fire_rate  # 360 DPS

# Orc with Critical Strike Level 5 (average)
crit_chance = 0.20
crit_multiplier = 2.5
expected_damage = damage_per_shot * ((1 - crit_chance) + (crit_chance * crit_multiplier))
# = 36 * (0.8 + 0.5) = 46.8 per shot
orc_dps = 46.8 / 0.1  # 468 DPS (+30% average)

# Troll at 90% HP missing (Berserker Rage max)
damage_bonus = 1.20  # +20%
attack_speed_bonus = 1.40  # +40% attack speed
troll_damage = damage_per_shot * damage_bonus  # 43.2
troll_fire_rate = 0.1 / attack_speed_bonus  # 0.071 seconds
troll_dps = 43.2 / 0.071  # 608 DPS (+69% at low HP!)
```

### Time-to-Kill (TTK):
```python
# AK-47 vs different races (body shots, no armor)
def ttk(target_hp, weapon_damage, fire_rate):
    shots_needed = ceil(target_hp / weapon_damage)
    return (shots_needed - 1) * fire_rate

# vs Night Elf (85 HP)
ttk_nelf = ceil(85 / 36) = 3 shots = 0.2s

# vs Orc (130 HP)
ttk_orc = ceil(130 / 36) = 4 shots = 0.3s

# vs Orc with Human's Devotion Aura (130 HP, 15% DR)
effective_damage = 36 * 0.85 = 30.6
ttk_orc_devotion = ceil(130 / 30.6) = 5 shots = 0.4s
```

---

## 🔧 Implementation Checklist

### Phase 1: Core Races (Week 1-2)
- [ ] Orc (simplest - passive abilities)
- [ ] Human (basic active abilities)
- [ ] Undead (passive effects)

### Phase 2: Advanced Races (Week 3-4)
- [ ] Night Elf (evasion mechanics)
- [ ] Dwarf (armor system)
- [ ] Troll (dynamic stats)

### Phase 3: Complex Races (Week 5-6)
- [ ] Blood Elf (mana system)
- [ ] Celestial (targeting system)

### Phase 4: Weapons (Week 7-8)
- [ ] All pistols (7 weapons)
- [ ] All SMGs (2 weapons)
- [ ] All rifles (7 weapons)
- [ ] All snipers (2 weapons)
- [ ] All shotguns (2 weapons)

### Phase 5: Balance & Polish (Week 9-10)
- [ ] Ability cooldown adjustments
- [ ] Damage multiplier tuning
- [ ] Visual effects polish
- [ ] Sound effects
- [ ] Playtesting and iteration

---

## 📊 Balance Testing Metrics

### What to Track:
```yaml
Per Race:
  - Win rate (target: 48-52%)
  - Pick rate (target: 10-15% each)
  - K/D ratio (target: 0.9-1.1)
  - Average level (should reach 6-8 per match)
  - Ability usage rate (should be >60% for active abilities)

Per Weapon:
  - Pick rate in each price bracket
  - Kill rate relative to other weapons
  - Accuracy percentage
  - Preferred races for each weapon

Per Map:
  - Dominant races on each map
  - Defensive vs Offensive win rates by race
  - Ability effectiveness (open vs closed maps)
```

### Red Flags:
- Any race above 55% win rate = TOO STRONG
- Any race below 45% win rate = TOO WEAK
- Pick rate <5% = Not fun or too complex
- Pick rate >20% = Too popular, others may be weak
- Ability unused >50% = Unclear or not useful

---

## 🎮 Quick Reference Cards

### For New Players:

#### **"I want easy races"**
1. **Orc** - Simple passives, high HP, forgiving
2. **Human** - Balanced, no extreme weaknesses
3. **Dwarf** - Tanky, simple to understand

#### **"I'm a good aimer"**
1. **Night Elf** - Rewards accuracy, mobile
2. **Blood Elf** - High damage, skill shots
3. **Undead** - Sustain through hits

#### **"I like abilities"**
1. **Blood Elf** - Active ability gameplay
2. **Human** - Teleport and invisibility
3. **Celestial** - Multiple active abilities

#### **"I play aggressive"**
1. **Orc** - Tank damage, push forward
2. **Troll** - More dangerous at low HP
3. **Undead** - Life steal encourages fighting

#### **"I play defensive"**
1. **Dwarf** - Entrench on sites
2. **Human** - Damage reduction
3. **Celestial** - Heal and shield allies

---

## 🏆 Competitive Balance

### Tournament Rules:
```yaml
Race Restrictions:
  - Max 2 of same race per team (prevents stacking)
  - Ban/Pick phase for ranked play
  - Each team bans 1 race before match

Ability Rules:
  - Ultimate abilities disabled for first 3 rounds (economy warmup)
  - All abilities reset between halves
  - Celestial healing counts toward MVP points

Money Adjustments:
  - Starting money: $800 (same as CS 1.6)
  - Max money: $16,000
  - Loss bonus: $1400-3400 (standard CS 1.6)
  - Shotgun kill bonus maintained (+$600)
```

### Map Balance:
```
Open Maps (dust2, mirage):
  - Favor: Night Elf, Celestial, Dwarf (long-range)
  - Disfavor: Orc, Troll (too slow)

Closed Maps (inferno, nuke):
  - Favor: Orc, Undead, Troll (close combat)
  - Disfavor: Night Elf, Dwarf (less mobility value)

Balanced Maps (cache, overpass):
  - All races viable
  - Best for competitive play
```

---

## 📈 Progression Rewards

### Level Milestones:
```yaml
Level 3:
  - Unlock: Race-specific spray paint
  - Achievement: "Getting Started"

Level 5:
  - Unlock: All abilities maxed for race
  - Achievement: "Mastery"

Level 7:
  - Unlock: Rare weapon skin for race color
  - Achievement: "Dedicated"

Level 10:
  - Unlock: Animated race emblem
  - Achievement: "Legend"
  - Reward: +5% XP for this race permanently
```

### Prestige System:
```yaml
Max Level 10 → Reset to Level 1 with Prestige

Prestige Benefits:
  - Keep: All unlocked skins and achievements
  - Gain: Prestige star icon next to name
  - Gain: +10% XP gain for faster re-leveling
  - Unlock: Exclusive prestige-only skins
  
Max Prestige: 10 (resets)
Total Max: Prestige 10, Level 10 (P10 L10)
```

---

## 🎨 Visual Style Guide

### Race Color Themes:
```
Orc: Orange/Red (#FF6B35)
Undead: Purple/Black (#6A0572)
Human: Blue/White (#4A90E2)
Night Elf: Teal/Silver (#00D9FF)
Blood Elf: Pink/Purple (#FF69B4)
Troll: Red/Brown (#8B4513)
Dwarf: Gray/Bronze (#A0A0A0)
Celestial: Gold/White (#FFD700)
```

### UI Elements:
```
Health Bar Colors:
  - 100-75% HP: Green
  - 74-50% HP: Yellow
  - 49-25% HP: Orange
  - 24-0% HP: Red

Ability Icons:
  - Ready: Full color with glow
  - Cooldown: Grayscale with timer
  - Passive: Constant subtle glow
  - Ultimate: Gold border

Damage Numbers:
  - Normal: White
  - Critical: Orange
  - Headshot: Red
  - Ability: Purple
  - Reflected: Blue
```

---

## 🎯 Weapon Economy Guide

### Eco Round Options ($1000-1500):
```
Best Choices:
  1. P250 ($300) + utility
  2. Nova ($1050) - high reward
  3. Scout ($1700) - risky but rewarding
  4. MP5 ($1500) - if you can full buy

Avoid:
  - Galil/FAMAS without armor
  - Deagle without backup plan
  - Any SMG other than MP5
```

### Force Buy ($2000-3000):
```
CT Side:
  - FAMAS ($2050) + armor
  - Scout ($1700) + armor + utility
  - Deagle ($650) + armor + utility

T Side:
  - Galil ($1800) + armor
  - Scout ($1700) + armor + nades
  - AK-47 ($2500) no armor (risky)
```

### Full Buy ($4000+):
```
CT Side:
  - M4A4/M4A1-S + armor + utility
  - AWP + armor + backup pistol
  - AUG + armor (scoped option)

T Side:
  - AK-47 + armor + utility
  - AWP + armor + backup pistol
  - SG 553 + armor (scoped option)
```

---

## 🔬 Advanced Mechanics

### Damage Stacking:
```
Base Damage: 36 (AK-47)
× Orc Damage Bonus (1.15): 41.4
× Critical Strike (2.5x): 103.5
× Headshot Multiplier (4x): 414 damage!

= Orc can deal 414 damage with crit headshot
= Instakills through any armor/HP combination
```

### Speed Stacking:
```
Base Speed: 250 units/second
× Night Elf (1.20): 300 units/second
× Knife Boost (1.0): 300 (no weapon boost)
× Celestial Aura (1.15): 345 units/second!

= Fastest possible: Night Elf + Celestial support
= 38% faster than baseline
```

### Ultimate Ability Combos:
```
Devastating Combos:
1. Orc Reincarnation + Troll Berserker Rage
   = Respawn with low HP in max rage mode

2. Night Elf Evasion + Human Devotion Aura
   = 22% dodge + 15% DR = 37% effective mitigation

3. Celestial Divine Shield + Blood Elf Mana Shield
   = 3.5s invuln + 60 mana shield = extreme survivability

4. Dwarf Entrenchment + Celestial Aura of Light
   = 50% DR + 15% speed = mobile fortress for team

5. Undead Unholy Aura + Troll Throwing Axes
   = Damage over time + slow = zone control
```

---

## 📝 Final Balance Notes

### Design Philosophy Recap:
1. **Every race viable** - No must-picks or auto-losses
2. **Counters exist** - Rock-paper-scissors dynamics
3. **Skill expression** - Better players win with any race
4. **Team synergy** - Combos reward coordination
5. **Economic balance** - Money matters, abilities don't replace gunplay

### Future Balance Patches:
```yaml
Planned Adjustments:
  - Orc Critical Strike: May reduce level 5 to 18% (from 20%)
  - Night Elf Evasion: May cap at 20% (from 22%)
  - Blood Elf Mana Shield: May reduce regen to 1.5/sec (from 2/sec)
  - Troll Berserker: May add scaling at 25-50% HP
  - Celestial Divine Shield: May reduce to 3.0s max (from 3.5s)

Reasoning: After playtesting, slight tweaks may be needed
```

### Community Feedback Loop:
```
Week 1-2: Gather data from beta testers
Week 3-4: Identify overperforming races
Week 5-6: Implement balance patch v1.1
Week 7-8: Monitor and iterate
Week 9-10: Finalize for competitive release
```

---

## 🎓 Conclusion

This guide provides a **perfectly balanced** race and weapon system inspired by the legendary CS 1.6 Warcraft mod. Every race has:

✅ **Clear identity** - Unique playstyle and fantasy
✅ **Balanced power** - No overpowered picks
✅ **Counterplay** - Ways to be countered and counter others
✅ **Skill expression** - Rewards good players
✅ **Team synergy** - Benefits coordinated play
✅ **Authentic CS** - Maintains CS 1.6 core gameplay

### Remember:
> *"Abilities enhance skill, they don't replace it."*

The best race is the one that **fits your playstyle** and **your team's strategy**. Master your chosen race, learn its counters, and outplay your opponents through skill, positioning, and teamwork.

**Good luck, and have fun!** 🎮

---

*Document Version: 1.0*  
*Last Updated: October 3, 2025*  
*Author: CS 1.7 Development Team*  
*Inspired by: Warcraft 3 Mod for CS 1.6*