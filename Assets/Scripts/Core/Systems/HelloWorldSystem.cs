using Core.Components;
using UnityEngine;

namespace Core.Systems
{
    public class HelloWorldSystem : ComponentSystem
    {
        public override void Update()
        {
            foreach (var e in entities) {
                var c = e.GetComponent<HelloWorldComponent>();
            }
        }
    }
}