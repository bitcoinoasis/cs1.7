# GUID Fix Report - Race Asset Files

## Date
October 4, 2025

## Issue Summary
Unity reported "Could not extract GUID" errors for all 8 race asset files due to placeholder GUID values instead of actual Unity asset references.

## Root Cause
When the race asset files were initially created, they used placeholder strings like `CRITICAL_STRIKE_GUID`, `BASH_GUID`, etc., instead of actual Unity GUIDs that reference the ability ScriptableObject files.

## Error Messages
```
Could not extract GUID in text file Assets/Data/Races/Race_Orc.asset at line 29.
Could not extract GUID in text file Assets/Data/Races/Race_Orc.asset at line 30.
Could not extract GUID in text file Assets/Data/Races/Race_Orc.asset at line 31.
```

This error pattern repeated for all 8 race files (Orc, Human, Undead, NightElf, BloodElf, Troll, Dwarf, Celestial).

## Solution
Retrieved actual GUIDs from the `.meta` files of all ability assets and replaced placeholder strings with proper Unity GUID references.

## Files Fixed

### 1. Race_Orc.asset
**Abilities:**
- Ultimate: Critical Strike → `0335bde2fba504955879762f64b226a1`
- Ability2: Bash → `f06ea4bfe9ffd47afb5d62d2ca5ad908`
- Ability3: Reincarnation → `5fc9898c1e6fd48b5881c5365734f05f`

### 2. Race_Human.asset
**Abilities:**
- Ultimate: Devotion Aura → `a9e7c1b1258f948429c1134de337597d`
- Ability2: Invisibility → `9ff4ccb89bca74863a0c6ffa399f7317`
- Ability3: Teleport → `7a06a95251c2241edace28ada5b1486f`

### 3. Race_Undead.asset
**Abilities:**
- Ultimate: Vampiric Aura → `6049c36ff7f364a0f9190c212aa23ff5`
- Ability2: Levitation → `e05d060a3244843abbecebce8a152b9d`
- Ability3: Unholy Aura → `24f11708d09754a9f9f2b4f0a2ae43df`

### 4. Race_NightElf.asset
**Abilities:**
- Ultimate: Evasion → `a27f5667975af4c01a6deb86b9eefb8a`
- Ability2: Blink → `652baeeedf5184644bcbe9f1c9b58455`
- Ability3: Thorns Aura → `077a464d84f3f45e98a64029a16c7138`

### 5. Race_BloodElf.asset
**Abilities:**
- Ultimate: Mana Shield → `b6d51681ef41443d4bc89a4093743405`
- Ability2: Arcane Missiles → `7a97fb02e54fa44009d3e2acadbba341`
- Ability3: Mana Burn → `3fb482955577240a0b602f860a79e1c1`

### 6. Race_Troll.asset
**Abilities:**
- Ultimate: Berserker → `f15401d5998dd429db4aa15b2a1277f0`
- Ability2: Regeneration → `1f57a6007e2b84a08976bade9421d405`
- Ability3: Axe Mastery → `0949f7d72a2374f7faffb7caad3eaafd`

### 7. Race_Dwarf.asset
**Abilities:**
- Ultimate: Stone Skin → `2b6004a4314fe407eaf0c079c6262400`
- Ability2: Shield Bash → `49f8cb425d6ac48bab95c034404ba751`
- Ability3: Heroic Strike → `6a5aa0a35366048de8fb6020df074cdd`

### 8. Race_Celestial.asset
**Abilities:**
- Ultimate: Holy Light → `bad66c954b0654837967911326a4745a`
- Ability2: Divine Shield → `f8546148915444a4f9ccb2dffbdc6a2b`
- Ability3: Resurrection → `eb8533466c80d4ad0b980af7bc1796eb`

## GUID Mapping Reference

### Orc Abilities
```
Orc_CriticalStrike.asset: 0335bde2fba504955879762f64b226a1
Orc_Bash.asset:           f06ea4bfe9ffd47afb5d62d2ca5ad908
Orc_Reincarnation.asset:  5fc9898c1e6fd48b5881c5365734f05f
```

### Human Abilities
```
Human_DevotionAura.asset: a9e7c1b1258f948429c1134de337597d
Human_Invisibility.asset: 9ff4ccb89bca74863a0c6ffa399f7317
Human_Teleport.asset:     7a06a95251c2241edace28ada5b1486f
```

### Undead Abilities
```
Undead_VampiricAura.asset: 6049c36ff7f364a0f9190c212aa23ff5
Undead_Levitation.asset:   e05d060a3244843abbecebce8a152b9d
Undead_UnholyAura.asset:   24f11708d09754a9f9f2b4f0a2ae43df
```

### NightElf Abilities
```
NightElf_Evasion.asset:    a27f5667975af4c01a6deb86b9eefb8a
NightElf_Blink.asset:      652baeeedf5184644bcbe9f1c9b58455
NightElf_ThornsAura.asset: 077a464d84f3f45e98a64029a16c7138
```

### BloodElf Abilities
```
BloodElf_ManaShield.asset:     b6d51681ef41443d4bc89a4093743405
BloodElf_ArcaneMissiles.asset: 7a97fb02e54fa44009d3e2acadbba341
BloodElf_ManaBurn.asset:       3fb482955577240a0b602f860a79e1c1
```

### Troll Abilities
```
Troll_Berserker.asset:    f15401d5998dd429db4aa15b2a1277f0
Troll_Regeneration.asset: 1f57a6007e2b84a08976bade9421d405
Troll_AxeMastery.asset:   0949f7d72a2374f7faffb7caad3eaafd
```

### Dwarf Abilities
```
Dwarf_StoneSkin.asset:    2b6004a4314fe407eaf0c079c6262400
Dwarf_ShieldBash.asset:   49f8cb425d6ac48bab95c034404ba751
Dwarf_HeroicStrike.asset: 6a5aa0a35366048de8fb6020df074cdd
```

### Celestial Abilities
```
Celestial_HolyLight.asset:    bad66c954b0654837967911326a4745a
Celestial_DivineShield.asset: f8546148915444a4f9ccb2dffbdc6a2b
Celestial_Resurrection.asset: eb8533466c80d4ad0b980af7bc1796eb
```

## Verification Steps

1. ✅ Extracted all ability GUIDs from `.meta` files
2. ✅ Replaced all placeholder GUIDs in 8 race asset files
3. ✅ Verified no remaining `_GUID` placeholders
4. ✅ Compilation check passed (0 errors)
5. ✅ Committed and pushed to GitHub

## Git History

**Commits:**
1. "Fix DebugCommands - use correct BotSpawner method name (ClearAllBots)"
2. "Fix all race asset files - replace placeholder GUIDs with actual ability GUIDs"

**Branch:** main
**Files Changed:** 8 race assets + 1 debug script
**Repository:** https://github.com/bitcoinoasis/cs1.7

## Impact

- All race ScriptableObjects now properly reference their 3 abilities
- Unity can correctly load and serialize race data
- PlayerRace system can now properly access ultimateAbility, ability2, and ability3
- AbilitySystem will correctly initialize abilities when race is selected
- No more Unity console spam about GUID extraction errors

## Next Steps

1. Open Unity and verify race assets load correctly in Inspector
2. Test race selection in-game to confirm abilities are properly assigned
3. Verify AbilitySystem initializes all 3 abilities for each race
4. Test ability activation for each race

## Notes

- This was likely caused by initial data creation using placeholder values
- Future ScriptableObject creation should be done through Unity Editor to auto-generate proper GUIDs
- `.meta` files are the source of truth for Unity asset GUIDs
