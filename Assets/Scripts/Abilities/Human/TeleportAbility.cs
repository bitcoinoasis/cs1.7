using UnityEngine;

/// <summary>
/// Human Ability 3: Teleport
/// Active - teleport forward in the direction you're facing
/// </summary>
public class TeleportAbility : Ability
{
    private CharacterController characterController;
    
    protected override void OnInitialize()
    {
        characterController = GetComponent<CharacterController>();
        Debug.Log($"[Teleport] Initialized. Distance: {GetValue()}m");
    }
    
    protected override void Activate()
    {
        float distance = GetValue(); // 5m, 7m, 9m, 11m, 13m
        
        // Calculate teleport destination
        Vector3 direction = transform.forward;
        Vector3 startPosition = transform.position + Vector3.up * 0.5f; // Start from chest height
        Vector3 destination = transform.position + direction * distance;
        
        // Check if path is clear (raycast)
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, distance))
        {
            // Hit a wall, stop before it
            destination = hit.point - direction * 0.5f;
            Debug.Log($"[Teleport] Hit obstacle, teleporting to: {hit.point}");
        }
        
        // Visual effect at start position
        PlayVisualEffect(transform.position);
        
        // Teleport
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = destination;
            characterController.enabled = true;
        }
        else
        {
            transform.position = destination;
        }
        
        // Visual effect at end position
        PlayVisualEffect(destination);
        
        Debug.Log($"<color=cyan>TELEPORT! Distance: {Vector3.Distance(startPosition, destination):F1}m</color>");
        
        // TODO: Add 0.3s vulnerability after teleport
    }
}
