using UnityEngine;
using System.Collections;

/// <summary>
/// Human Ability 2: Invisibility
/// Active - become invisible for a short duration
/// </summary>
public class InvisibilityAbility : Ability
{
    private Renderer[] renderers;
    private bool isInvisible = false;
    
    protected override void OnInitialize()
    {
        // Get all renderers on player
        renderers = GetComponentsInChildren<Renderer>();
        Debug.Log($"[Invisibility] Initialized. Duration: {GetValue()}s, Visibility: {GetSecondaryValue()}%");
    }
    
    protected override void Activate()
    {
        if (isInvisible)
        {
            Debug.Log("[Invisibility] Already invisible!");
            return;
        }
        
        float duration = GetValue(); // 2s, 3s, 4s, 5s, 6s
        float visibility = GetSecondaryValue() / 100f; // 40%, 30%, 20%, 15%, 10%
        
        StartCoroutine(InvisibilityCoroutine(duration, visibility));
        
        Debug.Log($"<color=cyan>INVISIBILITY! Duration: {duration}s, Visibility: {visibility * 100f}%</color>");
    }
    
    IEnumerator InvisibilityCoroutine(float duration, float visibility)
    {
        isInvisible = true;
        
        // Make player semi-transparent
        SetPlayerAlpha(visibility);
        
        // Visual effect
        PlayVisualEffect();
        
        // Wait for duration
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            // Check if player shoots or takes damage (breaks invisibility)
            Weapon weapon = GetComponentInChildren<Weapon>();
            if (weapon != null && weapon.HasShotRecently(0.1f))
            {
                Debug.Log("[Invisibility] Broken by shooting!");
                break;
            }
            
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.HasTakenDamageRecently(0.1f))
            {
                Debug.Log("[Invisibility] Broken by taking damage!");
                break;
            }
            
            yield return null;
        }
        
        // Restore visibility
        SetPlayerAlpha(1f);
        isInvisible = false;
        
        Debug.Log("[Invisibility] Ended");
    }
    
    void SetPlayerAlpha(float alpha)
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer == null) continue;
            
            foreach (Material mat in renderer.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                    
                    // Set rendering mode to transparent if needed
                    if (alpha < 1f)
                    {
                        mat.SetFloat("_Mode", 3); // Transparent
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 0);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.EnableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = 3000;
                    }
                    else
                    {
                        mat.SetFloat("_Mode", 0); // Opaque
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                        mat.SetInt("_ZWrite", 1);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.DisableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = -1;
                    }
                }
            }
        }
    }
}
