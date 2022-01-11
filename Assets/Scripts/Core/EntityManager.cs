using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class EntityManager
    {
        private uint maxEntities = 2000;
        private int entityCount;
        private List<Entity> entities;
        private Queue<uint> entityIDs;
        
        public EntityManager()
        {
            entities = new List<Entity>();
            entityIDs = new Queue<uint>();
            
            for (uint i = 0; i < maxEntities; i++) {
                entityIDs.Enqueue(i);
            }
        }
        
        public Entity CreateEntity()
        {
            if (entityCount >= maxEntities) {
                Debug.LogError("Max amount of entities reached!");
                return null;
            }
            
            var id = entityIDs.Dequeue();
            var e = new Entity(id);
            entityCount++;
            return e;
        }

        public void AddEntity(Entity entity) => entities.Add(entity);

        public void DestroyEntity(Entity entity)
        {
            entities.Remove(entity);
            entityIDs.Enqueue(entity.GetId());
            entityCount--;
        }
    }
}