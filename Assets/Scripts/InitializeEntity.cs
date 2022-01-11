using Core;
using Core.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Util
{
    public class InitializeEntity : MonoBehaviour
    {
        public int entityCount;
        
        private Coordinator coordinator;

        private void Awake()
        {
            coordinator = GetComponent<Coordinator>();
        }

        private void Start()
        {
            // Creating entities and entity configs is still possible in code, no editor needed technically
            var newEntity = coordinator.CreateEntity("");
            var moveComponent = coordinator.AddComponent<MoveComponent>(newEntity);
            moveComponent.moveSpeed = 20;
            var transformComponent = coordinator.AddComponent<TransformComponent>(newEntity);
            transformComponent.scale = Vector3.one;
            var rendercomponent = coordinator.AddComponent<RenderComponent>(newEntity);
            rendercomponent.mesh = Resources.Load<Mesh>("testMesh");
            rendercomponent.material = Resources.Load("testMaterial") as Material;
            coordinator.CreateConfig(newEntity, "MoveEntity_3");

            for (int i = 0; i < entityCount; i++) {
                coordinator.CreateEntity("MoveEntity");
                coordinator.CreateEntity("MoveEntity_3");
            }
        }

        [Button]
        private void AddEntity(string type, int amount)
        {
            for (int i = 0; i < amount; i++) {
                coordinator.CreateEntity(type);
            }
        }
    }
}