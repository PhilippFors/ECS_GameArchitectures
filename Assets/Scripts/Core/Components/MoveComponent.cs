using UnityEngine;

namespace Core.Components
{
    [CreateAssetMenu(menuName = "ECS/Components/MoveComponent", fileName = "new movecomponent")]
    public class MoveComponent : EntityComponent
    {
        public float moveSpeed;
    }
}