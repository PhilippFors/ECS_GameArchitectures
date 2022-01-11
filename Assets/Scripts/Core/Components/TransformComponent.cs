using UnityEngine;

namespace Core.Components
{
    [CreateAssetMenu(menuName = "ECS/Components/Transform Component")]
    public class TransformComponent : EntityComponent
    {
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation = Quaternion.identity;
    }
}