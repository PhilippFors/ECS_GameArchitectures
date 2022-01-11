using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SystemManager
    {
        private Dictionary<string, ComponentSystem> systems;
        private Dictionary<string, EntityView> entityViews;
        public SystemManager()
        {
            systems = new Dictionary<string, ComponentSystem>();
            entityViews = new Dictionary<string, EntityView>();
        }

        public T RegisterSystem<T>() where T : ComponentSystem, new()
        {
            var hash = typeof(T).Name;
            if (systems.ContainsKey(hash)) {
                Debug.LogError("System is already registered");
                return null;
            }

            var sys = new T();
            systems.Add(typeof(T).Name, sys);
            sys.Start();
            return sys;
        }

        public void RemoveSystem<T>() where T : ComponentSystem
        {
            var hash = typeof(T).Name;
            if (systems.ContainsKey(hash)) {
                Debug.LogError("System is not registered");
                return;
            }

            systems.TryGetValue(hash, out var val);
            val.OnDisable();
            systems.Remove(hash);
        }

        public EntityView GetEntityView<T>() where T : EntityComponent
        {
            var hash = typeof(T).Name;
            if (!entityViews.ContainsKey(hash)) {
                entityViews.Add(hash, new EntityView());
            }

            entityViews.TryGetValue(hash, out var val);
            return val;
        }

        public void Update()
        {
            foreach (var pair in systems) {
                var sys = pair.Value;
                sys.Update();
            }
        }
    }
}