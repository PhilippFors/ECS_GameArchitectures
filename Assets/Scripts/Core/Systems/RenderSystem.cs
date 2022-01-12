using Core.Components;
using UnityEngine;
using UnityEngine.Profiling;

namespace Core.Systems
{
    public class RenderSystem : ComponentSystem
    {
        public override void Tick()
        {
            Profiler.BeginSample("Render System");
            base.Tick();
            Profiler.EndSample();
        }

        public override void Update(Entity e)
        {
            Profiler.BeginSample("Render get component");
            var transform = e.GetComponent<TransformComponent>();
            var renderer = e.GetComponent<RenderComponent>();
            Profiler.EndSample();
            
            transform.rotation = Quaternion.identity;
            Matrix4x4 transformationMatrix = new Matrix4x4();
            transformationMatrix.SetTRS(transform.position, transform.rotation, transform.scale);

            Graphics.DrawMesh(renderer.mesh, transformationMatrix, renderer.material, 0);
        }
    }
}