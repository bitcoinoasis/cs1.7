using UnityEngine;

/// <summary>
/// Simple bot spawner for testing - spawns bots at designated spawn points
/// </summary>
public class BotSpawner : MonoBehaviour
{
    [Header("Bot Settings")]
    public GameObject botPrefab;
    public int team1BotCount = 4;
    public int team2BotCount = 4;
    
    [Header("Spawn Points")]
    public Transform[] team1SpawnPoints;
    public Transform[] team2SpawnPoints;
    
    [Header("Bot Difficulty")]
    public BotAI.BotDifficulty defaultDifficulty = BotAI.BotDifficulty.Medium;
    public bool randomizeDifficulty = true;
    
    [Header("Auto Spawn")]
    public bool spawnOnStart = true;
    public bool spawnOnRoundStart = false;

    private int botCounter = 1;

    void Start()
    {
        if (spawnOnStart)
        {
            SpawnAllBots();
        }

        // Subscribe to round start if needed
        if (spawnOnRoundStart)
        {
            // TODO: Subscribe to GameModeManager round start event
        }
    }

    [ContextMenu("Spawn All Bots")]
    public void SpawnAllBots()
    {
        SpawnTeam1Bots();
        SpawnTeam2Bots();
    }

    void SpawnTeam1Bots()
    {
        if (team1SpawnPoints.Length == 0)
        {
            Debug.LogError("No Team 1 spawn points assigned!");
            return;
        }

        for (int i = 0; i < team1BotCount; i++)
        {
            Transform spawnPoint = team1SpawnPoints[i % team1SpawnPoints.Length];
            SpawnBot(spawnPoint, 0, $"T Bot {botCounter}");
            botCounter++;
        }
    }

    void SpawnTeam2Bots()
    {
        if (team2SpawnPoints.Length == 0)
        {
            Debug.LogError("No Team 2 spawn points assigned!");
            return;
        }

        for (int i = 0; i < team2BotCount; i++)
        {
            Transform spawnPoint = team2SpawnPoints[i % team2SpawnPoints.Length];
            SpawnBot(spawnPoint, 1, $"CT Bot {botCounter}");
            botCounter++;
        }
    }

    GameObject SpawnBot(Transform spawnPoint, int teamID, string botName)
    {
        if (botPrefab == null)
        {
            Debug.LogError("Bot prefab not assigned!");
            return null;
        }

        // Instantiate bot
        GameObject bot = Instantiate(botPrefab, spawnPoint.position, spawnPoint.rotation);
        bot.name = botName;

        // Configure BotAI
        BotAI botAI = bot.GetComponent<BotAI>();
        if (botAI != null)
        {
            botAI.teamID = teamID;
            botAI.botName = botName;
            
            // Set difficulty
            if (randomizeDifficulty)
            {
                botAI.difficulty = (BotAI.BotDifficulty)Random.Range(0, 4);
            }
            else
            {
                botAI.difficulty = defaultDifficulty;
            }
        }
        else
        {
            Debug.LogWarning($"Bot {botName} missing BotAI component!");
        }

        // Register with economy system
        if (EconomySystem.Instance != null)
        {
            EconomySystem.Instance.RegisterPlayer(bot);
        }
        else
        {
            Debug.LogWarning("EconomySystem not found in scene!");
        }

        // Assign random race
        PlayerRace playerRace = bot.GetComponent<PlayerRace>();
        if (playerRace != null)
        {
            AssignRandomRace(playerRace);
        }

        Debug.Log($"Spawned {botName} on Team {teamID} with difficulty {botAI?.difficulty}");
        return bot;
    }

    void AssignRandomRace(PlayerRace playerRace)
    {
        // Find all race data assets
        RaceData[] allRaces = Resources.LoadAll<RaceData>("Data/Races");
        
        if (allRaces.Length > 0)
        {
            RaceData randomRace = allRaces[Random.Range(0, allRaces.Length)];
            playerRace.SetRace(randomRace);
        }
    }

    [ContextMenu("Clear All Bots")]
    public void ClearAllBots()
    {
        BotAI[] bots = FindObjectsByType<BotAI>(FindObjectsSortMode.None);
        foreach (var bot in bots)
        {
            Destroy(bot.gameObject);
        }
        botCounter = 1;
        Debug.Log($"Cleared {bots.Length} bots");
    }

    private void OnDrawGizmos()
    {
        // Draw spawn points
        if (team1SpawnPoints != null)
        {
            Gizmos.color = Color.red;
            foreach (var spawn in team1SpawnPoints)
            {
                if (spawn != null)
                {
                    Gizmos.DrawWireSphere(spawn.position, 1f);
                    Gizmos.DrawLine(spawn.position, spawn.position + spawn.forward * 2f);
                }
            }
        }

        if (team2SpawnPoints != null)
        {
            Gizmos.color = Color.blue;
            foreach (var spawn in team2SpawnPoints)
            {
                if (spawn != null)
                {
                    Gizmos.DrawWireSphere(spawn.position, 1f);
                    Gizmos.DrawLine(spawn.position, spawn.position + spawn.forward * 2f);
                }
            }
        }
    }
}
