#!/bin/bash
# Unity Project Reorganization Script
# Reorganizes project structure to industry standards while preserving git history

set -e  # Exit on error

echo "========================================="
echo "CS 1.7 Project Reorganization"
echo "========================================="
echo ""

# Store the project root
PROJECT_ROOT="/Users/syedshah/cs1.7"
cd "$PROJECT_ROOT"

echo "Step 1: Creating new folder structure..."
echo ""

# Create Core folders
mkdir -p "Assets/Scripts/Core"
mkdir -p "Assets/Scripts/Gameplay/Player"
mkdir -p "Assets/Scripts/Gameplay/Bot"
mkdir -p "Assets/Scripts/Gameplay/Combat"
mkdir -p "Assets/Scripts/Gameplay/GameModes"
mkdir -p "Assets/Scripts/Systems/Economy"
mkdir -p "Assets/Scripts/Systems/Abilities/Implementations"
mkdir -p "Assets/Scripts/Systems/Races"
mkdir -p "Assets/Scripts/UI/Menus"
mkdir -p "Assets/Scripts/UI/HUD"
mkdir -p "Assets/Scripts/UI/Feedback"
mkdir -p "Assets/Scripts/Data/Races"
mkdir -p "Assets/Scripts/Data/Abilities"
mkdir -p "Assets/Scripts/Data/Weapons"
mkdir -p "Assets/Scripts/Utilities"
mkdir -p "Assets/Scripts/Initialization"
mkdir -p "Assets/Scripts/Editor/Tools"
mkdir -p "Assets/Scripts/Editor/Inspectors"
mkdir -p "Assets/Tests/EditMode"
mkdir -p "Assets/Tests/PlayMode"
mkdir -p "Assets/Prefabs/Player"
mkdir -p "Assets/Prefabs/Bots"
mkdir -p "Assets/Prefabs/Weapons"
mkdir -p "Assets/Prefabs/UI"
mkdir -p "Assets/Prefabs/Environment"

echo "✓ New folder structure created"
echo ""

echo "Step 2: Moving files to new locations (preserving git history)..."
echo ""

# Move Player scripts
if [ -f "Assets/Scripts/Player/PlayerController.cs" ]; then
    git mv "Assets/Scripts/Player/PlayerController.cs" "Assets/Scripts/Gameplay/Player/"
    echo "✓ Moved PlayerController.cs"
fi

if [ -f "Assets/Scripts/Player/PlayerHealth.cs" ]; then
    git mv "Assets/Scripts/Player/PlayerHealth.cs" "Assets/Scripts/Gameplay/Player/"
    echo "✓ Moved PlayerHealth.cs"
fi

# Move Bot scripts
if [ -f "Assets/Scripts/BotAI.cs" ]; then
    git mv "Assets/Scripts/BotAI.cs" "Assets/Scripts/Gameplay/Bot/"
    echo "✓ Moved BotAI.cs"
fi

if [ -f "Assets/Scripts/BotSpawner.cs" ]; then
    git mv "Assets/Scripts/BotSpawner.cs" "Assets/Scripts/Gameplay/Bot/"
    echo "✓ Moved BotSpawner.cs"
fi

if [ -f "Assets/Scripts/Bot/Bot.cs" ]; then
    git mv "Assets/Scripts/Bot/Bot.cs" "Assets/Scripts/Gameplay/Bot/BotController.cs"
    echo "✓ Moved and renamed Bot.cs -> BotController.cs"
fi

# Move Combat scripts
if [ -f "Assets/Scripts/Game/Hitbox.cs" ]; then
    git mv "Assets/Scripts/Game/Hitbox.cs" "Assets/Scripts/Gameplay/Combat/"
    echo "✓ Moved Hitbox.cs"
fi

if [ -f "Assets/Scripts/Weapons/Weapon.cs" ]; then
    git mv "Assets/Scripts/Weapons/Weapon.cs" "Assets/Scripts/Gameplay/Combat/"
    echo "✓ Moved Weapon.cs"
fi

# Move GameMode scripts
if [ -f "Assets/Scripts/GameModeManager.cs" ]; then
    git mv "Assets/Scripts/GameModeManager.cs" "Assets/Scripts/Gameplay/GameModes/"
    echo "✓ Moved GameModeManager.cs"
fi

# Move Economy system
if [ -f "Assets/Scripts/EconomySystem.cs" ]; then
    git mv "Assets/Scripts/EconomySystem.cs" "Assets/Scripts/Systems/Economy/"
    echo "✓ Moved EconomySystem.cs"
fi

