using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using CS17.Core;

public enum Team
{
    None,
    Terrorist,
    CounterTerrorist
}

public enum RoundState
{
    Waiting,
    BuyTime,
    Active,
    RoundEnd
}

namespace CS17.Core
{
    /// <summary>
    /// Main game manager - handles game state, rounds, and player management
    /// Uses EventSystem for communication and ServiceLocator for dependencies
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [Header("Demo Mode")]
        public bool demoMode = true;
        public int demoModeUnlimitedMoney = 16000;
        
        [Header("Round Settings")]
        public float buyTime = 15f;
        public float roundTime = 120f;
        public float roundEndTime = 5f;
        public int roundsToWin = 16;
        
        [Header("Money Settings")]
        public int startMoney = 800;
        public int killReward = 300;
        public int winReward = 3500;
        public int loseReward = 1400;
        
        [Header("Current State")]
        public RoundState currentState = RoundState.Waiting;
        public int currentRound = 0;
        public int terroristScore = 0;
        public int counterTerroristScore = 0;
        public float roundTimer = 0f;
        
        [Header("Events")]
        public UnityEvent<Team> OnRoundWin;
        public UnityEvent<RoundState> OnRoundStateChanged;
        public UnityEvent<float> OnRoundTimerUpdate;
        
        private List<PlayerInfo> players = new List<PlayerInfo>();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                // Register with ServiceLocator
                Services.Register(this);
                
                Debug.Log("[GameManager] Initialized and registered with ServiceLocator");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            // Initialize game
            Initialize();
            
