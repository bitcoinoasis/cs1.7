using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages different game modes - Bomb Defusal, Team Deathmatch, Deathmatch, Gun Game
/// </summary>
public class GameModeManager : MonoBehaviour
{
    [Header("Game Mode")]
    public GameMode currentGameMode = GameMode.BombDefusal;
    
    [Header("Round Settings")]
    public float roundTime = 115f; // CS standard round time
    public float buyTime = 20f;
    public int maxRounds = 30;
    public int roundsToWin = 16;
    
    [Header("Bomb Defusal Settings")]
    public float bombTimer = 45f;
    public float defuseTime = 10f;
    public float defuseTimeWithKit = 5f;
    public Transform[] bombSites;
    
    [Header("Team Deathmatch Settings")]
    public int teamKillsToWin = 50;
    
    [Header("Deathmatch Settings")]
    public int killsToWin = 30;
    public bool respawnEnabled = true;
    public float respawnDelay = 3f;
    
    [Header("Gun Game Settings")]
    public WeaponData[] gunGameWeapons;
    public int killsPerWeapon = 1;

    // State
    private float currentRoundTime;
    private float currentBuyTime;
    private int currentRound;
    private int team1Score;
    private int team2Score;
    private bool isRoundActive;
    private bool isBombPlanted;
    private GameObject plantedBomb;
    private float bombPlantTime;

    // Teams
    private List<GameObject> team1Players = new List<GameObject>();
    private List<GameObject> team2Players = new List<GameObject>();
    private Dictionary<GameObject, int> playerGunGameLevels = new Dictionary<GameObject, int>();

    // Singleton
    public static GameModeManager Instance { get; private set; }

    public enum GameMode
    {
        BombDefusal,
        TeamDeathmatch,
        Deathmatch,
        GunGame
    }

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

    void Start()
    {
        InitializeGameMode();
        StartRound();
    }

    void Update()
    {
        if (!isRoundActive) return;

        // Update timers
        if (currentBuyTime > 0)
        {
            currentBuyTime -= Time.deltaTime;
            if (currentBuyTime <= 0)
            {
                OnBuyTimeEnd();
            }
        }

        if (currentRoundTime > 0)
        {
            currentRoundTime -= Time.deltaTime;
            if (currentRoundTime <= 0)
            {
                OnRoundTimeExpired();
            }
        }

        // Bomb timer (if planted)
        if (isBombPlanted)
        {
            float bombTimeRemaining = bombTimer - (Time.time - bombPlantTime);
            if (bombTimeRemaining <= 0)
            {
                OnBombExplode();
            }
        }

        // Check win conditions
        CheckWinConditions();
    }

    void InitializeGameMode()
    {
        currentRound = 0;
        team1Score = 0;
        team2Score = 0;

        // Find all players and assign teams
        AssignTeams();

        // Mode-specific initialization
        switch (currentGameMode)
        {
            case GameMode.GunGame:
                InitializeGunGame();
                break;
        }
    }

    void AssignTeams()
    {
        PlayerHealth[] allPlayers = FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None);
        
