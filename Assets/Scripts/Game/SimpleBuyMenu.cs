using UnityEngine;

/// <summary>
/// Simple on-screen buy menu using Unity's OnGUI (no Canvas needed!)
/// Press B to open, click to buy weapons
/// </summary>
public class SimpleBuyMenu : MonoBehaviour
{
    [Header("Weapon Shop")]
    [Tooltip("List of weapons available for purchase")]
    public WeaponShopItem[] weaponsForSale;
    
    [Header("Settings")]
    [Tooltip("Only allow buying in Demo Mode or Buy Time")]
    public bool requireBuyTime = false;
    
    private bool isMenuOpen = false;
    private GameObject player;
    private WeaponManager weaponManager;
    private Rect menuRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400);
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            weaponManager = player.GetComponent<WeaponManager>();
        }
    }
    
    private void Update()
    {
        // Check if we can buy
        bool canBuy = true;
        if (requireBuyTime && GameManager.Instance != null)
        {
            canBuy = GameManager.Instance.demoMode || GameManager.Instance.currentState == RoundState.BuyTime;
        }
        
        // Toggle menu with B key
        if (canBuy && Input.GetKeyDown(KeyCode.B))
        {
            isMenuOpen = !isMenuOpen;
            
            if (isMenuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
        // Close menu with ESC
        if (isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    private void OnGUI()
    {
        if (!isMenuOpen) return;
        
        // Style setup
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 16;
        
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 14;
        buttonStyle.padding = new RectOffset(10, 10, 10, 10);
        
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 18;
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.alignment = TextAnchor.MiddleCenter;
        
        // Main menu box
        GUI.Box(menuRect, "", boxStyle);
        
        GUILayout.BeginArea(menuRect);
        GUILayout.Space(10);
        
        // Title
        GUILayout.Label("=== WEAPON SHOP ===", labelStyle);
        GUILayout.Space(10);
        
        // Money display
        int money = 0;
        if (GameManager.Instance != null && player != null)
        {
            money = GameManager.Instance.GetPlayerMoney(player);
        }
        GUILayout.Label($"Money: ${money}", labelStyle);
        GUILayout.Space(10);
        
        // Weapon list
        if (weaponsForSale != null && weaponsForSale.Length > 0)
        {
            foreach (WeaponShopItem item in weaponsForSale)
            {
                if (item.weaponPrefab == null) continue;
                
                // Get weapon name
                string weaponName = item.weaponPrefab.name;
                Weapon weaponComponent = item.weaponPrefab.GetComponent<Weapon>();
                if (weaponComponent != null && weaponComponent.weaponData != null)
                {
                    weaponName = weaponComponent.weaponData.weaponName;
                }
                
                // Check if can afford
                bool canAfford = money >= item.price;
                GUI.enabled = canAfford;
                
                // Button color based on affordability
                Color originalColor = GUI.backgroundColor;
                if (canAfford)
                    GUI.backgroundColor = Color.green;
                else
                    GUI.backgroundColor = Color.red;
                
                if (GUILayout.Button($"{weaponName} - ${item.price}", buttonStyle, GUILayout.Height(40)))
                {
                    BuyWeapon(item);
                }
                
                GUI.backgroundColor = originalColor;
                GUI.enabled = true;
                
                GUILayout.Space(5);
            }
        }
        else
        {
            GUILayout.Label("No weapons available!", labelStyle);
        }
        
        GUILayout.Space(10);
        
        // Close button
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Close (ESC)", buttonStyle, GUILayout.Height(40)))
        {
            isMenuOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        GUILayout.EndArea();
    }
    
    private void BuyWeapon(WeaponShopItem item)
    {
        if (GameManager.Instance != null && player != null)
        {
            if (GameManager.Instance.SpendMoney(player, item.price))
            {
                // Give weapon to player
                if (weaponManager != null)
                {
                    weaponManager.AddWeapon(item.weaponPrefab);
                    Debug.Log($"✓ Purchased {item.weaponPrefab.name}!");
                }
            }
            else
            {
                Debug.Log("✗ Not enough money!");
            }
        }
    }
}
