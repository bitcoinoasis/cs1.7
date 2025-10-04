using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple crosshair that displays in the center of the screen.
/// Attach to a Canvas with a UI Image component.
/// </summary>
public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Settings")]
    [Tooltip("Color of the crosshair")]
    public Color crosshairColor = Color.white;
    
    [Tooltip("Size of the crosshair")]
    public float crosshairSize = 20f;
    
    [Tooltip("Thickness of crosshair lines")]
    public float crosshairThickness = 2f;
    
    [Tooltip("Gap from center")]
    public float crosshairGap = 5f;
    
    [Header("Dynamic Crosshair")]
    [Tooltip("Expand crosshair when moving/shooting")]
    public bool dynamicCrosshair = true;
    
    [Tooltip("How much to expand when shooting")]
    public float shootExpansion = 5f;
    
    [Tooltip("How fast crosshair returns to normal size")]
    public float returnSpeed = 10f;
    
    private Image[] crosshairLines;
    private float currentExpansion = 0f;
    private Canvas canvas;
    
    private void Start()
    {
        // Create canvas if it doesn't exist
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("CrosshairCanvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            transform.SetParent(canvasObj.transform);
        }
        
        CreateCrosshair();
    }
    
    private void CreateCrosshair()
    {
        // Create 4 lines for crosshair (top, bottom, left, right)
        crosshairLines = new Image[4];
        
        for (int i = 0; i < 4; i++)
        {
            GameObject line = new GameObject($"CrosshairLine_{i}");
            line.transform.SetParent(transform);
            
            Image img = line.AddComponent<Image>();
            img.color = crosshairColor;
            
            RectTransform rect = line.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            
            crosshairLines[i] = img;
        }
        
        UpdateCrosshairSize();
    }
    
    private void Update()
    {
        // Expand crosshair when shooting
        if (Input.GetButton("Fire1") && dynamicCrosshair)
        {
            currentExpansion = shootExpansion;
        }
        
        // Return to normal size
        if (currentExpansion > 0)
        {
            currentExpansion = Mathf.Lerp(currentExpansion, 0, returnSpeed * Time.deltaTime);
            UpdateCrosshairSize();
        }
    }
    
    private void UpdateCrosshairSize()
    {
        float size = crosshairSize + currentExpansion;
        float gap = crosshairGap + currentExpansion;
        
        // Top line
        RectTransform topRect = crosshairLines[0].GetComponent<RectTransform>();
        topRect.sizeDelta = new Vector2(crosshairThickness, size);
        topRect.anchoredPosition = new Vector2(0, gap + size / 2);
        
        // Bottom line
        RectTransform bottomRect = crosshairLines[1].GetComponent<RectTransform>();
        bottomRect.sizeDelta = new Vector2(crosshairThickness, size);
        bottomRect.anchoredPosition = new Vector2(0, -(gap + size / 2));
        
        // Left line
        RectTransform leftRect = crosshairLines[2].GetComponent<RectTransform>();
        leftRect.sizeDelta = new Vector2(size, crosshairThickness);
        leftRect.anchoredPosition = new Vector2(-(gap + size / 2), 0);
        
        // Right line
        RectTransform rightRect = crosshairLines[3].GetComponent<RectTransform>();
        rightRect.sizeDelta = new Vector2(size, crosshairThickness);
        rightRect.anchoredPosition = new Vector2(gap + size / 2, 0);
    }
    
    /// <summary>
    /// Trigger crosshair expansion (call when weapon fires)
    /// </summary>
    public void OnShoot()
    {
        if (dynamicCrosshair)
        {
            currentExpansion = shootExpansion;
        }
    }
    
    /// <summary>
    /// Change crosshair color
    /// </summary>
    public void SetColor(Color color)
    {
        crosshairColor = color;
        if (crosshairLines != null)
        {
            foreach (Image line in crosshairLines)
            {
                if (line != null)
                    line.color = color;
            }
        }
    }
}
