using System.Collections.Generic;
using Core.Components;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Systems
{
    public class RenderSystem : ComponentSystem
    {
        public override void Update()
        {
            foreach (var e in entities) {
                var transform = e.GetComponent<TransformComponent>();
                var renderer = e.GetComponent<RenderComponent>();
                transform.rotation = Quaternion.identity;
                Matrix4x4 transformationMatrix = new Matrix4x4();
                transformationMatrix.SetTRS(transform.position, transform.rotation, transform.scale);
                Graphics.DrawMesh(renderer.mesh, transformationMatrix, renderer.material, 0);
            }
        }
    }
}