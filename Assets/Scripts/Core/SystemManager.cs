using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SystemManager
    {
        private Dictionary<string, ComponentSystem> systems;
        private Dictionary<string, ComponentMask> masks;

        public SystemManager()
        {
            masks = new Dictionary<string, ComponentMask>();
            systems = new Dictionary<string, ComponentSystem>();
        }

        public T RegisterSystem<T>() where T : ComponentSystem, new()
        {
            var hash = typeof(T).Name;
            if (systems.ContainsKey(hash)) {
                Debug.LogError("System is already registered");
                return null;
            }

            var sys = new T();
            sys.entities = new List<Entity>();
            systems.Add(typeof(T).Name, sys);
            sys.Initialize();
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
            systems.Remove(hash);
        }

        public void UpdateEntity(Entity entity, ComponentMask entityMask)
        {
            foreach (var pair in systems) {
                var type = pair.Key;
                var system = pair.Value;
                masks.TryGetValue(type, out var systemMask);

                if ((entityMask & systemMask) == systemMask) {
                    if (!system.entities.Contains(entity)) {
                        system.entities.Add(entity);
                        system.Start(entity);
                    }
                }
                else {
                    system.entities.Remove(entity);
                }
            }
        }

        public void DestroyEntity(Entity entity)
        {
            foreach (var pair in systems) {
                var sys = pair.Value;
                if (sys.entities.Contains(entity)) {
                    sys.entities.Remove(entity);
                }
            }
        }
        
        public void SetComponentMask<T>(ComponentMask mask) where T : ComponentSystem
        {
            var hash = typeof(T).Name;
            masks.Add(hash, mask);
        }

        public void Update()
        {
            if (systems.Count == 0) {
                return;
            }
            foreach (var pair in systems) {
                var sys = pair.Value;
                sys.Update();
            }
        }
    }
}