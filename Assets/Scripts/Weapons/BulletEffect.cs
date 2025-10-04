using UnityEngine;
using System.Collections;

/// <summary>
/// Creates visual effects for bullets including tracers and impact effects.
/// </summary>
public class BulletEffect : MonoBehaviour
{
    [Header("Tracer Settings")]
    [Tooltip("Color of the bullet tracer line")]
    public Color tracerColor = Color.yellow;
    
    [Tooltip("Width of the tracer line")]
    public float tracerWidth = 0.05f;
    
    [Tooltip("How long the tracer line stays visible")]
    public float tracerDuration = 0.1f;
    
    [Header("Impact Settings")]
    [Tooltip("Size of impact flash")]
    public float impactSize = 0.2f;
    
    [Tooltip("Color of impact effect")]
    public Color impactColor = Color.red;
    
    [Tooltip("How long impact effect stays visible")]
    public float impactDuration = 0.15f;
    
    /// <summary>
    /// Create a bullet tracer from start to end point
    /// </summary>
    public static void CreateTracer(Vector3 startPos, Vector3 endPos, Color color, float width = 0.05f, float duration = 0.1f)
    {
        GameObject tracerObj = new GameObject("BulletTracer");
        LineRenderer line = tracerObj.AddComponent<LineRenderer>();
        
        // Configure line renderer
        line.startWidth = width;
        line.endWidth = width;
        line.positionCount = 2;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
        
        // Set material and color
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = color;
        line.endColor = color;
        
        // Destroy after duration
        Destroy(tracerObj, duration);
    }
    
    /// <summary>
    /// Create impact effect at hit point
    /// </summary>
    public static void CreateImpact(Vector3 position, Vector3 normal, Color color, float size = 0.2f, float duration = 0.15f)
    {
        // Create impact flash sphere
        GameObject impactObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        impactObj.name = "BulletImpact";
        impactObj.transform.position = position;
        impactObj.transform.localScale = Vector3.one * size;
        
        // Remove collider (it's just visual)
        Destroy(impactObj.GetComponent<Collider>());
        
        // Set color
        Renderer rend = impactObj.GetComponent<Renderer>();
        rend.material.color = color;
        
        // Make it emissive
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", color * 2f);
        
        // Destroy after duration
        Destroy(impactObj, duration);
        
        // Create bullet hole decal (optional - simple version)
        CreateBulletHole(position, normal, duration * 2f);
    }
    
    /// <summary>
    /// Create a simple bullet hole decal
    /// </summary>
    private static void CreateBulletHole(Vector3 position, Vector3 normal, float duration)
    {
        GameObject hole = GameObject.CreatePrimitive(PrimitiveType.Quad);
        hole.name = "BulletHole";
        
        // Position slightly above surface to prevent z-fighting
        hole.transform.position = position + normal * 0.01f;
        hole.transform.rotation = Quaternion.LookRotation(normal);
        hole.transform.localScale = Vector3.one * 0.1f;
        
        // Remove collider
        Destroy(hole.GetComponent<Collider>());
        
        // Set dark color
        Renderer rend = hole.GetComponent<Renderer>();
        rend.material.color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        
        // Destroy after a while
        Destroy(hole, duration);
    }
    
    /// <summary>
    /// Create muzzle flash effect at weapon position
    /// </summary>
    public static GameObject CreateMuzzleFlash(Transform weaponTransform, Color color, float size = 0.3f)
    {
        GameObject flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        flash.name = "MuzzleFlash";
        flash.transform.SetParent(weaponTransform);
        flash.transform.localPosition = Vector3.forward * 0.5f; // In front of weapon
        flash.transform.localScale = Vector3.one * size;
        
        // Remove collider
        Destroy(flash.GetComponent<Collider>());
        
        // Set bright color
        Renderer rend = flash.GetComponent<Renderer>();
        rend.material.color = color;
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", color * 3f);
        
        flash.SetActive(false);
        return flash;
    }
    
    /// <summary>
    /// Show muzzle flash briefly
    /// </summary>
    public static IEnumerator FlashMuzzle(GameObject muzzleFlash, float duration = 0.05f)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(duration);
            muzzleFlash.SetActive(false);
        }
    }
}
