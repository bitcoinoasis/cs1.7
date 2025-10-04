using UnityEngine;
using System.Collections;

/// <summary>
/// Orc Ability 3: Reincarnation
/// Passive trigger - respawn at death location with partial HP (once per round)
/// </summary>
public class ReincarnationAbility : Ability
{
    private PlayerHealth playerHealth;
    private bool hasUsedThisRound = false;
    
    protected override void OnInitialize()
    {
        playerHealth = GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath.AddListener(() => TryReincarnate(false));
            Debug.Log($"[Reincarnation] Initialized. Respawn HP: {GetValue()}%");
        }
    }
    
    protected override void Activate()
    {
        // Passive trigger ability
    }
    
    void TryReincarnate(bool wasHeadshot)
    {
        // Check if already used this round
        if (hasUsedThisRound)
        {
            Debug.Log("[Reincarnation] Already used this round");
            return;
        }
        
        // Check if killed by headshot (cannot reincarnate)
        if (wasHeadshot)
        {
            Debug.Log("[Reincarnation] Cannot reincarnate from headshot!");
            return;
        }
        
        // Check cooldown
        if (!IsReady)
        {
            Debug.Log($"[Reincarnation] On cooldown: {CooldownRemaining:F1}s");
            return;
        }
        
        // Start reincarnation
        StartCoroutine(ReincarnateCoroutine());
    }
    
    IEnumerator ReincarnateCoroutine()
    {
        hasUsedThisRound = true;
        
        float respawnHPPercent = GetValue(); // 25%, 35%, 45%, 55%, 65%
        float cooldown = GetCooldown();
        
        Debug.Log($"<color=green>REINCARNATION! Reviving in 2 seconds with {respawnHPPercent}% HP...</color>");
        
        // Store death position
        Vector3 deathPosition = transform.position;
        
        // 2-second delay (vulnerable)
        yield return new WaitForSeconds(2f);
        
        // Respawn
        if (playerHealth != null)
        {
            int respawnHP = Mathf.RoundToInt(playerHealth.maxHealth * (respawnHPPercent / 100f));
            playerHealth.currentHealth = respawnHP;
            
            // Restore position
            transform.position = deathPosition;
            
            // Reduce ammo to 50%
            Weapon weapon = GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                weapon.CurrentAmmo = weapon.CurrentAmmo / 2;
                weapon.ReserveAmmo = weapon.ReserveAmmo / 2;
            }
            
            // Visual effect
            PlayVisualEffect();
            
            // Start cooldown
            currentCooldown = cooldown;
            
            Debug.Log($"<color=green>REINCARNATED! HP: {respawnHP}/{playerHealth.maxHealth}</color>");
        }
    }
    
    // Call this at the start of each round
    public void ResetRound()
    {
        hasUsedThisRound = false;
    }
    
    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath.RemoveListener(() => TryReincarnate(false));
        }
    }
}
