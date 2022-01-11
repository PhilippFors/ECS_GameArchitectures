using UnityEngine;

namespace Core.Components
{
    [CreateAssetMenu(menuName = "ECS/Components/MoveComponent", fileName = "new movecomponent")]
    public class MoveComponent : EntityComponent
    {
        public float moveSpeed;
        public float moveTime;
        public float maxMoveTime = 1f;
        public bool turnAround;
    }
}