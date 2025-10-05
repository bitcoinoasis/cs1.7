using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// AI controller for bots - handles movement, combat, weapon selection, and difficulty settings
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerController))]
public class BotAI : MonoBehaviour
{
    [Header("Bot Settings")]
    public BotDifficulty difficulty = BotDifficulty.Medium;
    public string botName = "Bot";
    public int teamID = 0;

    [Header("Combat Settings")]
    public float detectionRange = 50f;
    public float shootingRange = 100f;
    public float optimalRange = 20f;
    public float reactionTimeMin = 0.1f;
    public float reactionTimeMax = 0.5f;
    
    [Header("Accuracy Settings")]
    [Range(0f, 1f)] public float baseAccuracy = 0.7f;
    public float aimSmoothing = 5f;
    public float aimOffset = 0.5f;
    
    [Header("Movement Settings")]
    public float stoppingDistance = 15f;
    public float retreatHealthThreshold = 30f;
    public float strafeSpeed = 3f;
    
    [Header("Weapon Preferences")]
    public List<string> preferredWeapons = new List<string> { "AK-47", "M4A4", "AWP", "Desert Eagle" };
    public float weaponSwitchDelay = 1f;

    // Components
    private NavMeshAgent navAgent;
    private PlayerHealth health;
    private PlayerController controller;
    private Weapon currentWeapon;
    private Transform currentTarget;
    
    // State
    private float reactionTime;
    private float lastWeaponSwitch;
    private float lastShotTime;
    private Vector3 lastKnownTargetPos;
    private bool isReloading;
    private bool isRetreating;
    private float strafeDirection = 1f;
    private float nextStrafeChange;

    // Buy phase
    private bool hasBoughtThisRound;
    
    public enum BotDifficulty
    {
        Easy,
        Medium,
        Hard,
        Expert
    }

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<PlayerController>();
        
