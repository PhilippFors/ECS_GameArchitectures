using Core.Components;
using UnityEngine;

namespace Core.Systems
{
    public class MoveSystem : ComponentSystem
    {
        public override void Update()
        {
            foreach (var e in entities) {
                var c = e.GetComponent<MoveComponent>();
            }
        }
    }
}