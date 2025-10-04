using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages player economy - money, purchases, kill rewards, and round bonuses
/// </summary>
public class EconomySystem : MonoBehaviour
{
    [Header("Starting Money")]
    public int startingMoney = 800;
    public int pistolRoundMoney = 800;
    
    [Header("Kill Rewards")]
    public int standardKillReward = 300;
    public int knifeKillReward = 1500;
    public int sniperKillReward = 100;
    
    [Header("Round Loss Bonuses")]
    public int[] lossBonus = { 1400, 1900, 2400, 2900, 3400 };
    public int maxLossBonus = 3400;
    
    [Header("Round Win Bonuses")]
    public int roundWinBonus = 3250;
    public int bombPlantBonus = 300;
    public int bombDefuseBonus = 300;
    
    [Header("Team Money")]
    public int teamBonusPerPlayer = 0;

    // Player data
    private Dictionary<GameObject, PlayerEconomy> playerEconomies = new Dictionary<GameObject, PlayerEconomy>();

    // Singleton
    public static EconomySystem Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Register a player in the economy system
    /// </summary>
    public void RegisterPlayer(GameObject player)
    {
        if (!playerEconomies.ContainsKey(player))
        {
            playerEconomies[player] = new PlayerEconomy(startingMoney);
        }
    }

    /// <summary>
    /// Get player's current money
    /// </summary>
    public int GetPlayerMoney(GameObject player)
    {
        if (playerEconomies.ContainsKey(player))
        {
            return playerEconomies[player].CurrentMoney;
        }
        return 0;
    }

    /// <summary>
    /// Add money to player
    /// </summary>
    public void AddMoney(GameObject player, int amount)
    {
        if (playerEconomies.ContainsKey(player))
        {
            playerEconomies[player].AddMoney(amount);
            Debug.Log($"{player.name} received ${amount}. New balance: ${playerEconomies[player].CurrentMoney}");
        }
    }

    /// <summary>
    /// Deduct money from player (for purchases)
    /// </summary>
    public bool SpendMoney(GameObject player, int amount)
    {
        if (playerEconomies.ContainsKey(player))
        {
            if (playerEconomies[player].CanAfford(amount))
            {
                playerEconomies[player].SpendMoney(amount);
                Debug.Log($"{player.name} spent ${amount}. Remaining: ${playerEconomies[player].CurrentMoney}");
                return true;
            }
            else
            {
                Debug.Log($"{player.name} cannot afford ${amount}. Current: ${playerEconomies[player].CurrentMoney}");
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// Handle kill reward
    /// </summary>
    public void OnKill(GameObject killer, GameObject victim, Weapon weaponUsed)
    {
        if (!playerEconomies.ContainsKey(killer)) return;

        int reward = standardKillReward;
        
        // Determine reward based on weapon
        if (weaponUsed != null)
        {
            reward = weaponUsed.killReward;
        }

        AddMoney(killer, reward);
        playerEconomies[killer].kills++;
    }

    /// <summary>
    /// Round end - distribute bonuses
    /// </summary>
    public void OnRoundEnd(bool teamWon, GameObject[] teamPlayers, int consecutiveLosses = 0)
    {
        foreach (GameObject player in teamPlayers)
        {
            if (!playerEconomies.ContainsKey(player)) continue;

            if (teamWon)
            {
                AddMoney(player, roundWinBonus);
                playerEconomies[player].consecutiveLosses = 0;
            }
            else
            {
                // Loss bonus increases each consecutive loss
                int lossIndex = Mathf.Min(consecutiveLosses, lossBonus.Length - 1);
                int lossMoney = lossBonus[lossIndex];
                AddMoney(player, lossMoney);
                playerEconomies[player].consecutiveLosses = consecutiveLosses + 1;
            }
        }
    }

    /// <summary>
    /// Bomb plant bonus
    /// </summary>
    public void OnBombPlant(GameObject planter)
    {
        if (playerEconomies.ContainsKey(planter))
        {
            AddMoney(planter, bombPlantBonus);
        }
    }

    /// <summary>
    /// Bomb defuse bonus
    /// </summary>
    public void OnBombDefuse(GameObject defuser)
    {
        if (playerEconomies.ContainsKey(defuser))
        {
            AddMoney(defuser, bombDefuseBonus);
        }
    }

    /// <summary>
    /// New round - reset buy status
    /// </summary>
    public void OnNewRound()
    {
        foreach (var economy in playerEconomies.Values)
        {
            economy.hasBoughtThisRound = false;
        }
    }

    /// <summary>
    /// Buy weapon for player
    /// </summary>
    public bool BuyWeapon(GameObject player, WeaponData weaponData)
    {
        if (SpendMoney(player, weaponData.price))
        {
            // TODO: Integrate with weapon system to give weapon to player
            playerEconomies[player].hasBoughtThisRound = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Buy armor
    /// </summary>
    public bool BuyArmor(GameObject player, int armorCost = 650)
    {
        if (SpendMoney(player, armorCost))
        {
            // TODO: Integrate with armor system
            return true;
        }
        return false;
    }

    /// <summary>
    /// Buy kevlar + helmet
    /// </summary>
    public bool BuyFullArmor(GameObject player, int fullArmorCost = 1000)
    {
        if (SpendMoney(player, fullArmorCost))
        {
            // TODO: Integrate with armor system
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get player stats
    /// </summary>
    public PlayerEconomy GetPlayerStats(GameObject player)
    {
        if (playerEconomies.ContainsKey(player))
        {
            return playerEconomies[player];
        }
        return null;
    }

    /// <summary>
    /// Reset player economy (new match)
    /// </summary>
    public void ResetPlayerEconomy(GameObject player)
    {
        if (playerEconomies.ContainsKey(player))
        {
            playerEconomies[player] = new PlayerEconomy(pistolRoundMoney);
        }
    }

    /// <summary>
    /// Reset all economies (new match)
    /// </summary>
    public void ResetAllEconomies()
    {
        foreach (var player in playerEconomies.Keys)
        {
            playerEconomies[player] = new PlayerEconomy(pistolRoundMoney);
        }
    }
}

/// <summary>
/// Player economy data
/// </summary>
[System.Serializable]
public class PlayerEconomy
{
    public int CurrentMoney { get; private set; }
    public int TotalEarned { get; private set; }
    public int TotalSpent { get; private set; }
    public int kills;
    public int deaths;
    public int consecutiveLosses;
    public bool hasBoughtThisRound;

    public PlayerEconomy(int startingMoney)
    {
        CurrentMoney = startingMoney;
        TotalEarned = startingMoney;
        TotalSpent = 0;
        kills = 0;
        deaths = 0;
        consecutiveLosses = 0;
        hasBoughtThisRound = false;
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        TotalEarned += amount;
        CurrentMoney = Mathf.Clamp(CurrentMoney, 0, 16000); // CS money cap
    }

    public void SpendMoney(int amount)
    {
        CurrentMoney -= amount;
        TotalSpent += amount;
    }

    public bool CanAfford(int amount)
    {
        return CurrentMoney >= amount;
    }
}
