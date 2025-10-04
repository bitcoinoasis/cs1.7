using UnityEngine;
using System.Collections.Generic;

public class BotSpawner : MonoBehaviour
{
    [Header("Bot Prefab")]
    public GameObject botPrefab;
    
    [Header("Spawn Settings")]
    public int numberOfBots = 10;
    public bool spawnOnStart = true;
    public float spawnRadius = 10f;
    public float botSpacing = 2f;
    
    [Header("Spawn Patterns")]
    public SpawnPattern spawnPattern = SpawnPattern.Circle;
    public Transform[] customSpawnPoints;
    
    [Header("Bot Configuration")]
    public int botHealth = 100;
    public bool botsRespawn = true;
    
    private List<GameObject> spawnedBots = new List<GameObject>();
    
    public enum SpawnPattern
    {
        Circle,
        Line,
        Grid,
        Random,
        CustomPoints
    }
    
    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnBots();
        }
    }
    
    private void Update()
    {
        // Quick spawn controls
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SpawnBots();
        }
        
        if (Input.GetKeyDown(KeyCode.F6))
        {
            ClearBots();
        }
    }
    
    public void SpawnBots()
    {
        ClearBots();
        
        switch (spawnPattern)
        {
            case SpawnPattern.Circle:
                SpawnInCircle();
                break;
            case SpawnPattern.Line:
                SpawnInLine();
                break;
            case SpawnPattern.Grid:
                SpawnInGrid();
                break;
            case SpawnPattern.Random:
                SpawnRandom();
                break;
            case SpawnPattern.CustomPoints:
                SpawnAtCustomPoints();
                break;
        }
    }
    
    private void SpawnInCircle()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfBots;
            Vector3 position = transform.position + new Vector3(
                Mathf.Cos(angle) * spawnRadius,
                0,
                Mathf.Sin(angle) * spawnRadius
            );
            
            SpawnBot(position, Quaternion.LookRotation(transform.position - position));
        }
    }
    
    private void SpawnInLine()
    {
        Vector3 startPos = transform.position - transform.forward * (numberOfBots * botSpacing / 2f);
        
        for (int i = 0; i < numberOfBots; i++)
        {
            Vector3 position = startPos + transform.forward * (i * botSpacing);
            SpawnBot(position, Quaternion.LookRotation(-transform.forward));
        }
    }
    
    private void SpawnInGrid()
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(numberOfBots));
        int cols = Mathf.CeilToInt((float)numberOfBots / rows);
        
        Vector3 startPos = transform.position - new Vector3(cols * botSpacing / 2f, 0, rows * botSpacing / 2f);
        
        int botCount = 0;
        for (int row = 0; row < rows && botCount < numberOfBots; row++)
        {
            for (int col = 0; col < cols && botCount < numberOfBots; col++)
            {
                Vector3 position = startPos + new Vector3(col * botSpacing, 0, row * botSpacing);
                SpawnBot(position, Quaternion.LookRotation(-transform.forward));
                botCount++;
            }
        }
    }
    
    private void SpawnRandom()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );
            
            Vector3 position = transform.position + randomOffset;
            SpawnBot(position, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
    
    private void SpawnAtCustomPoints()
    {
        if (customSpawnPoints == null || customSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No custom spawn points assigned!");
            return;
        }
        
        for (int i = 0; i < Mathf.Min(numberOfBots, customSpawnPoints.Length); i++)
        {
            if (customSpawnPoints[i] != null)
            {
                SpawnBot(customSpawnPoints[i].position, customSpawnPoints[i].rotation);
            }
        }
    }
    
    private void SpawnBot(Vector3 position, Quaternion rotation)
    {
        if (botPrefab == null)
        {
            Debug.LogError("Bot prefab is not assigned!");
            return;
        }
        
        GameObject bot = Instantiate(botPrefab, position, rotation);
        Bot botComponent = bot.GetComponent<Bot>();
        
        if (botComponent != null)
        {
            botComponent.maxHealth = botHealth;
            botComponent.health = botHealth;
            botComponent.respawnOnDeath = botsRespawn;
        }
        
        spawnedBots.Add(bot);
    }
    
    public void ClearBots()
    {
        foreach (GameObject bot in spawnedBots)
        {
            if (bot != null)
            {
                Destroy(bot);
            }
        }
        spawnedBots.Clear();
    }
    
    public void SpawnAllBots()
    {
        SpawnBots();
    }
    
    public void DespawnAllBots()
    {
        ClearBots();
    }
    
    private void OnDrawGizmos()
    {
        // Draw spawn area visualization
        Gizmos.color = Color.cyan;
        
        switch (spawnPattern)
        {
            case SpawnPattern.Circle:
                DrawCircle(transform.position, spawnRadius, 32);
                break;
            case SpawnPattern.Random:
                Gizmos.DrawWireCube(transform.position, new Vector3(spawnRadius * 2, 1, spawnRadius * 2));
                break;
        }
    }
    
    private void DrawCircle(Vector3 center, float radius, int segments)
    {
        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * Mathf.PI * 2f / segments;
            float angle2 = (i + 1) * Mathf.PI * 2f / segments;
            
            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1) * radius, 0, Mathf.Sin(angle1) * radius);
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2) * radius, 0, Mathf.Sin(angle2) * radius);
            
            Gizmos.DrawLine(point1, point2);
        }
    }
}