        team1Players.Clear();
        team2Players.Clear();

        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (i % 2 == 0)
                team1Players.Add(allPlayers[i].gameObject);
            else
                team2Players.Add(allPlayers[i].gameObject);
        }
    }

    void StartRound()
    {
        currentRound++;
        currentRoundTime = roundTime;
        currentBuyTime = buyTime;
        isRoundActive = true;
        isBombPlanted = false;

        Debug.Log($"Round {currentRound} started!");

        // Reset players
        ResetAllPlayers();

        // Notify economy system
        if (EconomySystem.Instance != null)
        {
            EconomySystem.Instance.OnNewRound();
        }

        // Mode-specific round start
        switch (currentGameMode)
        {
            case GameMode.BombDefusal:
                StartBombDefusalRound();
                break;
            case GameMode.Deathmatch:
            case GameMode.TeamDeathmatch:
                StartDeathmatchRound();
                break;
            case GameMode.GunGame:
                StartGunGameRound();
                break;
        }
    }

    void OnBuyTimeEnd()
    {
        Debug.Log("Buy time ended!");
        // Lock buy menu
    }

    void OnRoundTimeExpired()
    {
        Debug.Log("Round time expired!");
        
        // In Bomb Defusal, CT wins if time runs out and bomb not planted
        if (currentGameMode == GameMode.BombDefusal)
        {
            if (!isBombPlanted)
            {
                EndRound(team2Players, "CT wins - Time expired");
            }
        }
        else
        {
            // Other modes - check score
            CheckWinConditions();
        }
    }

    void CheckWinConditions()
    {
        switch (currentGameMode)
        {
            case GameMode.BombDefusal:
                CheckBombDefusalWin();
                break;
            case GameMode.TeamDeathmatch:
                CheckTeamDeathmatchWin();
                break;
            case GameMode.Deathmatch:
                CheckDeathmatchWin();
                break;
            case GameMode.GunGame:
                CheckGunGameWin();
                break;
        }
    }

    void CheckBombDefusalWin()
    {
        // Check if all players on one team are dead
        bool team1Alive = team1Players.Any(p => p != null && !p.GetComponent<PlayerHealth>().IsDead());
        bool team2Alive = team2Players.Any(p => p != null && !p.GetComponent<PlayerHealth>().IsDead());

        if (!team1Alive && !isBombPlanted)
        {
            EndRound(team2Players, "CT wins - Terrorists eliminated");
        }
        else if (!team2Alive && !isBombPlanted)
        {
            EndRound(team1Players, "T wins - Counter-Terrorists eliminated");
        }
        else if (!team2Alive && isBombPlanted)
        {
            // T can still win if bomb explodes
        }
    }

    void CheckTeamDeathmatchWin()
    {
        int team1Kills = 0;
        int team2Kills = 0;

        foreach (var player in team1Players)
        {
            var economy = EconomySystem.Instance?.GetPlayerStats(player);
            if (economy != null) team1Kills += economy.kills;
        }

        foreach (var player in team2Players)
        {
            var economy = EconomySystem.Instance?.GetPlayerStats(player);
            if (economy != null) team2Kills += economy.kills;
        }

        if (team1Kills >= teamKillsToWin)
        {
            EndMatch(team1Players, $"Team 1 wins {team1Kills}-{team2Kills}!");
        }
        else if (team2Kills >= teamKillsToWin)
        {
            EndMatch(team2Players, $"Team 2 wins {team2Kills}-{team1Kills}!");
        }
    }

    void CheckDeathmatchWin()
    {
        PlayerHealth[] allPlayers = FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None);
        
        foreach (var player in allPlayers)
        {
            var economy = EconomySystem.Instance?.GetPlayerStats(player.gameObject);
            if (economy != null && economy.kills >= killsToWin)
            {
                EndMatch(new GameObject[] { player.gameObject }, $"{player.gameObject.name} wins with {economy.kills} kills!");
            }
        }
    }

    void CheckGunGameWin()
    {
        foreach (var kvp in playerGunGameLevels)
        {
            if (kvp.Value >= gunGameWeapons.Length)
            {
                EndMatch(new GameObject[] { kvp.Key }, $"{kvp.Key.name} wins Gun Game!");
            }
        }
    }

    void EndRound(List<GameObject> winningTeam, string reason)
    {
        isRoundActive = false;
        Debug.Log($"Round {currentRound} ended: {reason}");

        // Update scores
        if (winningTeam == team1Players)
        {
            team1Score++;
        }
        else
        {
            team2Score++;
        }

        // Economy rewards
        if (EconomySystem.Instance != null)
        {
            bool team1Won = winningTeam == team1Players;
            EconomySystem.Instance.OnRoundEnd(team1Won, team1Players.ToArray());
            EconomySystem.Instance.OnRoundEnd(!team1Won, team2Players.ToArray());
        }

        // Check if match is won
        if (team1Score >= roundsToWin || team2Score >= roundsToWin)
        {
            EndMatch(winningTeam, $"Match ended {team1Score}-{team2Score}");
        }
        else
        {
            Invoke(nameof(StartRound), 5f); // 5 second delay before next round
        }
    }

    void EndMatch(IEnumerable<GameObject> winners, string reason)
    {
        Debug.Log($"Match ended: {reason}");
        // TODO: Show match end screen
        // TODO: Reset for new match
    }

    void ResetAllPlayers()
    {
        PlayerHealth[] allPlayers = FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None);
        foreach (var player in allPlayers)
        {
            player.Respawn();
            
            // Notify bots of round start
            BotAI botAI = player.GetComponent<BotAI>();
            if (botAI != null)
            {
                botAI.OnRoundStart();
                
                // Trigger buy phase for bots
                if (EconomySystem.Instance != null)
                {
                    int money = EconomySystem.Instance.GetPlayerMoney(player.gameObject);
                    botAI.BuyPhase(money);
                }
            }
        }
    }

    // Bomb Defusal specific
    void StartBombDefusalRound()
    {
        // Spawn players at appropriate spawn points
        // Give bomb to random T player
    }

    public void OnBombPlant(GameObject planter, Transform site)
    {
        isBombPlanted = true;
        bombPlantTime = Time.time;
        Debug.Log($"Bomb planted at {site.name}!");

        if (EconomySystem.Instance != null)
        {
            EconomySystem.Instance.OnBombPlant(planter);
        }
    }

    public void OnBombDefuse(GameObject defuser)
    {
        isBombPlanted = false;
        Debug.Log("Bomb defused!");
        
        if (EconomySystem.Instance != null)
        {
            EconomySystem.Instance.OnBombDefuse(defuser);
        }

        EndRound(team2Players, "CT wins - Bomb defused");
    }

    void OnBombExplode()
    {
        Debug.Log("Bomb exploded!");
        EndRound(team1Players, "T wins - Bomb exploded");
    }

    // Deathmatch specific
    void StartDeathmatchRound()
    {
        // Continuous spawning mode
    }

    public void OnPlayerDeath(GameObject player)
    {
        if (respawnEnabled && (currentGameMode == GameMode.Deathmatch || currentGameMode == GameMode.GunGame))
        {
            StartCoroutine(RespawnPlayerDelayed(player, respawnDelay));
        }
    }

    System.Collections.IEnumerator RespawnPlayerDelayed(GameObject player, float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnPlayer(player);
    }

    void RespawnPlayer(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Respawn();
        }
    }

    // Gun Game specific
    void InitializeGunGame()
    {
        PlayerHealth[] allPlayers = FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None);
        foreach (var player in allPlayers)
        {
            playerGunGameLevels[player.gameObject] = 0;
        }
    }

    void StartGunGameRound()
    {
        // Give each player their current weapon
        foreach (var kvp in playerGunGameLevels)
        {
            GiveWeaponForLevel(kvp.Key, kvp.Value);
        }
    }

    public void OnGunGameKill(GameObject killer)
    {
        if (playerGunGameLevels.ContainsKey(killer))
        {
            playerGunGameLevels[killer]++;
            
            if (playerGunGameLevels[killer] < gunGameWeapons.Length)
            {
                GiveWeaponForLevel(killer, playerGunGameLevels[killer]);
            }
        }
    }

    void GiveWeaponForLevel(GameObject player, int level)
    {
        if (level < gunGameWeapons.Length)
        {
            // TODO: Integrate with weapon system to give weapon
            Debug.Log($"{player.name} advanced to level {level}: {gunGameWeapons[level].weaponName}");
        }
    }

    public float GetRoundTimeRemaining() => currentRoundTime;
    public float GetBuyTimeRemaining() => currentBuyTime;
    public int GetCurrentRound() => currentRound;
    public int GetTeam1Score() => team1Score;
    public int GetTeam2Score() => team2Score;
    public bool IsBombPlanted() => isBombPlanted;
    public float GetBombTimeRemaining() => isBombPlanted ? bombTimer - (Time.time - bombPlantTime) : 0f;
}
