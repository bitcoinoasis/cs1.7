using UnityEngine;

public class DebugCommands : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    private PlayerExperience playerXP;
    private PlayerHealth playerHealth;
    private WeaponManager weaponManager;
    private AbilitySystem abilitySystem;
    
    [Header("Debug Settings")]
    public bool enableDebugCommands = true;
    public int xpPerCommand = 1000;
    public int levelsPerCommand = 1;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerXP = player.GetComponent<PlayerExperience>();
            playerHealth = player.GetComponent<PlayerHealth>();
            weaponManager = player.GetComponent<WeaponManager>();
            abilitySystem = player.GetComponent<AbilitySystem>();
        }
    }
    
    private void Update()
    {
        if (!enableDebugCommands) return;
        
        // XP and Leveling Commands
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            AddXP();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            LevelUp();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            MaxLevel();
        }
        
        // Money Command
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddMoney();
        }
        
        // Bot Commands
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            SpawnBots();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            ClearBots();
        }
        
        // Health Commands
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            FullHealth();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            ToggleGodMode();
        }
        
        // Ability Commands
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            ResetAllCooldowns();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            AddSkillPoint();
        }
        
        // Weapon Commands (Minus key)
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            RefillAmmo();
        }
    }
    
    private void AddXP()
    {
        if (playerXP != null)
        {
            playerXP.AddExperience(xpPerCommand);
            Debug.Log($"Added {xpPerCommand} XP. Total: {playerXP.currentXP}, Level: {playerXP.currentLevel}");
        }
    }
    
    private void LevelUp()
    {
        if (playerXP != null)
        {
            int xpNeeded = playerXP.GetXPForNextLevel() - playerXP.currentXP;
            if (xpNeeded > 0)
            {
                playerXP.AddExperience(xpNeeded);
                Debug.Log($"Leveled up! Now level {playerXP.currentLevel}");
            }
            else
            {
                Debug.Log("Already at max level!");
            }
        }
    }
    
    private void MaxLevel()
    {
        if (playerXP != null)
        {
            int maxLevel = playerXP.GetMaxLevel();
            while (playerXP.currentLevel < maxLevel)
            {
                int xpNeeded = playerXP.GetXPForNextLevel() - playerXP.currentXP;
                playerXP.AddExperience(xpNeeded + 1);
            }
            Debug.Log($"Reached max level {playerXP.currentLevel}!");
        }
    }
    
    private void FullHealth()
    {
        if (playerHealth != null)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
            playerHealth.armor = playerHealth.maxArmor;
            Debug.Log("Full health and armor restored!");
        }
    }
    
    private bool godMode = false;
    private void ToggleGodMode()
    {
        godMode = !godMode;
        
        if (playerHealth != null)
        {
            if (godMode)
            {
                playerHealth.maxHealth = 999999;
                playerHealth.currentHealth = 999999;
                Debug.Log("God Mode: ON");
            }
            else
            {
                playerHealth.maxHealth = 100;
                playerHealth.currentHealth = 100;
                Debug.Log("God Mode: OFF");
            }
        }
    }
    
    private void ResetAllCooldowns()
    {
        if (abilitySystem != null)
        {
            abilitySystem.ResetAllCooldowns();
        }
    }
    
    private void AddSkillPoint()
    {
        if (playerXP != null)
        {
            playerXP.skillPoints++;
            Debug.Log($"Added skill point! Total: {playerXP.skillPoints}");
        }
    }
    
    private void AddMoney()
    {
        if (GameManager.Instance != null && player != null)
        {
            GameManager.Instance.AddMoney(player, 5000);
            int totalMoney = GameManager.Instance.GetPlayerMoney(player);
            Debug.Log($"Added $5000! Total: ${totalMoney}");
        }
    }
    
    private void SpawnBots()
    {
        BotSpawner spawner = FindFirstObjectByType<BotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnAllBots();
            Debug.Log("Bots spawned!");
        }
        else
        {
            Debug.LogWarning("No BotSpawner found in scene!");
        }
    }
    
    private void ClearBots()
    {
        BotSpawner spawner = FindFirstObjectByType<BotSpawner>();
        if (spawner != null)
        {
            spawner.DespawnAllBots();
            Debug.Log("Bots cleared!");
        }
        else
        {
            Debug.LogWarning("No BotSpawner found in scene!");
        }
    }
    
    private void RefillAmmo()
    {
        if (weaponManager != null)
        {
            Weapon currentWeapon = weaponManager.GetCurrentWeapon();
            if (currentWeapon != null)
            {
                currentWeapon.AddAmmo(999);
                Debug.Log("Ammo refilled!");
            }
        }
    }
    
    // Display debug commands on screen
    private void OnGUI()
    {
        if (!enableDebugCommands) return;
        
        GUI.color = Color.white;
        GUIStyle style = new GUIStyle();
        style.fontSize = 12;
        style.normal.textColor = Color.yellow;
        
        string helpText = "=== DEBUG COMMANDS ===\n";
        helpText += "1 - Add " + xpPerCommand + " XP\n";
        helpText += "2 - Level Up\n";
        helpText += "3 - Max Level\n";
        helpText += "4 - Add $5000\n";
        helpText += "5 - Spawn Bots\n";
        helpText += "6 - Clear Bots\n";
        helpText += "7 - Full Health\n";
        helpText += "8 - Toggle God Mode\n";
        helpText += "9 - Reset Cooldowns\n";
        helpText += "0 - Add Skill Point\n";
        helpText += "- (Minus) - Refill Ammo\n";
        helpText += "B - Buy Menu (Unlimited $)\n";
        helpText += "R - Reload Weapon\n";
        
        GUI.Label(new Rect(10, 10, 300, 350), helpText, style);
    }
}
