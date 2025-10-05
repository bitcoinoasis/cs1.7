using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Kill feed system - displays recent kills, headshots, and multi-kills
/// </summary>
public class KillFeedUI : MonoBehaviour
{
    [Header("Kill Feed Settings")]
    public Transform killFeedContainer;
    public GameObject killFeedEntryPrefab;
    public int maxEntries = 5;
    public float entryDuration = 5f;
    
    [Header("Icons")]
    public Sprite headshotIcon;
    public Sprite knifeIcon;
    public Sprite grenadeIcon;
    
    private Queue<KillFeedEntry> activeEntries = new Queue<KillFeedEntry>();

    /// <summary>
    /// Add a kill to the feed
    /// </summary>
    public void AddKill(string killerName, string victimName, string weaponName, bool isHeadshot = false)
    {
        // Create new entry
        GameObject entryObj = Instantiate(killFeedEntryPrefab, killFeedContainer);
        
        // Setup entry UI
        TextMeshProUGUI killerText = entryObj.transform.Find("Killer")?.GetComponent<TextMeshProUGUI>();
        if (killerText != null) killerText.text = killerName;

        TextMeshProUGUI victimText = entryObj.transform.Find("Victim")?.GetComponent<TextMeshProUGUI>();
        if (victimText != null) victimText.text = victimName;

        TextMeshProUGUI weaponText = entryObj.transform.Find("Weapon")?.GetComponent<TextMeshProUGUI>();
        if (weaponText != null) weaponText.text = weaponName;

        // Headshot icon
        Image headshotImage = entryObj.transform.Find("HeadshotIcon")?.GetComponent<Image>();
        if (headshotImage != null)
        {
            headshotImage.gameObject.SetActive(isHeadshot);
            if (isHeadshot && headshotIcon != null)
            {
                headshotImage.sprite = headshotIcon;
            }
        }

        // Add to queue
        KillFeedEntry entry = new KillFeedEntry
        {
            entryObject = entryObj,
            timeCreated = Time.time
        };
        activeEntries.Enqueue(entry);

        // Remove old entries
        while (activeEntries.Count > maxEntries)
        {
            KillFeedEntry oldEntry = activeEntries.Dequeue();
            Destroy(oldEntry.entryObject);
        }
    }

    void Update()
    {
        // Remove expired entries
        while (activeEntries.Count > 0)
        {
            KillFeedEntry entry = activeEntries.Peek();
            if (Time.time - entry.timeCreated > entryDuration)
            {
                activeEntries.Dequeue();
                Destroy(entry.entryObject);
            }
            else
            {
                break; // Queue is ordered by time, so we can stop here
            }
        }
    }

    private class KillFeedEntry
    {
        public GameObject entryObject;
        public float timeCreated;
    }
}