# Move Ability system
if [ -f "Assets/Scripts/Abilities/AbilitySystem.cs" ]; then
    git mv "Assets/Scripts/Abilities/AbilitySystem.cs" "Assets/Scripts/Systems/Abilities/"
    echo "✓ Moved AbilitySystem.cs"
fi

if [ -f "Assets/Scripts/Abilities/Ability.cs" ]; then
    git mv "Assets/Scripts/Abilities/Ability.cs" "Assets/Scripts/Systems/Abilities/"
    echo "✓ Moved Ability.cs"
fi

# Move Ability implementations
if [ -d "Assets/Scripts/Abilities/Orc" ]; then
    git mv "Assets/Scripts/Abilities/Orc" "Assets/Scripts/Systems/Abilities/Implementations/"
    echo "✓ Moved Orc abilities"
fi

if [ -d "Assets/Scripts/Abilities/Human" ]; then
    git mv "Assets/Scripts/Abilities/Human" "Assets/Scripts/Systems/Abilities/Implementations/"
    echo "✓ Moved Human abilities"
fi

if [ -d "Assets/Scripts/Abilities/Undead" ]; then
    git mv "Assets/Scripts/Abilities/Undead" "Assets/Scripts/Systems/Abilities/Implementations/"
    echo "✓ Moved Undead abilities"
fi

if [ -d "Assets/Scripts/Abilities/NightElf" ]; then
    git mv "Assets/Scripts/Abilities/NightElf" "Assets/Scripts/Systems/Abilities/Implementations/"
    echo "✓ Moved NightElf abilities"
fi

# Move Race system
if [ -f "Assets/Scripts/Warcraft/PlayerRace.cs" ]; then
    git mv "Assets/Scripts/Warcraft/PlayerRace.cs" "Assets/Scripts/Systems/Races/"
    echo "✓ Moved PlayerRace.cs"
fi

if [ -f "Assets/Scripts/Warcraft/PlayerExperience.cs" ]; then
    git mv "Assets/Scripts/Warcraft/PlayerExperience.cs" "Assets/Scripts/Systems/Races/"
    echo "✓ Moved PlayerExperience.cs"
fi

if [ -f "Assets/Scripts/Warcraft/RaceData.cs" ]; then
    git mv "Assets/Scripts/Warcraft/RaceData.cs" "Assets/Scripts/Data/Races/"
    echo "✓ Moved RaceData.cs"
fi

if [ -f "Assets/Scripts/Warcraft/AbilityData.cs" ]; then
    git mv "Assets/Scripts/Warcraft/AbilityData.cs" "Assets/Scripts/Data/Abilities/"
    echo "✓ Moved AbilityData.cs"
fi

# Move Weapons
if [ -f "Assets/Scripts/Weapons/WeaponManager.cs" ]; then
    git mv "Assets/Scripts/Weapons/WeaponManager.cs" "Assets/Scripts/Weapons/"
    echo "✓ WeaponManager.cs already in correct location"
fi

if [ -f "Assets/Scripts/Weapons/WeaponData.cs" ]; then
    git mv "Assets/Scripts/Weapons/WeaponData.cs" "Assets/Scripts/Data/Weapons/"
    echo "✓ Moved WeaponData.cs"
fi

if [ -f "Assets/Scripts/Weapons/BulletEffect.cs" ]; then
    git mv "Assets/Scripts/Weapons/BulletEffect.cs" "Assets/Scripts/Weapons/"
    echo "✓ BulletEffect.cs already in correct location"
fi

# Move UI scripts
if [ -f "Assets/Scripts/Game/BuyMenu.cs" ]; then
    git mv "Assets/Scripts/Game/BuyMenu.cs" "Assets/Scripts/UI/Menus/"
    echo "✓ Moved BuyMenu.cs"
fi

if [ -f "Assets/Scripts/UI/BuyMenuUI.cs" ]; then
    git mv "Assets/Scripts/UI/BuyMenuUI.cs" "Assets/Scripts/UI/Menus/"
    echo "✓ Moved BuyMenuUI.cs"
fi

if [ -f "Assets/Scripts/Game/SimpleBuyMenu.cs" ]; then
    git mv "Assets/Scripts/Game/SimpleBuyMenu.cs" "Assets/Scripts/UI/Menus/"
    echo "✓ Moved SimpleBuyMenu.cs"
fi

if [ -f "Assets/Scripts/UI/RaceSelectionMenu.cs" ]; then
    git mv "Assets/Scripts/UI/RaceSelectionMenu.cs" "Assets/Scripts/UI/Menus/"
    echo "✓ Moved RaceSelectionMenu.cs"
