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
            // Creating entities is still possible in code, no editor needed technically
            // var newEntity = coordinator.CreateEntity("MoveEntity");
            // var component = coordinator.AddComponent<MoveComponent>(newEntity);
            // component.moveSpeed = 20;
            // coordinator.CreateConfig(newEntity, "MoveEntity_3");

            for (int i = 0; i < entityCount; i++) {
                coordinator.CreateEntity("MoveEntity");
                // coordinator.CreateEntity("MoveEntity_2");
                // coordinator.CreateEntity("MoveEntity_3");
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