using UnityEngine;

namespace Core.Components
{
    [CreateAssetMenu(menuName = "ECS/Components/Render Component")]
    public class RenderComponent : EntityComponent
    {
        public Mesh mesh;
        public Material material;
    }
}