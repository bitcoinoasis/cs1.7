using UnityEngine;

public enum WeaponType
{
    Pistol,
    SMG,
    Rifle,
    Sniper,
    Shotgun,
    Melee
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic Info")]
    public string weaponName;
    public WeaponType weaponType;
    public Sprite weaponIcon;
    
    [Header("Damage Stats")]
    public float damage = 30f;
    public float fireRate = 0.1f; // Seconds between shots
    public float headshotMultiplier = 4f;
    public float legMultiplier = 0.75f;
    [Range(0f, 1f)]
    public float armorPenetration = 0.5f; // 0-1 (percentage)
    
    [Header("Ammo")]
    public int magazineSize = 30;
    public int reserveAmmo = 90;
    public float reloadTime = 2.5f;
    
    [Header("Accuracy")]
    [Tooltip("Lower = more accurate")]
    public float baseAccuracy = 1.0f;
    public float recoilAmount = 2f;
    public float range = 1000f;
    
    [Header("Movement")]
    [Tooltip("1.0 = 250 units/sec (knife speed)")]
    public float movementSpeedMultiplier = 0.9f;
    
    [Header("Economy")]
    public int price = 2500;
    public int killReward = 300;
    public int bonusReward = 0; // Extra for shotgun kills, etc.
    
    [Header("Special Features")]
    public bool isAutomatic = false;
    public bool isSilenced = false;
    public bool hasScope = false;
    public float scopeZoom = 2f; // 2x or 4x
    public bool isBurstMode = false;
    public int burstCount = 3;
    
    [Header("Shotgun Specific")]
    public int pelletsPerShot = 1; // 9 for Nova, 6 for XM1014
    
    [Header("Visual/Audio")]
    public GameObject weaponPrefab;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public GameObject muzzleFlashPrefab;
    
    [Header("Recoil")]
    public float recoilSpeed = 10f;
    public float recoilReturnSpeed = 5f;
}
