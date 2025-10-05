using UnityEngine;

public enum HitboxType
{
    Head,       // 4x damage (headshot)
    Body,       // 1x damage (normal)
    Arms,       // 0.75x damage
    Legs        // 0.75x damage
}

/// <summary>
/// Component that identifies different body parts for damage calculation.
/// Attach to child objects of Bot or Player to create hitboxes.
/// </summary>
public class Hitbox : MonoBehaviour
{
    [Header("Hitbox Settings")]
    [Tooltip("Type of hitbox - determines damage multiplier")]
    public HitboxType hitboxType = HitboxType.Body;
    
    [Header("Damage Multipliers")]
    [Tooltip("Head shots deal 4x damage")]
    public float headMultiplier = 4f;
    
    [Tooltip("Body shots deal normal damage")]
    public float bodyMultiplier = 1f;
    
    [Tooltip("Arm shots deal reduced damage")]
    public float armMultiplier = 0.75f;
    
    [Tooltip("Leg shots deal reduced damage")]
    public float legMultiplier = 0.75f;
    
    [Header("References")]
    [Tooltip("Reference to parent Bot (auto-assigned)")]
    private Bot parentBot;
    
    [Tooltip("Reference to parent PlayerHealth (auto-assigned)")]
    private PlayerHealth parentPlayer;
    
    private void Awake()
    {
        // Try to find parent Bot
        parentBot = GetComponentInParent<Bot>();
        
        // If not a bot, try to find parent PlayerHealth
        if (parentBot == null)
        {
            parentPlayer = GetComponentInParent<PlayerHealth>();
        }
    }
    
    /// <summary>
    /// Get the damage multiplier based on hitbox type
    /// </summary>
    public float GetDamageMultiplier()
    {
        switch (hitboxType)
        {
            case HitboxType.Head:
                return headMultiplier;
            case HitboxType.Body:
                return bodyMultiplier;
            case HitboxType.Arms:
                return armMultiplier;
            case HitboxType.Legs:
                return legMultiplier;
            default:
                return bodyMultiplier;
        }
    }
    
    /// <summary>
    /// Apply damage through the hitbox to parent entity
    /// </summary>
    public void TakeDamage(float baseDamage)
    {
        float finalDamage = baseDamage * GetDamageMultiplier();
        
        // Apply to bot if this is a bot hitbox
        if (parentBot != null)
        {
            parentBot.TakeDamage(finalDamage, null);
            
            // Debug feedback
            if (hitboxType == HitboxType.Head)
            {
                Debug.Log($"HEADSHOT! Damage: {finalDamage}");
            }
        }
        // Apply to player if this is a player hitbox
        else if (parentPlayer != null)
        {
            parentPlayer.TakeDamage((int)finalDamage);
            
            if (hitboxType == HitboxType.Head)
            {
                Debug.Log($"Player took HEADSHOT! Damage: {finalDamage}");
            }
        }
    }
    
    /// <summary>
    /// Get parent bot (if this is a bot hitbox)
    /// </summary>
    public Bot GetBot()
    {
        return parentBot;
    }
    
    /// <summary>
    /// Get parent player (if this is a player hitbox)
    /// </summary>
    public PlayerHealth GetPlayer()
    {
        return parentPlayer;
    }
    
    /// <summary>
    /// Visual debug - show hitbox type in editor
    /// </summary>
    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.white;
        
        switch (hitboxType)
        {
            case HitboxType.Head:
                gizmoColor = Color.red;
                break;
            case HitboxType.Body:
                gizmoColor = Color.yellow;
                break;
            case HitboxType.Arms:
                gizmoColor = Color.blue;
                break;
            case HitboxType.Legs:
                gizmoColor = Color.green;
                break;
        }
        
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
