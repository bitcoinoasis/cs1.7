using UnityEngine;
using System.Collections;

/// <summary>
/// Orc Ability 2: Bash
/// Passive - chance to stun enemies on hit
/// </summary>
public class BashAbility : Ability
{
    private Weapon weapon;
    private System.Collections.Generic.Dictionary<GameObject, float> stunCooldowns = new System.Collections.Generic.Dictionary<GameObject, float>();
    
    protected override void OnInitialize()
    {
        weapon = GetComponentInChildren<Weapon>();
        
        if (weapon != null)
        {
            weapon.onHitEnemy += TryBash;
            Debug.Log($"[Bash] Initialized. Chance: {GetValue()}%, Duration: {GetSecondaryValue()}s");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    void TryBash(GameObject enemy, float damage)
    {
        if (enemy == null) return;
        
        float bashChance = GetValue(); // 5%, 8%, 10%, 12%, 15%
        float stunDuration = GetSecondaryValue(); // 0.3s, 0.4s, 0.5s, 0.6s, 0.75s
        
        // Check if enemy is on cooldown (can't stun same target twice in 3 seconds)
        if (stunCooldowns.ContainsKey(enemy) && Time.time < stunCooldowns[enemy])
        {
            return;
        }
        
        // Roll for bash
        if (Random.Range(0f, 100f) < bashChance)
        {
            // Apply stun
            ApplyStun(enemy, stunDuration);
            
            // Add cooldown for this enemy
            if (stunCooldowns.ContainsKey(enemy))
                stunCooldowns[enemy] = Time.time + 3f;
            else
                stunCooldowns.Add(enemy, Time.time + 3f);
            
            PlayVisualEffect(enemy.transform.position);
            Debug.Log($"<color=yellow>BASH! Stunned {enemy.name} for {stunDuration}s</color>");
        }
    }
    
    void ApplyStun(GameObject target, float duration)
    {
        // Stun freezes movement but not shooting
        PlayerController controller = target.GetComponent<PlayerController>();
        if (controller != null)
        {
            StartCoroutine(StunCoroutine(controller, duration));
        }
        
        // TODO: Add screen shake effect for stunned player
    }
    
    IEnumerator StunCoroutine(PlayerController controller, float duration)
    {
        float originalWalkSpeed = controller.walkSpeed;
        float originalSprintSpeed = controller.sprintSpeed;
        controller.walkSpeed = 0f; // Freeze movement
        controller.sprintSpeed = 0f;
        
        yield return new WaitForSeconds(duration);
        
        controller.walkSpeed = originalWalkSpeed; // Restore movement
        controller.sprintSpeed = originalSprintSpeed;
    }
    
    void OnDestroy()
    {
        if (weapon != null)
        {
            weapon.onHitEnemy -= TryBash;
        }
    }
}
