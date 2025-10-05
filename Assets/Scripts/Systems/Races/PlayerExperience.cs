using UnityEngine;
using UnityEngine.Events;

public class PlayerExperience : MonoBehaviour
{
    [Header("Experience")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int skillPoints = 0;
    
    [Header("Level Progression")]
    public int[] xpPerLevel = new int[] { 
        0, 100, 250, 450, 700, 1000, 1350, 1750, 2200, 2700, 3250 
    };
    
    [Header("Events")]
    public UnityEvent<int, int, int> OnXPChanged; // current XP, XP for next level, current level
    public UnityEvent<int> OnLevelUp;
    public UnityEvent<int> OnSkillPointsChanged;
    
    private PlayerRace playerRace;
    
    private void Start()
    {
        playerRace = GetComponent<PlayerRace>();
        OnXPChanged?.Invoke(currentXP, GetXPForNextLevel(), currentLevel);
        OnSkillPointsChanged?.Invoke(skillPoints);
    }
    
    public void AddExperience(int amount)
    {
        currentXP += amount;
        
        // Check for level up
        while (currentLevel < GetMaxLevel() && currentXP >= GetXPForNextLevel())
        {
            LevelUp();
        }
        
        OnXPChanged?.Invoke(currentXP, GetXPForNextLevel(), currentLevel);
    }
    
    private void LevelUp()
    {
        currentLevel++;
        skillPoints++;
        
        OnLevelUp?.Invoke(currentLevel);
        OnSkillPointsChanged?.Invoke(skillPoints);
        
        Debug.Log($"Level Up! Now level {currentLevel}");
    }
    
    public int GetXPForNextLevel()
    {
        if (currentLevel >= GetMaxLevel())
            return xpPerLevel[xpPerLevel.Length - 1];
        
        return xpPerLevel[currentLevel];
    }
    
    public int GetMaxLevel()
    {
        if (playerRace != null && playerRace.GetCurrentRace() != null)
            return playerRace.GetCurrentRace().maxLevel;
        
        return xpPerLevel.Length - 1;
    }
    
    public bool CanAffordSkillPoint()
    {
        return skillPoints > 0;
    }
    
    public void SpendSkillPoint()
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            OnSkillPointsChanged?.Invoke(skillPoints);
        }
    }
    
    public float GetLevelProgress()
    {
        if (currentLevel <= 0 || currentLevel >= GetMaxLevel())
            return 1f;
        
        int xpForCurrentLevel = currentLevel > 0 ? xpPerLevel[currentLevel - 1] : 0;
        int xpForNextLevel = xpPerLevel[currentLevel];
        int xpIntoLevel = currentXP - xpForCurrentLevel;
        int xpNeeded = xpForNextLevel - xpForCurrentLevel;
        
        return (float)xpIntoLevel / xpNeeded;
    }
}
