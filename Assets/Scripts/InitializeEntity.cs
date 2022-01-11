using Core;
using Core.Components;
using UnityEngine;

namespace Util
{
    public class InitializeEntity : MonoBehaviour
    {
        private Coordinator coordinator;
        private void Awake()
        {
            coordinator = GetComponent<Coordinator>();
        }

        private void Start()
        {
            // Creating entities is still possible in code, no editor needed technically
            var newEntity = coordinator.CreateEntity("");
            var component = coordinator.AddComponent<MoveComponent>(newEntity);
            component.moveSpeed = 20;
            coordinator.CreateConfig(newEntity, "MoveEntity_3");
            coordinator.DestoryEntity(newEntity);
            
            for (int i = 0; i < 1000; i++) {
                coordinator.CreateEntity("MoveEntity");
                coordinator.CreateEntity("MoveEntity_2");
                coordinator.CreateEntity("MoveEntity_3");
            }
        }
    }
}