fi

if [ -f "Assets/Scripts/UI/RaceSelectionUI.cs" ]; then
    git mv "Assets/Scripts/UI/RaceSelectionUI.cs" "Assets/Scripts/UI/Menus/"
    echo "✓ Moved RaceSelectionUI.cs"
fi

if [ -f "Assets/Scripts/UI/ScoreboardUI.cs" ]; then
    git mv "Assets/Scripts/UI/ScoreboardUI.cs" "Assets/Scripts/UI/Menus/"
    echo "✓ Moved ScoreboardUI.cs"
fi

# Move HUD scripts
if [ -f "Assets/Scripts/UI/GameHUD.cs" ]; then
    git mv "Assets/Scripts/UI/GameHUD.cs" "Assets/Scripts/UI/HUD/"
    echo "✓ Moved GameHUD.cs"
fi

if [ -f "Assets/Scripts/UI/AbilityHUD.cs" ]; then
    git mv "Assets/Scripts/UI/AbilityHUD.cs" "Assets/Scripts/UI/HUD/"
    echo "✓ Moved AbilityHUD.cs"
fi

if [ -f "Assets/Scripts/UI/SimpleHealthDisplay.cs" ]; then
    git mv "Assets/Scripts/UI/SimpleHealthDisplay.cs" "Assets/Scripts/UI/HUD/"
    echo "✓ Moved SimpleHealthDisplay.cs"
fi

if [ -f "Assets/Scripts/UI/Crosshair.cs" ]; then
    git mv "Assets/Scripts/UI/Crosshair.cs" "Assets/Scripts/UI/HUD/"
    echo "✓ Moved Crosshair.cs"
fi

# Move Feedback scripts
if [ -f "Assets/Scripts/UI/KillFeedUI.cs" ]; then
    git mv "Assets/Scripts/UI/KillFeedUI.cs" "Assets/Scripts/UI/Feedback/"
    echo "✓ Moved KillFeedUI.cs"
fi

# Move Utilities
if [ -f "Assets/Scripts/Game/DebugCommands.cs" ]; then
    git mv "Assets/Scripts/Game/DebugCommands.cs" "Assets/Scripts/Utilities/"
    echo "✓ Moved DebugCommands.cs"
fi

# Move Initialization scripts
if [ -f "Assets/Scripts/Game/SceneInitializer.cs" ]; then
    git mv "Assets/Scripts/Game/SceneInitializer.cs" "Assets/Scripts/Initialization/"
    echo "✓ Moved SceneInitializer.cs"
fi

if [ -f "Assets/Scripts/Game/BotSpawnerAutoSetup.cs" ]; then
    git mv "Assets/Scripts/Game/BotSpawnerAutoSetup.cs" "Assets/Scripts/Initialization/"
    echo "✓ Moved BotSpawnerAutoSetup.cs"
fi

# Move Core/GameManager
if [ -f "Assets/Scripts/Game/GameManager.cs" ]; then
    git mv "Assets/Scripts/Game/GameManager.cs" "Assets/Scripts/Core/"
    echo "✓ Moved GameManager.cs"
fi

# Move Editor scripts
if [ -f "Assets/Scripts/Editor/CreateBotPrefab.cs" ]; then
    git mv "Assets/Scripts/Editor/CreateBotPrefab.cs" "Assets/Scripts/Editor/Tools/"
    echo "✓ Moved CreateBotPrefab.cs"
fi

if [ -f "Assets/Scripts/Editor/MCPConnectionTest.cs" ]; then
    git mv "Assets/Scripts/Editor/MCPConnectionTest.cs" "Assets/Scripts/Editor/Tools/"
    echo "✓ Moved MCPConnectionTest.cs"
fi

if [ -f "Assets/Scripts/Editor/DataResourcesMigration.cs" ]; then
    git mv "Assets/Scripts/Editor/DataResourcesMigration.cs" "Assets/Scripts/Editor/Tools/"
    echo "✓ Moved DataResourcesMigration.cs"
fi

echo ""
echo "Step 3: Cleaning up empty folders..."
echo ""

# Remove old empty folders (Unity will handle .meta files)
find Assets/Scripts -type d -empty -delete 2>/dev/null || true

echo "✓ Cleanup complete"
echo ""

echo "========================================="
echo "Reorganization Complete!"
echo "========================================="
echo ""
echo "Next steps:"
echo "1. Unity will reimport assets (may take a moment)"
echo "2. Check console for any broken references"
echo "3. Run: git status"
echo "4. Commit changes: git add -A && git commit -m 'Reorganize project structure'"
echo ""
