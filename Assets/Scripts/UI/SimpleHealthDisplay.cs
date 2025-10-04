using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple health display that creates its own UI elements
/// </summary>
public class SimpleHealthDisplay : MonoBehaviour
{
    private Text healthText;
    private PlayerHealth playerHealth;

    void Start()
    {
        // Create Text element
        GameObject textObj = new GameObject("HealthText");
        textObj.transform.SetParent(transform);
        
        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
        
        healthText = textObj.AddComponent<Text>();
        healthText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        healthText.fontSize = 24;
        healthText.color = Color.white;
        healthText.alignment = TextAnchor.MiddleCenter;
        
        // Find player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = $"HP: {Mathf.RoundToInt(playerHealth.currentHealth)}/{Mathf.RoundToInt(playerHealth.maxHealth)}";
        }
    }
}
