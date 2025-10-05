using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS17.Core
{
    /// <summary>
    /// Service Locator pattern for managing game systems
    /// Provides centralized access to core services without tight coupling
    /// </summary>
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceLocator();
                }
                return _instance;
            }
        }

        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        /// <summary>
        /// Register a service
        /// </summary>
        public void Register<T>(T service) where T : class
        {
            Type type = typeof(T);
            
            if (services.ContainsKey(type))
            {
                Debug.LogWarning($"[ServiceLocator] Service {type.Name} is already registered. Overwriting...");
                services[type] = service;
            }
            else
            {
                services.Add(type, service);
                Debug.Log($"[ServiceLocator] Registered service: {type.Name}");
            }
        }

        /// <summary>
        /// Unregister a service
        /// </summary>
        public void Unregister<T>() where T : class
        {
            Type type = typeof(T);
            
            if (services.ContainsKey(type))
            {
                services.Remove(type);
                Debug.Log($"[ServiceLocator] Unregistered service: {type.Name}");
            }
        }

        /// <summary>
        /// Get a registered service
        /// </summary>
        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            
            if (services.TryGetValue(type, out object service))
            {
                return service as T;
            }
            
            Debug.LogError($"[ServiceLocator] Service {type.Name} not found! Make sure it's registered.");
            return null;
        }

        /// <summary>
        /// Try to get a service without logging errors
        /// </summary>
        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            
            if (services.TryGetValue(type, out object obj))
            {
                service = obj as T;
                return service != null;
            }
            
            service = null;
            return false;
        }

        /// <summary>
        /// Check if a service is registered
        /// </summary>
        public bool IsRegistered<T>() where T : class
        {
            return services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Clear all services (useful for scene transitions)
        /// </summary>
        public void Clear()
        {
            Debug.Log("[ServiceLocator] Clearing all services");
            services.Clear();
        }

        /// <summary>
        /// Get count of registered services
        /// </summary>
        public int Count => services.Count;
    }

    /// <summary>
    /// Static accessor for convenience
    /// </summary>
    public static class Services
    {
        public static void Register<T>(T service) where T : class => ServiceLocator.Instance.Register(service);
        public static void Unregister<T>() where T : class => ServiceLocator.Instance.Unregister<T>();
        public static T Get<T>() where T : class => ServiceLocator.Instance.Get<T>();
        public static bool TryGet<T>(out T service) where T : class => ServiceLocator.Instance.TryGet(out service);
        public static bool IsRegistered<T>() where T : class => ServiceLocator.Instance.IsRegistered<T>();
    }
}
