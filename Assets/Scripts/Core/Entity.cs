using System.Collections.Generic;

namespace Core
{
    public class Entity
    {
        private Dictionary<string, EntityComponent> components;
        private uint entityID;
        
        public Entity(uint entityID)
        {
            this.entityID = entityID;
            components = new Dictionary<string, EntityComponent>();
        }
        
        public virtual T GetComponent<T>() where T : EntityComponent
        {
            components.TryGetValue(typeof(T).Name, out var val);
            return (T) val;
        }

        public virtual void AddComponent<T>() where T : EntityComponent, new()
        {
            components.Add(typeof(T).Name, new T());
        }

        public virtual void RemoveComponent<T>() where T : EntityComponent
        {
            var hash = typeof(T).Name;
            if (components.ContainsKey(hash)) {
                components.Remove(hash);
            }
        }
        
        public virtual bool HasComponent<T>() where T : EntityComponent
        {
            components.TryGetValue(typeof(T).Name, out var val);
            if (val != null) {
                return true;
            }

            return false;
        }

        public uint GetId() => entityID;
    }
}