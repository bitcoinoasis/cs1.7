using UnityEngine;
using System;
using System.Collections.Generic;

namespace CS17.Core
{
    /// <summary>
    /// Central event system for loosely coupled communication between systems
    /// Implements observer pattern for game-wide events
    /// </summary>
    public class EventSystem : MonoBehaviour
    {
        private static EventSystem _instance;
        public static EventSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("EventSystem");
                    _instance = go.AddComponent<EventSystem>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

        #region Subscribe/Unsubscribe

        public void Subscribe<T>(string eventName, Action<T> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = Delegate.Combine(eventDictionary[eventName], listener);
            }
            else
            {
                eventDictionary[eventName] = listener;
            }
        }

        public void Subscribe(string eventName, Action listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = Delegate.Combine(eventDictionary[eventName], listener);
            }
            else
            {
                eventDictionary[eventName] = listener;
            }
        }

        public void Unsubscribe<T>(string eventName, Action<T> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = Delegate.Remove(eventDictionary[eventName], listener);
                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName);
                }
            }
        }

        public void Unsubscribe(string eventName, Action listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = Delegate.Remove(eventDictionary[eventName], listener);
                if (eventDictionary[eventName] == null)
                {
                    eventDictionary.Remove(eventName);
                }
            }
        }

        #endregion

        #region Publish

        public void Publish<T>(string eventName, T arg)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate del))
            {
                (del as Action<T>)?.Invoke(arg);
            }
        }

        public void Publish(string eventName)
        {
            if (eventDictionary.TryGetValue(eventName, out Delegate del))
            {
                (del as Action)?.Invoke();
            }
        }

        #endregion

        public void Clear()
        {
            eventDictionary.Clear();
        }
    }

    #region Event Names (Constants)

    /// <summary>
    /// Centralized event name constants to avoid string typos
    /// </summary>
    public static class GameEvents
    {
        // Player Events
        public const string PLAYER_SPAWNED = "PlayerSpawned";
        public const string PLAYER_DIED = "PlayerDied";
        public const string PLAYER_RESPAWNED = "PlayerRespawned";
        public const string PLAYER_DAMAGED = "PlayerDamaged";
        public const string PLAYER_HEALED = "PlayerHealed";
        
        // Combat Events
        public const string KILL_CONFIRMED = "KillConfirmed";
        public const string HEADSHOT_SCORED = "HeadshotScored";
        public const string WEAPON_FIRED = "WeaponFired";
        public const string WEAPON_RELOADED = "WeaponReloaded";
        public const string WEAPON_SWITCHED = "WeaponSwitched";
        
        // Game State Events
        public const string ROUND_STARTED = "RoundStarted";
        public const string ROUND_ENDED = "RoundEnded";
        public const string GAME_STARTED = "GameStarted";
        public const string GAME_ENDED = "GameEnded";
        public const string BUY_TIME_STARTED = "BuyTimeStarted";
        public const string BUY_TIME_ENDED = "BuyTimeEnded";
        
        // Economy Events
        public const string MONEY_CHANGED = "MoneyChanged";
        public const string ITEM_PURCHASED = "ItemPurchased";
        public const string PURCHASE_FAILED = "PurchaseFailed";
        
        // Ability Events
        public const string ABILITY_USED = "AbilityUsed";
        public const string ABILITY_COOLDOWN_READY = "AbilityCooldownReady";
        public const string RACE_CHANGED = "RaceChanged";
        public const string LEVEL_UP = "LevelUp";
        
        // Bot Events
        public const string BOT_SPAWNED = "BotSpawned";
        public const string BOT_DIED = "BotDied";
        public const string ALL_BOTS_CLEARED = "AllBotsCleared";
        
        // UI Events
        public const string MENU_OPENED = "MenuOpened";
        public const string MENU_CLOSED = "MenuClosed";
        public const string SCOREBOARD_TOGGLED = "ScoreboardToggled";
    }

    #endregion

    #region Event Data Classes

    /// <summary>
    /// Data passed with combat events
    /// </summary>
    public class CombatEventData
    {
        public GameObject attacker;
        public GameObject victim;
        public float damage;
        public bool isHeadshot;
        public string weaponName;

        public CombatEventData(GameObject attacker, GameObject victim, float damage, bool isHeadshot = false, string weaponName = "")
        {
            this.attacker = attacker;
            this.victim = victim;
            this.damage = damage;
            this.isHeadshot = isHeadshot;
            this.weaponName = weaponName;
        }
    }

    /// <summary>
    /// Data passed with money events
    /// </summary>
    public class MoneyEventData
    {
        public GameObject player;
        public int oldAmount;
        public int newAmount;
        public int delta;
        public string reason;

        public MoneyEventData(GameObject player, int oldAmount, int newAmount, string reason = "")
        {
            this.player = player;
            this.oldAmount = oldAmount;
            this.newAmount = newAmount;
            this.delta = newAmount - oldAmount;
            this.reason = reason;
        }
    }

    /// <summary>
    /// Data passed with ability events
    /// </summary>
    public class AbilityEventData
    {
        public GameObject caster;
        public string abilityName;
        public int abilityLevel;
        public float cooldownRemaining;

        public AbilityEventData(GameObject caster, string abilityName, int level, float cooldown = 0)
        {
            this.caster = caster;
            this.abilityName = abilityName;
            this.abilityLevel = level;
            this.cooldownRemaining = cooldown;
        }
    }

    #endregion
}
