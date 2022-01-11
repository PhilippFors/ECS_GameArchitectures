using System.Collections.Generic;

namespace Core
{
    [System.Serializable]
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

        public void AddComponent(EntityComponent component)
        {
            components.Add(component.GetType().Name, component);
        }

        public virtual void RemoveComponent<T>() where T : EntityComponent
        {
            var hash = typeof(T).Name;
            if (components.ContainsKey(hash)) {
                components.Remove(hash);
            }
        }

        public List<EntityComponent> GetComponents()
        {
            var list = new List<EntityComponent>();
            foreach (var pair in components) {
                list.Add(pair.Value);
            }

            return list;
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