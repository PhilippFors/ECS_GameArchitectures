using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Util;

namespace Core
{
    [Serializable]
    public class EntityManager
    {
        [SerializeField] private uint maxEntities = 2000;
        [SerializeField, ReadOnly] private int entityCount;
        [SerializeField] private List<EntityConfig> configs;

        private Dictionary<Entity, ComponentMask> componentMasks;
        private Queue<uint> entityIDs;
        private List<Entity> entities;

        public EntityManager()
        {
            Init();
        }

        public void Init()
        {
            entities = new List<Entity>();
            entityIDs = new Queue<uint>();
            componentMasks = new Dictionary<Entity, ComponentMask>();

            for (uint i = 0; i < maxEntities; i++) {
                entityIDs.Enqueue(i);
            }
        }

        public T AddComponent<T>(Entity entity) where T : EntityComponent
        {
            var inst = ScriptableObject.CreateInstance(typeof(T));
            var component = (EntityComponent) UnityHelper.Instance.InstantiateObject(inst);
            return (T) AddComponent(entity, component);
        }

        public EntityComponent AddComponent(Entity entity, EntityComponent component)
        {
            entity.AddComponent(component);

            // i just want the type, not the namespaces :(
            var componentType = component.GetType().ToString();
            var split = componentType.Split('.');
            var t = split[split.Length - 1];

            var maskParse = (ComponentMask) Enum.Parse(typeof(ComponentMask), t);
            componentMasks[entity] = componentMasks[entity] | maskParse;
            return component;
        }

        public void RemoveComponent<T>(Entity entity) where T : EntityComponent
        {
            entity.RemoveComponent<T>();
            var componentType = typeof(T);
            var maskParse = (ComponentMask) Enum.Parse(typeof(ComponentMask), componentType.ToString());
            componentMasks[entity] = componentMasks[entity] & maskParse;
        }

        public Entity CreateEntity(string type)
        {
            if (entityCount >= maxEntities) {
                Debug.LogError("Max amount of entities reached!");
                return null;
            }

            var id = entityIDs.Dequeue();
            var newEntity = new Entity(id);

            ComponentMask mask = ComponentMask.None;
            componentMasks.Add(newEntity, mask);

            if (type != "") {
                var config = configs.Find(x => x.entityType == type);
                foreach (var component in config.components) {
                    AddComponent(newEntity, component);
                }
            }

            AddEntity(newEntity);
            entityCount++;
            return newEntity;
        }

        public void CreateConfig(Entity entity, string configName)
        {
            var components = entity.GetComponents();
            var config = ScriptableObject.CreateInstance(typeof(EntityConfig));
            var configInstance = (EntityConfig) ScriptableObject.Instantiate(config);
            configInstance.name = configName;
            configInstance.entityType = configName;
            configInstance.components = components;

            configs.Add(configInstance);
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void DestroyEntity(Entity entity)
        {
            entities.Remove(entity);
            componentMasks.Remove(entity);
            entityIDs.Enqueue(entity.GetId());
            entityCount--;
        }

        public ComponentMask GetComponentMask(Entity entity)
        {
            componentMasks.TryGetValue(entity, out var val);
            return val;
        }
    }
}