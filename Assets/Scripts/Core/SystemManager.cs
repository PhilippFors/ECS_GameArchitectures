using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SystemManager
    {
        private Dictionary<int, ComponentSystem> componentSystems;
        private Dictionary<int, ComponentMask> componentMasks;

        public SystemManager()
        {
            componentMasks = new Dictionary<int, ComponentMask>();
            componentSystems = new Dictionary<int, ComponentSystem>();
        }

        public T RegisterSystem<T>() where T : ComponentSystem, new()
        {
            var hash = typeof(T).GetHashCode();
            if (componentSystems.ContainsKey(hash)) {
                Debug.LogError("System is already registered");
                return null;
            }

            var sys = new T();
            sys.entities = new List<Entity>();
            componentSystems.Add(typeof(T).GetHashCode(), sys);
            sys.Initialize();
            return sys;
        }

        public void RemoveSystem<T>() where T : ComponentSystem
        {
            var hash = typeof(T).GetHashCode();
            if (componentSystems.ContainsKey(hash)) {
                Debug.LogError("System is not registered");
                return;
            }

            componentSystems.TryGetValue(hash, out var val);
            componentSystems.Remove(hash);
        }

        public void UpdateEntity(Entity entity, ComponentMask entityMask)
        {
            foreach (var pair in componentSystems) {
                var type = pair.Key;
                var system = pair.Value;
                componentMasks.TryGetValue(type, out var systemMask);

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
            foreach (var pair in componentSystems) {
                var sys = pair.Value;
                if (sys.entities.Contains(entity)) {
                    sys.entities.Remove(entity);
                }
            }
        }
        
        public void SetComponentMask<T>(ComponentMask mask) where T : ComponentSystem
        {
            var hash = typeof(T).GetHashCode();
            componentMasks.Add(hash, mask);
        }

        public void Update()
        {
            if (componentSystems.Count == 0) {
                return;
            }
            
            foreach (var pair in componentSystems) {
                var sys = pair.Value;
                sys.Tick();
            }
        }
    }
}