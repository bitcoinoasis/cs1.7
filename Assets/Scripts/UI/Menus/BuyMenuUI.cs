using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Buy menu UI for purchasing weapons, equipment, and armor
/// </summary>
public class BuyMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject buyMenuPanel;
    public GameObject pistolsPanel;
    public GameObject smgsPanel;
    public GameObject riflesPanel;
    public GameObject snipersPanel;
    public GameObject shotgunsPanel;
    public GameObject equipmentPanel;
    
    [Header("Category Buttons")]
    public Button pistolsButton;
    public Button smgsButton;
    public Button riflesButton;
    public Button snipersButton;
    public Button shotgunsButton;
    public Button equipmentButton;
    
    [Header("Money Display")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI buyTimeText;
    
    [Header("Weapon Button Prefab")]
    public GameObject weaponButtonPrefab;
    
    [Header("Weapon Data")]
    public WeaponData[] pistols;
    public WeaponData[] smgs;
    public WeaponData[] rifles;
    public WeaponData[] snipers;
    public WeaponData[] shotguns;
    
    [Header("Equipment Prices")]
    public int armorPrice = 650;
    public int helmetPrice = 350;
    public int fullArmorPrice = 1000;
    public int defuseKitPrice = 400;
    public int grenadePrice = 300;
    public int flashbangPrice = 200;
    public int smokePrice = 300;

    private GameObject player;
    private Dictionary<GameObject, List<Button>> categoryButtons = new Dictionary<GameObject, List<Button>>();
    private GameObject currentPanel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Setup category button listeners
        pistolsButton.onClick.AddListener(() => ShowCategory(pistolsPanel));
        smgsButton.onClick.AddListener(() => ShowCategory(smgsPanel));
        riflesButton.onClick.AddListener(() => ShowCategory(riflesPanel));
        snipersButton.onClick.AddListener(() => ShowCategory(snipersPanel));
        shotgunsButton.onClick.AddListener(() => ShowCategory(shotgunsPanel));
        equipmentButton.onClick.AddListener(() => ShowCategory(equipmentPanel));
        
        // Populate weapon categories
        PopulateWeaponCategory(pistolsPanel, pistols);
        PopulateWeaponCategory(smgsPanel, smgs);
        PopulateWeaponCategory(riflesPanel, rifles);
        PopulateWeaponCategory(snipersPanel, snipers);
        PopulateWeaponCategory(shotgunsPanel, shotguns);
        PopulateEquipmentPanel();
        
        // Start with pistols
        ShowCategory(pistolsPanel);
        
        // Hide by default
        buyMenuPanel.SetActive(false);
    }

    void Update()
    {
        // Toggle buy menu with 'B' key
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuyMenu();
        }

        // Update money display
        if (buyMenuPanel.activeSelf)
        {
            UpdateMoneyDisplay();
            UpdateBuyTimeDisplay();
        }
    }

    void PopulateWeaponCategory(GameObject panel, WeaponData[] weapons)
    {
        if (panel == null) return;

        // Find container (assume it's the first child)
        Transform container = panel.transform.GetChild(0);
        List<Button> buttons = new List<Button>();

        foreach (WeaponData weapon in weapons)
        {
            GameObject buttonObj = Instantiate(weaponButtonPrefab, container);
            
            // Set weapon name
            TextMeshProUGUI nameText = buttonObj.transform.Find("WeaponName")?.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = weapon.weaponName;
            }

            // Set price
            TextMeshProUGUI priceText = buttonObj.transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
            if (priceText != null)
            {
                priceText.text = $"${weapon.price}";
            }

            // Set icon
            Image iconImage = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
            if (iconImage != null && weapon.weaponIcon != null)
            {
                iconImage.sprite = weapon.weaponIcon;
            }

            // Set stats preview
            TextMeshProUGUI statsText = buttonObj.transform.Find("Stats")?.GetComponent<TextMeshProUGUI>();
            if (statsText != null)
            {
                statsText.text = $"DMG: {weapon.damage} | {weapon.magazineSize}/{weapon.reserveAmmo}";
            }

            // Add purchase button
            Button buyButton = buttonObj.GetComponent<Button>();
            WeaponData weaponRef = weapon; // Capture for lambda
            buyButton.onClick.AddListener(() => PurchaseWeapon(weaponRef));
            
            buttons.Add(buyButton);
        }

        categoryButtons[panel] = buttons;
    }

    void PopulateEquipmentPanel()
    {
        if (equipmentPanel == null) return;

        Transform container = equipmentPanel.transform.GetChild(0);

        // Armor button
        CreateEquipmentButton(container, "Kevlar Vest", armorPrice, () => PurchaseArmor(false));
        
        // Full armor button
        CreateEquipmentButton(container, "Kevlar + Helmet", fullArmorPrice, () => PurchaseArmor(true));
        
        // Defuse kit button (CT only)
        CreateEquipmentButton(container, "Defuse Kit", defuseKitPrice, PurchaseDefuseKit);
        
        // Grenades
        CreateEquipmentButton(container, "HE Grenade", grenadePrice, () => PurchaseGrenade("HE"));
        CreateEquipmentButton(container, "Flashbang", flashbangPrice, () => PurchaseGrenade("Flash"));
        CreateEquipmentButton(container, "Smoke Grenade", smokePrice, () => PurchaseGrenade("Smoke"));
    }

    void CreateEquipmentButton(Transform container, string itemName, int price, System.Action onPurchase)
    {
        GameObject buttonObj = Instantiate(weaponButtonPrefab, container);
        
        TextMeshProUGUI nameText = buttonObj.transform.Find("WeaponName")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null) nameText.text = itemName;

        TextMeshProUGUI priceText = buttonObj.transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
        if (priceText != null) priceText.text = $"${price}";

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => onPurchase());
    }

    void ShowCategory(GameObject panel)
    {
        // Hide all panels
        pistolsPanel.SetActive(false);
        smgsPanel.SetActive(false);
        riflesPanel.SetActive(false);
        snipersPanel.SetActive(false);
        shotgunsPanel.SetActive(false);
        equipmentPanel.SetActive(false);

        // Show selected panel
        panel.SetActive(true);
        currentPanel = panel;
    }

    void PurchaseWeapon(WeaponData weapon)
    {
        if (player == null || EconomySystem.Instance == null) return;

        int playerMoney = EconomySystem.Instance.GetPlayerMoney(player);
        
        if (playerMoney >= weapon.price)
        {
            if (EconomySystem.Instance.BuyWeapon(player, weapon))
            {
                // TODO: Actually give weapon to player
                Debug.Log($"Purchased {weapon.weaponName} for ${weapon.price}");
                
                // Play purchase sound
                PlayPurchaseSound();
                
                // Update UI
                UpdateMoneyDisplay();
            }
        }
        else
        {
            Debug.Log($"Not enough money! Need ${weapon.price}, have ${playerMoney}");
            PlayErrorSound();
        }
    }

    void PurchaseArmor(bool includeHelmet)
    {
        if (player == null || EconomySystem.Instance == null) return;

        int price = includeHelmet ? fullArmorPrice : armorPrice;
        
        if (includeHelmet)
        {
            if (EconomySystem.Instance.BuyFullArmor(player, price))
            {
                Debug.Log("Purchased Kevlar + Helmet");
                PlayPurchaseSound();
            }
            else
            {
                PlayErrorSound();
            }
        }
        else
        {
            if (EconomySystem.Instance.BuyArmor(player, price))
            {
                Debug.Log("Purchased Kevlar Vest");
                PlayPurchaseSound();
            }
            else
            {
                PlayErrorSound();
            }
        }
    }

    void PurchaseDefuseKit()
    {
        if (player == null || EconomySystem.Instance == null) return;

        if (EconomySystem.Instance.SpendMoney(player, defuseKitPrice))
        {
            Debug.Log("Purchased Defuse Kit");
            PlayPurchaseSound();
            // TODO: Give defuse kit to player
        }
        else
        {
            PlayErrorSound();
        }
    }

    void PurchaseGrenade(string grenadeType)
    {
        if (player == null || EconomySystem.Instance == null) return;

        int price = grenadeType == "HE" ? grenadePrice : grenadeType == "Flash" ? flashbangPrice : smokePrice;

        if (EconomySystem.Instance.SpendMoney(player, price))
        {
            Debug.Log($"Purchased {grenadeType} Grenade");
            PlayPurchaseSound();
            // TODO: Give grenade to player
        }
        else
        {
            PlayErrorSound();
        }
    }

    void UpdateMoneyDisplay()
    {
        if (moneyText != null && player != null && EconomySystem.Instance != null)
        {
            int money = EconomySystem.Instance.GetPlayerMoney(player);
            moneyText.text = $"${money}";
        }
    }

    void UpdateBuyTimeDisplay()
    {
        if (buyTimeText != null && GameModeManager.Instance != null)
        {
            float buyTime = GameModeManager.Instance.GetBuyTimeRemaining();
            
            if (buyTime > 0)
            {
                buyTimeText.text = $"Buy Time: {buyTime:F1}s";
                buyTimeText.color = Color.green;
            }
            else
            {
                buyTimeText.text = "Buy Time Expired";
                buyTimeText.color = Color.red;
            }
        }
    }

    public void ToggleBuyMenu()
    {
        if (GameModeManager.Instance != null)
        {
            float buyTime = GameModeManager.Instance.GetBuyTimeRemaining();
            
            if (buyTime <= 0)
            {
                Debug.Log("Buy time expired!");
                return;
            }
        }

        buyMenuPanel.SetActive(!buyMenuPanel.activeSelf);
        
        // Lock/unlock cursor
        if (buyMenuPanel.activeSelf)
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

    public void CloseBuyMenu()
    {
        buyMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void PlayPurchaseSound()
    {
        // TODO: Play purchase sound effect
    }

    void PlayErrorSound()
    {
        // TODO: Play error sound effect
    }
}
