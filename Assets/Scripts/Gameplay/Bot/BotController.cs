using UnityEngine;

public class Bot : MonoBehaviour
{
    [Header("Bot Settings")]
    public int health = 100;
    public int maxHealth = 100;
    public bool respawnOnDeath = true;
    public float respawnDelay = 3f;
    
    [Header("Visual Feedback")]
    public Color normalColor = Color.red;
    public Color hitColor = Color.yellow;
    public float hitFlashDuration = 0.1f;
    
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private Renderer botRenderer;
    private bool isDead = false;
    
    private void Start()
    {
        // Store spawn position
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        
        // Get renderer for visual feedback
        botRenderer = GetComponent<Renderer>();
        if (botRenderer != null)
        {
            botRenderer.material.color = normalColor;
        }
        
        health = maxHealth;
    }
    
    public void TakeDamage(float damage, GameObject attacker = null)
    {
        if (isDead) return;
        
        health -= Mathf.RoundToInt(damage);
        
        // Visual feedback
        if (botRenderer != null)
        {
            botRenderer.material.color = hitColor;
            Invoke(nameof(ResetColor), hitFlashDuration);
        }
        
        // Show damage number (optional)
        Debug.Log($"Bot took {damage} damage! Health: {health}/{maxHealth}");
        
        if (health <= 0)
        {
            Die(attacker);
        }
    }
    
    private void ResetColor()
    {
        if (botRenderer != null && !isDead)
        {
            botRenderer.material.color = normalColor;
        }
    }
    
    private void Die(GameObject killer)
    {
        isDead = true;
        
        // Give XP to killer
        if (killer != null)
        {
            PlayerExperience killerXP = killer.GetComponent<PlayerExperience>();
            if (killerXP != null)
            {
                killerXP.AddExperience(100);
            }
        }
        
        // Respawn or destroy
        if (respawnOnDeath)
        {
            // Hide the bot
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), respawnDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Respawn()
    {
        health = maxHealth;
        isDead = false;
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        gameObject.SetActive(true);
        
        if (botRenderer != null)
        {
            botRenderer.material.color = normalColor;
        }
    }
    
    public bool IsDead()
    {
        return isDead;
    }
}
