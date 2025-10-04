using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public int armor = 0;
    public int maxArmor = 100;
    
    [Header("Events")]
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent<int> OnArmorChanged;
    public UnityEvent OnPlayerDeath;
    
    // Ability hooks (delegates and events cannot have Header attribute)
    public delegate void DamageModifier(ref float damage);
    public event DamageModifier onBeforeTakeDamage;
    public System.Action<float, GameObject> onTakeDamageFrom; // damage, attacker
    
    private bool isDead = false;
    private float lastDamageTime = 0f;
    
    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
        OnArmorChanged?.Invoke(armor);
    }
    
    public void TakeDamage(int damage, GameObject attacker = null)
    {
        if (isDead) return;
        
        float damageFloat = damage;
        
        // Call ability hooks for damage modification (Evasion, Devotion Aura, etc.)
        onBeforeTakeDamage?.Invoke(ref damageFloat);
        
        // Track damage time for invisibility
        lastDamageTime = Time.time;
        
        // Convert back to int
        int finalDamage = Mathf.RoundToInt(damageFloat);
        
        // Calculate damage reduction from armor
        int damageToArmor = Mathf.Min(finalDamage / 2, armor);
        int damageToHealth = finalDamage - damageToArmor;
        
        armor -= damageToArmor;
        currentHealth -= damageToHealth;
        
        OnHealthChanged?.Invoke(currentHealth);
        OnArmorChanged?.Invoke(armor);
        
        // Notify ability system
        onTakeDamageFrom?.Invoke(finalDamage, attacker);
        
        if (currentHealth <= 0)
        {
            Die(attacker);
        }
    }
    
    public void Heal(int amount)
    {
        if (isDead) return;
        
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }
    
    public bool HasTakenDamageRecently(float timeWindow)
    {
        return (Time.time - lastDamageTime) < timeWindow;
    }
    
    public void AddArmor(int amount)
    {
        armor = Mathf.Min(armor + amount, maxArmor);
        OnArmorChanged?.Invoke(armor);
    }
    
    private void Die(GameObject killer)
    {
        isDead = true;
        OnPlayerDeath?.Invoke();
        
        // Notify the killer for XP gain
        if (killer != null)
        {
            PlayerExperience killerXP = killer.GetComponent<PlayerExperience>();
            if (killerXP != null)
            {
                killerXP.AddExperience(100); // Base XP for kill
            }
        }
        
        // Respawn after delay
        Invoke(nameof(Respawn), 3f);
    }
    
    private void Respawn()
    {
        isDead = false;
        currentHealth = maxHealth;
        armor = 0;
        
        OnHealthChanged?.Invoke(currentHealth);
        OnArmorChanged?.Invoke(armor);
        
        // Find spawn point
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;
        }
    }
    
    public bool IsDead()
    {
        return isDead;
    }
}
