using UnityEngine;

namespace Core
{
    public class Coordinator : MonoBehaviour
    {
        private EntityManager entityManager;
        private SystemManager systemManager;

        private void Awake()
        {
            entityManager = new EntityManager();
            systemManager = new SystemManager();
        }

        private void Update()
        {
            systemManager.Update();
        }

        public Entity CreateEntity() => entityManager.CreateEntity();

        public void AddComponent<T>(Entity entity) where T : EntityComponent, new()
        {
            entity.AddComponent<T>();
        }

        public void RemoveComponent<T>(Entity entity) where T : EntityComponent
        {
            entity.RemoveComponent<T>();
        }
    }
}