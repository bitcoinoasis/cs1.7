using UnityEngine;
using UnityEngine.UI;
using CS17.Core;

public class BuyMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject buyMenuPanel;
    public Text moneyText;
    public Transform weaponListContainer;
    public GameObject weaponButtonPrefab;
    
    [Header("Weapon Shop")]
    public WeaponShopItem[] weaponsForSale;
    
    private GameObject player;
    private WeaponManager weaponManager;
    private bool isMenuOpen = false;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            weaponManager = player.GetComponent<WeaponManager>();
        }
        
        CreateWeaponButtons();
        HideMenu();
    }
    
    private void Update()
    {
        // In demo mode, always allow buying. Otherwise, only during buy time
        bool canBuy = GameManager.Instance != null && 
                     (GameManager.Instance.demoMode || GameManager.Instance.currentState == RoundState.BuyTime);
        
        if (canBuy)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleMenu();
            }
        }
        else
        {
            if (isMenuOpen)
                HideMenu();
        }
        
        // Update money display
        if (moneyText != null && GameManager.Instance != null && player != null)
        {
            int money = GameManager.Instance.GetPlayerMoney(player);
            moneyText.text = "$" + money;
        }
    }
    
    private void CreateWeaponButtons()
    {
        // Safety checks
        if (weaponButtonPrefab == null || weaponListContainer == null)
        {
            Debug.LogWarning("BuyMenu: Weapon button prefab or container not assigned!");
            return;
        }
        
        if (weaponsForSale == null || weaponsForSale.Length == 0)
        {
            Debug.LogWarning("BuyMenu: No weapons for sale!");
            return;
        }
        
        foreach (WeaponShopItem item in weaponsForSale)
        {
            if (item.weaponPrefab == null)
            {
                Debug.LogWarning("BuyMenu: Weapon prefab is null in shop item!");
                continue;
            }
            
            GameObject buttonObj = Instantiate(weaponButtonPrefab, weaponListContainer);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            
            if (button == null)
            {
                Debug.LogError("BuyMenu: Weapon button prefab doesn't have Button component!");
                Destroy(buttonObj);
                continue;
            }
            
            if (buttonText != null)
            {
                buttonText.text = $"{item.weaponPrefab.name} - ${item.price}";
            }
            
            button.onClick.AddListener(() => BuyWeapon(item));
        }
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
                }
                
                Debug.Log($"Purchased {item.weaponPrefab.name}");
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }
    
    private void ToggleMenu()
    {
        if (isMenuOpen)
            HideMenu();
        else
            ShowMenu();
    }
    
    private void ShowMenu()
    {
        if (buyMenuPanel != null)
        {
            buyMenuPanel.SetActive(true);
            isMenuOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    private void HideMenu()
    {
        if (buyMenuPanel != null)
        {
            buyMenuPanel.SetActive(false);
            isMenuOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

[System.Serializable]
public class WeaponShopItem
{
    public GameObject weaponPrefab;
    public int price;
}
