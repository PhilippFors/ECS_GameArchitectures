using Core.Components;
using UnityEngine;

namespace Core.Systems
{
    public class MoveSystem : ComponentSystem
    {
        public override void Start(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>();
            transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
        }

        public override void Update()
        {
            // foreach (var e in entities) {
            //     var c = e.GetComponent<MoveComponent>();
            // }
        }
    }
}