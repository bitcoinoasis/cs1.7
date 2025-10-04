using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    public List<GameObject> weaponPrefabs = new List<GameObject>();
    public Transform weaponHolder;
    
    private List<GameObject> weapons = new List<GameObject>();
    private int currentWeaponIndex = 0;
    private Weapon currentWeapon;
    
    private void Start()
    {
        // Instantiate all weapons
        foreach (GameObject weaponPrefab in weaponPrefabs)
        {
            GameObject weapon = Instantiate(weaponPrefab, weaponHolder);
            weapon.SetActive(false);
            weapons.Add(weapon);
        }
        
        // Equip first weapon
        if (weapons.Count > 0)
        {
            EquipWeapon(0);
        }
    }
    
    private void Update()
    {
        // Weapon switching with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchWeapon(3);
        
        // Scroll wheel weapon switching
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            SwitchWeapon(currentWeaponIndex - 1);
        else if (scroll < 0f)
            SwitchWeapon(currentWeaponIndex + 1);
    }
    
    private void SwitchWeapon(int index)
    {
        if (weapons.Count == 0) return;
        
        // Wrap around
        if (index < 0)
            index = weapons.Count - 1;
        else if (index >= weapons.Count)
            index = 0;
        
        if (index == currentWeaponIndex) return;
        
        EquipWeapon(index);
    }
    
    private void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count) return;
        
        // Disable current weapon
        if (currentWeapon != null)
        {
            weapons[currentWeaponIndex].SetActive(false);
        }
        
        // Enable new weapon
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].SetActive(true);
        currentWeapon = weapons[currentWeaponIndex].GetComponent<Weapon>();
    }
    
    public void AddWeapon(GameObject weaponPrefab)
    {
        GameObject weapon = Instantiate(weaponPrefab, weaponHolder);
        weapon.SetActive(false);
        weapons.Add(weapon);
    }
    
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
