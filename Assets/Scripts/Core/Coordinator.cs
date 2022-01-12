using Core.Systems;
using UnityEngine;

namespace Core
{
    [DefaultExecutionOrder(-1)]
    public class Coordinator : MonoBehaviour
    {
        [SerializeField] private EntityManager entityManager; // serialized by Unity, no instantiation needed
        private SystemManager systemManager;

        private void Awake()
        {
            systemManager = new SystemManager();
            entityManager.Init();
            
            RegisterSystem<MoveSystem>();
            RegisterSystem<RenderSystem>();
            
            var moveSystemMask = ComponentMask.MoveComponent | ComponentMask.TransformComponent;
            var renderSystemMask = ComponentMask.RenderComponent | ComponentMask.TransformComponent;

            SetComponentMask<MoveSystem>(moveSystemMask);
            SetComponentMask<RenderSystem>(renderSystemMask);
        }

        // main update loop
        private void Update()
        {
            systemManager.Update();
        }

        public void RegisterSystem<T>() where T : ComponentSystem, new()
        {
            systemManager.RegisterSystem<T>();
        }

        public void RemoveSystem<T>() where T : ComponentSystem
        {
            systemManager.RemoveSystem<T>();
        }

        public void SetComponentMask<T>(ComponentMask mask) where T : ComponentSystem
        {
            systemManager.SetComponentMask<T>(mask);
        }
        
        public Entity CreateEntity(string type)
        {
            var entity = entityManager.CreateEntity(type);
            systemManager.UpdateEntity(entity, entityManager.GetComponentMask(entity));
            return entity;
        }

        public void DestoryEntity(Entity entity)
        {
            entityManager.DestroyEntity(entity);
            systemManager.DestroyEntity(entity);
        }
        
        public void CreateConfig(Entity entity, string configName) => entityManager.CreateConfig(entity, configName);
        
        public T AddComponent<T>(Entity entity) where T : EntityComponent
        {
            var c = entityManager.AddComponent<T>(entity);
            systemManager.UpdateEntity(entity, entityManager.GetComponentMask(entity));
            return c;
        }

        public void RemoveComponent<T>(Entity entity) where T : EntityComponent
        {
            entityManager.RemoveComponent<T>(entity);
            systemManager.UpdateEntity(entity, entityManager.GetComponentMask(entity));
        }
    }
}