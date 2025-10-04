using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// In-game scoreboard showing player stats, team scores, and round information
/// </summary>
public class ScoreboardUI : MonoBehaviour
{
    [Header("Scoreboard Panel")]
    public GameObject scoreboardPanel;
    public Transform team1Container;
    public Transform team2Container;
    
    [Header("Team Headers")]
    public TextMeshProUGUI team1NameText;
    public TextMeshProUGUI team2NameText;
    public TextMeshProUGUI team1ScoreText;
    public TextMeshProUGUI team2ScoreText;
    
    [Header("Player Row Prefab")]
    public GameObject playerRowPrefab;
    
    [Header("Match Info")]
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timeText;
    
    private Dictionary<GameObject, GameObject> playerRows = new Dictionary<GameObject, GameObject>();
    private bool isShowing = false;

    void Update()
    {
        // Toggle scoreboard with TAB
        if (Input.GetKey(KeyCode.Tab))
        {
            if (!isShowing)
            {
                ShowScoreboard();
            }
        }
        else
        {
            if (isShowing)
            {
                HideScoreboard();
            }
        }

        // Update scoreboard if showing
        if (isShowing)
        {
            UpdateScoreboard();
        }
    }

    void ShowScoreboard()
    {
        scoreboardPanel.SetActive(true);
        isShowing = true;
        UpdateScoreboard();
    }

    void HideScoreboard()
    {
        scoreboardPanel.SetActive(false);
        isShowing = false;
    }

    void UpdateScoreboard()
    {
        UpdateMatchInfo();
        UpdateTeamScores();
        UpdatePlayerRows();
    }

    void UpdateMatchInfo()
    {
        if (GameModeManager.Instance != null)
        {
            int currentRound = GameModeManager.Instance.GetCurrentRound();
            float timeRemaining = GameModeManager.Instance.GetRoundTimeRemaining();
            
            if (roundText != null)
            {
                roundText.text = $"Round {currentRound}";
            }

            if (timeText != null)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
                timeText.text = $"{minutes:00}:{seconds:00}";
            }
        }
    }

    void UpdateTeamScores()
    {
        if (GameModeManager.Instance != null)
        {
            int team1Score = GameModeManager.Instance.GetTeam1Score();
            int team2Score = GameModeManager.Instance.GetTeam2Score();
            
            if (team1ScoreText != null) team1ScoreText.text = team1Score.ToString();
            if (team2ScoreText != null) team2ScoreText.text = team2Score.ToString();
        }
    }

    void UpdatePlayerRows()
    {
        // Get all players
        PlayerHealth[] allPlayers = FindObjectsOfType<PlayerHealth>();
        
        // Separate into teams
        List<PlayerHealth> team1 = new List<PlayerHealth>();
        List<PlayerHealth> team2 = new List<PlayerHealth>();

        foreach (var player in allPlayers)
        {
            BotAI bot = player.GetComponent<BotAI>();
            if (bot != null)
            {
                if (bot.teamID == 0)
                    team1.Add(player);
                else
                    team2.Add(player);
            }
            else
            {
                // Player is on team 1 by default
                team1.Add(player);
            }
        }

        // Sort by kills (descending)
        team1 = team1.OrderByDescending(p => GetPlayerKills(p.gameObject)).ToList();
        team2 = team2.OrderByDescending(p => GetPlayerKills(p.gameObject)).ToList();

        // Update team 1
        UpdateTeamContainer(team1Container, team1);
        
        // Update team 2
        UpdateTeamContainer(team2Container, team2);
    }

    void UpdateTeamContainer(Transform container, List<PlayerHealth> players)
    {
        // Clear old rows
        foreach (Transform child in container)
        {
            if (child.gameObject != playerRowPrefab)
            {
                Destroy(child.gameObject);
            }
        }

        // Create new rows
        foreach (var player in players)
        {
            GameObject rowObj = Instantiate(playerRowPrefab, container);
            UpdatePlayerRow(rowObj, player);
        }
    }

    void UpdatePlayerRow(GameObject row, PlayerHealth player)
    {
        // Get player name
        string playerName = player.gameObject.name;
        BotAI bot = player.GetComponent<BotAI>();
        if (bot != null)
        {
            playerName = bot.botName;
        }

        // Get stats
        int kills = GetPlayerKills(player.gameObject);
        int deaths = GetPlayerDeaths(player.gameObject);
        int ping = GetPlayerPing(player.gameObject);
        int money = GetPlayerMoney(player.gameObject);

        // Get race
        string race = "None";
        PlayerRace playerRace = player.GetComponent<PlayerRace>();
        if (playerRace != null && playerRace.CurrentRace != null)
        {
            race = playerRace.CurrentRace.raceName;
        }

        // Update UI elements
        TextMeshProUGUI nameText = row.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null) nameText.text = playerName;

        TextMeshProUGUI raceText = row.transform.Find("Race")?.GetComponent<TextMeshProUGUI>();
        if (raceText != null) raceText.text = race;

        TextMeshProUGUI killsText = row.transform.Find("Kills")?.GetComponent<TextMeshProUGUI>();
        if (killsText != null) killsText.text = kills.ToString();

        TextMeshProUGUI deathsText = row.transform.Find("Deaths")?.GetComponent<TextMeshProUGUI>();
        if (deathsText != null) deathsText.text = deaths.ToString();

        TextMeshProUGUI kdText = row.transform.Find("KD")?.GetComponent<TextMeshProUGUI>();
        if (kdText != null)
        {
            float kd = deaths > 0 ? (float)kills / deaths : kills;
            kdText.text = kd.ToString("F2");
        }

        TextMeshProUGUI moneyText = row.transform.Find("Money")?.GetComponent<TextMeshProUGUI>();
        if (moneyText != null) moneyText.text = $"${money}";

        TextMeshProUGUI pingText = row.transform.Find("Ping")?.GetComponent<TextMeshProUGUI>();
        if (pingText != null) pingText.text = ping.ToString();

        // Highlight dead players
        if (player.IsDead)
        {
            Image background = row.GetComponent<Image>();
            if (background != null)
            {
                background.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }
    }

    int GetPlayerKills(GameObject player)
    {
        if (EconomySystem.Instance != null)
        {
            var stats = EconomySystem.Instance.GetPlayerStats(player);
            if (stats != null) return stats.kills;
        }
        return 0;
    }

    int GetPlayerDeaths(GameObject player)
    {
        if (EconomySystem.Instance != null)
        {
            var stats = EconomySystem.Instance.GetPlayerStats(player);
            if (stats != null) return stats.deaths;
        }
        return 0;
    }

    int GetPlayerMoney(GameObject player)
    {
        if (EconomySystem.Instance != null)
        {
            return EconomySystem.Instance.GetPlayerMoney(player);
        }
        return 0;
    }

    int GetPlayerPing(GameObject player)
    {
        // TODO: Implement actual ping calculation
        return Random.Range(10, 100);
    }
}