            if (!demoMode)
            {
                StartNewRound();
            }
            else
            {
                // In demo mode, start in active state with no timers
                SetState(RoundState.Active);
            }
        }
        
        private void Initialize()
        {
            // Publish game started event
            EventSystem.Instance.Publish(GameEvents.GAME_STARTED);
        }
        
        private void SetState(RoundState newState)
        {
            currentState = newState;
            OnRoundStateChanged?.Invoke(currentState);
            
            // Publish state change event
            switch (newState)
            {
                case RoundState.BuyTime:
                    EventSystem.Instance.Publish(GameEvents.BUY_TIME_STARTED);
                    break;
                case RoundState.Active:
                    EventSystem.Instance.Publish(GameEvents.BUY_TIME_ENDED);
                    break;
                case RoundState.RoundEnd:
                    EventSystem.Instance.Publish(GameEvents.ROUND_ENDED);
                    break;
            }
        }
    
    private void Update()
    {
        if (!demoMode)
        {
            UpdateRoundTimer();
        }
    }
    
    private void UpdateRoundTimer()
    {
        if (currentState == RoundState.BuyTime)
        {
            roundTimer -= Time.deltaTime;
            if (roundTimer <= 0)
            {
                StartActivePhase();
            }
        }
        else if (currentState == RoundState.Active)
        {
            roundTimer -= Time.deltaTime;
            if (roundTimer <= 0)
            {
                // CT wins if time runs out
                EndRound(Team.CounterTerrorist);
            }
        }
        else if (currentState == RoundState.RoundEnd)
        {
            roundTimer -= Time.deltaTime;
            if (roundTimer <= 0)
            {
                StartNewRound();
            }
        }
        
        OnRoundTimerUpdate?.Invoke(roundTimer);
    }
    
    public void StartNewRound()
    {
        currentRound++;
        currentState = RoundState.BuyTime;
        roundTimer = buyTime;
        
        OnRoundStateChanged?.Invoke(currentState);
        
        // Respawn all players
        RespawnAllPlayers();
    }
    
    private void StartActivePhase()
    {
        currentState = RoundState.Active;
        roundTimer = roundTime;
        OnRoundStateChanged?.Invoke(currentState);
    }
    
    public void EndRound(Team winningTeam)
    {
        currentState = RoundState.RoundEnd;
        roundTimer = roundEndTime;
        
        // Update scores
        if (winningTeam == Team.Terrorist)
            terroristScore++;
        else if (winningTeam == Team.CounterTerrorist)
            counterTerroristScore++;
        
        OnRoundWin?.Invoke(winningTeam);
        OnRoundStateChanged?.Invoke(currentState);
        
        // Give money rewards
        GiveMoneyRewards(winningTeam);
        
        // Check for match end
        if (terroristScore >= roundsToWin || counterTerroristScore >= roundsToWin)
        {
            EndMatch();
        }
    }
    
    private void GiveMoneyRewards(Team winningTeam)
    {
        foreach (var player in players)
        {
            if (player.team == winningTeam)
                player.money += winReward;
            else
                player.money += loseReward;
        }
    }
    
    private void EndMatch()
    {
        Debug.Log($"Match Over! T: {terroristScore} - CT: {counterTerroristScore}");
        // Load end screen or restart
    }
    
    private void RespawnAllPlayers()
    {
        GameObject[] tSpawns = GameObject.FindGameObjectsWithTag("TSpawn");
        GameObject[] ctSpawns = GameObject.FindGameObjectsWithTag("CTSpawn");
        
        foreach (var player in players)
        {
            if (player.playerObject != null)
            {
                Transform spawnPoint = null;
                
                if (player.team == Team.Terrorist && tSpawns.Length > 0)
                    spawnPoint = tSpawns[Random.Range(0, tSpawns.Length)].transform;
                else if (player.team == Team.CounterTerrorist && ctSpawns.Length > 0)
                    spawnPoint = ctSpawns[Random.Range(0, ctSpawns.Length)].transform;
                
                if (spawnPoint != null)
                {
                    player.playerObject.transform.position = spawnPoint.position;
                    player.playerObject.transform.rotation = spawnPoint.rotation;
                }
                
                // Reset health
                PlayerHealth health = player.playerObject.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.currentHealth = health.maxHealth;
                }
            }
        }
    }
    
    public void RegisterPlayer(GameObject player, Team team)
    {
        int initialMoney = demoMode ? demoModeUnlimitedMoney : startMoney;
        
        PlayerInfo info = new PlayerInfo
        {
            playerObject = player,
            team = team,
            money = initialMoney
        };
        players.Add(info);
    }
    
    public void OnPlayerKilled(GameObject victim, GameObject killer)
    {
        // Give XP and money to killer
        if (killer != null)
        {
            PlayerInfo killerInfo = players.Find(p => p.playerObject == killer);
            if (killerInfo != null)
            {
                killerInfo.money += killReward;
            }
        }
        
        // Check if all players on one team are dead
        CheckRoundEnd();
    }
    
    private void CheckRoundEnd()
    {
        // Don't end rounds in demo mode
        if (demoMode) return;
        
        bool anyTerroristsAlive = false;
        bool anyCTsAlive = false;
        
        foreach (var player in players)
        {
            if (player.playerObject != null)
            {
                PlayerHealth health = player.playerObject.GetComponent<PlayerHealth>();
                if (health != null && !health.IsDead())
                {
                    if (player.team == Team.Terrorist)
                        anyTerroristsAlive = true;
                    else if (player.team == Team.CounterTerrorist)
                        anyCTsAlive = true;
                }
            }
        }
        
        // End round if one team is eliminated
        if (!anyTerroristsAlive && anyCTsAlive)
            EndRound(Team.CounterTerrorist);
        else if (!anyCTsAlive && anyTerroristsAlive)
            EndRound(Team.Terrorist);
    }
    
    public int GetPlayerMoney(GameObject player)
    {
        if (demoMode) return demoModeUnlimitedMoney; // Always return max money in demo mode
        
        PlayerInfo info = players.Find(p => p.playerObject == player);
        return info != null ? info.money : 0;
    }
    
    public bool SpendMoney(GameObject player, int amount)
    {
        if (demoMode) return true; // Always allow purchases in demo mode
        
        PlayerInfo info = players.Find(p => p.playerObject == player);
        if (info != null && info.money >= amount)
        {
            info.money -= amount;
            return true;
        }
        return false;
    }
    
    public void AddMoney(GameObject player, int amount)
    {
        PlayerInfo info = players.Find(p => p.playerObject == player);
        if (info != null)
        {
            info.money += amount;
        }
    }
    }
}

[System.Serializable]
public class PlayerInfo
{
    public GameObject playerObject;
    public Team team;
    public int money;
    public int kills;
    public int deaths;
}
