using UnityEngine;

/// <summary>
/// Night Elf Ultimate: Evasion
/// Passive - chance to completely avoid damage
/// </summary>
public class EvasionAbility : Ability
{
    private PlayerHealth playerHealth;
    
    protected override void OnInitialize()
    {
        playerHealth = GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.onBeforeTakeDamage += TryEvade;
            Debug.Log($"[Evasion] Initialized. Dodge chance: {GetValue()}%");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    void TryEvade(ref float damage)
    {
        float dodgeChance = GetValue(); // 10%, 13%, 16%, 19%, 22%
        
        if (Random.Range(0f, 100f) < dodgeChance)
        {
            float originalDamage = damage;
            damage = 0; // Completely avoid damage
            
            PlayVisualEffect();
            Debug.Log($"<color=cyan>EVADED! Avoided {originalDamage:F1} damage</color>");
        }
    }
    
    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.onBeforeTakeDamage -= TryEvade;
        }
    }
}

/// <summary>
/// Night Elf Ability 2: Blink
/// Active - instantly dash forward, avoiding all damage
/// </summary>
public class BlinkAbility : Ability
{
    protected override void OnInitialize()
    {
        Debug.Log($"[Blink] Initialized. Distance: {GetValue()}m");
    }
    
    protected override void Activate()
    {
        float distance = GetValue(); // 8m, 9m, 10m, 11m, 12m
        
        Vector3 direction = transform.forward;
        Vector3 destination = transform.position + direction * distance;
        
        // Check for walls
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            destination = hit.point - direction * 0.5f;
        }
        
        // Visual effect
        PlayVisualEffect(transform.position);
        
        // Dash
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = destination;
            controller.enabled = true;
        }
        else
        {
            transform.position = destination;
        }
        
        PlayVisualEffect(destination);
        
        Debug.Log($"<color=cyan>BLINK! Dashed {distance}m forward</color>");
    }
}

/// <summary>
/// Night Elf Ability 3: Thorns Aura
/// Passive - reflect a portion of damage taken back to attacker
/// </summary>
public class ThornsAuraAbility : Ability
{
    private PlayerHealth playerHealth;
    
    protected override void OnInitialize()
    {
        playerHealth = GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.onTakeDamageFrom += ReflectDamage;
            Debug.Log($"[Thorns Aura] Initialized. Reflect: {GetValue()}%");
        }
    }
    
    protected override void Activate()
    {
        // Passive ability
    }
    
    void ReflectDamage(float damageTaken, GameObject attacker)
    {
        if (attacker == null) return;
        
        float reflectPercent = GetValue() / 100f; // 10%, 15%, 20%, 25%, 30%
        float reflectedDamage = damageTaken * reflectPercent;
        
        PlayerHealth attackerHealth = attacker.GetComponent<PlayerHealth>();
        if (attackerHealth != null)
        {
            attackerHealth.TakeDamage((int)reflectedDamage);
            
            PlayVisualEffect();
            Debug.Log($"[Thorns Aura] Reflected {reflectedDamage:F1} damage to {attacker.name}");
        }
    }
    
    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.onTakeDamageFrom -= ReflectDamage;
        }
    }
}
