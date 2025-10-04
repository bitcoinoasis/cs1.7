using UnityEngine;
using UnityEngine.Events;

public class PlayerRace : MonoBehaviour
{
    [Header("Current Race")]
    public RaceData currentRace;
    
    [Header("Events")]
    public UnityEvent<RaceData> OnRaceChanged;
    
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private float baseWalkSpeed;
    private int baseMaxHealth;
    private float healthRegenTimer = 0f;
    
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        
        if (playerController != null)
            baseWalkSpeed = playerController.walkSpeed;
        
        if (playerHealth != null)
            baseMaxHealth = playerHealth.maxHealth;
        
        // Apply default race if set
        if (currentRace != null)
        {
            ApplyRaceStats();
        }
    }
    
    private void Update()
    {
        // Handle health regeneration
        if (currentRace != null && currentRace.healthRegenPerSecond > 0)
        {
            healthRegenTimer += Time.deltaTime;
            if (healthRegenTimer >= 1f)
            {
                playerHealth.Heal((int)currentRace.healthRegenPerSecond);
                healthRegenTimer = 0f;
            }
        }
    }
    
    public void SetRace(RaceData newRace)
    {
        currentRace = newRace;
        ApplyRaceStats();
        OnRaceChanged?.Invoke(newRace);
    }
    
    private void ApplyRaceStats()
    {
        if (currentRace == null) return;
        
        // Apply health multiplier
        if (playerHealth != null)
        {
            playerHealth.maxHealth = Mathf.RoundToInt(baseMaxHealth * currentRace.healthMultiplier);
            playerHealth.currentHealth = playerHealth.maxHealth;
            playerHealth.AddArmor((int)currentRace.armorBonus);
        }
        
        // Apply speed multiplier
        if (playerController != null)
        {
            playerController.walkSpeed = baseWalkSpeed * currentRace.speedMultiplier;
            playerController.sprintSpeed = (baseWalkSpeed * 1.4f) * currentRace.speedMultiplier;
        }
    }
    
    public float GetDamageMultiplier()
    {
        return currentRace != null ? currentRace.damageMultiplier : 1f;
    }
    
    public RaceData GetCurrentRace()
    {
        return currentRace;
    }
}