        SetDifficultySettings();
        InitializeBot();
    }

    void Update()
    {
        if (health.IsDead()) return;

        // Find and track targets
        FindTarget();
        
        if (currentTarget != null)
        {
            EngageTarget();
        }
        else
        {
            Patrol();
        }

        // Handle strafing during combat
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) < shootingRange)
        {
            PerformStrafe();
        }
    }

    void InitializeBot()
    {
        // Set random reaction time based on difficulty
        reactionTime = Random.Range(reactionTimeMin, reactionTimeMax);
        nextStrafeChange = Time.time + Random.Range(1f, 3f);
        hasBoughtThisRound = false;
    }

    void SetDifficultySettings()
    {
        switch (difficulty)
        {
            case BotDifficulty.Easy:
                baseAccuracy = 0.4f;
                reactionTimeMin = 0.4f;
                reactionTimeMax = 0.8f;
                aimSmoothing = 2f;
                aimOffset = 2f;
                detectionRange = 30f;
                break;

            case BotDifficulty.Medium:
                baseAccuracy = 0.6f;
                reactionTimeMin = 0.2f;
                reactionTimeMax = 0.5f;
                aimSmoothing = 5f;
                aimOffset = 1f;
                detectionRange = 50f;
                break;

            case BotDifficulty.Hard:
                baseAccuracy = 0.8f;
                reactionTimeMin = 0.1f;
                reactionTimeMax = 0.3f;
                aimSmoothing = 8f;
                aimOffset = 0.5f;
                detectionRange = 70f;
                break;

            case BotDifficulty.Expert:
                baseAccuracy = 0.95f;
                reactionTimeMin = 0.05f;
                reactionTimeMax = 0.15f;
                aimSmoothing = 12f;
                aimOffset = 0.2f;
                detectionRange = 100f;
                break;
        }
    }

    void FindTarget()
    {
        PlayerHealth[] allPlayers = FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None);
        float closestDistance = detectionRange;
        Transform closestTarget = null;

        foreach (PlayerHealth player in allPlayers)
        {
            // Skip self, dead players, and teammates
            if (player == health || player.IsDead()) continue;
            
            // Check if on different team (implement team check)
            GameObject playerObj = player.gameObject;
            BotAI botAI = playerObj.GetComponent<BotAI>();
            if (botAI != null && botAI.teamID == teamID) continue;

            float distance = Vector3.Distance(transform.position, player.transform.position);
            
            if (distance < closestDistance)
            {
                // Check line of sight
                Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out RaycastHit hit, distance))
                {
                    if (hit.collider.gameObject == playerObj)
                    {
                        closestDistance = distance;
                        closestTarget = player.transform;
                    }
                }
            }
        }

        currentTarget = closestTarget;
        if (currentTarget != null)
        {
            lastKnownTargetPos = currentTarget.position;
        }
    }

    void EngageTarget()
    {
        if (currentTarget == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        
        // Decide whether to retreat
        isRetreating = health.currentHealth < retreatHealthThreshold;

        // Movement logic
        if (isRetreating)
        {
            Retreat();
        }
        else if (distanceToTarget > optimalRange * 1.5f)
        {
            // Move closer
            navAgent.isStopped = false;
            navAgent.SetDestination(currentTarget.position);
        }
        else if (distanceToTarget < optimalRange * 0.5f)
        {
            // Too close, back up
            Vector3 retreatPos = transform.position - (currentTarget.position - transform.position).normalized * 5f;
            navAgent.SetDestination(retreatPos);
        }
        else
        {
            // Optimal range - stop and shoot
            navAgent.isStopped = true;
        }

        // Aim and shoot
        AimAtTarget();
        ShootAtTarget();
    }

    void AimAtTarget()
    {
        if (currentTarget == null) return;

        // Calculate aim point with difficulty-based offset
        Vector3 targetPoint = currentTarget.position + Vector3.up * 1.5f; // Head height
        Vector3 offset = Random.insideUnitSphere * aimOffset;
        targetPoint += offset;

        // Smooth aim
        Vector3 direction = (targetPoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * aimSmoothing);
    }

    void ShootAtTarget()
    {
        if (currentTarget == null) return;
        if (currentWeapon == null) return;
        if (isReloading) return;

        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        
        if (distanceToTarget > shootingRange) return;

        // Wait for reaction time
        if (Time.time - lastShotTime < reactionTime) return;

        // Check if need to reload
        if (currentWeapon.CurrentAmmo <= 0)
        {
            StartReload();
            return;
        }

        // Accuracy check based on difficulty
        float accuracyRoll = Random.value;
        if (accuracyRoll < baseAccuracy)
        {
            // Simulate weapon fire
            currentWeapon.Fire();
            lastShotTime = Time.time;
        }
    }

    void StartReload()
    {
        isReloading = true;
        // Assuming weapon has Reload() method
        if (currentWeapon != null)
        {
            Invoke(nameof(FinishReload), currentWeapon.ReloadTime);
        }
    }

    void FinishReload()
    {
        isReloading = false;
    }

    void Retreat()
    {
        // Move away from target
        Vector3 retreatDirection = (transform.position - currentTarget.position).normalized;
        Vector3 retreatPos = transform.position + retreatDirection * 10f;
        
        navAgent.isStopped = false;
        navAgent.SetDestination(retreatPos);
    }

    void PerformStrafe()
    {
        if (Time.time > nextStrafeChange)
        {
            strafeDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
            nextStrafeChange = Time.time + Random.Range(1f, 2f);
        }

        Vector3 strafeDir = transform.right * strafeDirection;
        Vector3 newPos = transform.position + strafeDir * strafeSpeed * Time.deltaTime;
        
        if (NavMesh.SamplePosition(newPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

    void Patrol()
    {
        // Random patrol when no target
        if (!navAgent.hasPath || navAgent.remainingDistance < 2f)
        {
            Vector3 randomPoint = GetRandomPatrolPoint();
            navAgent.SetDestination(randomPoint);
        }
    }

    Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f;
        randomDirection += transform.position;
        
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 20f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        
        return transform.position;
    }

    /// <summary>
    /// Buy phase logic - called at round start
    /// </summary>
    public void BuyPhase(int availableMoney)
    {
        if (hasBoughtThisRound) return;

        // Simple buy logic based on money
        if (availableMoney >= 4750 && preferredWeapons.Contains("AWP"))
        {
            BuyWeapon("AWP");
        }
        else if (availableMoney >= 3100 && preferredWeapons.Contains("M4A4"))
        {
            BuyWeapon("M4A4");
        }
        else if (availableMoney >= 2700 && preferredWeapons.Contains("AK-47"))
        {
            BuyWeapon("AK-47");
        }
        else if (availableMoney >= 1500)
        {
            BuyWeapon("MP5-SD");
        }
        else if (availableMoney >= 500)
        {
            BuyWeapon("USP-S");
        }

        // Buy armor if enough money
        if (availableMoney >= 1000)
        {
            BuyArmor();
        }

        hasBoughtThisRound = true;
    }

    void BuyWeapon(string weaponName)
    {
        // This would integrate with your economy/weapon system
        Debug.Log($"{botName} bought {weaponName}");
        // TODO: Integrate with actual weapon purchasing system
    }

    void BuyArmor()
    {
        Debug.Log($"{botName} bought armor");
        // TODO: Integrate with armor system
    }

    public void OnRoundStart()
    {
        hasBoughtThisRound = false;
        isReloading = false;
        isRetreating = false;
    }

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Visualize shooting range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        
        // Visualize optimal range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, optimalRange);
    }
}
