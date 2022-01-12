using Core.Components;
using UnityEngine;
using UnityEngine.Profiling;

namespace Core.Systems
{
    public class MoveSystem : ComponentSystem
    {
        public override void Tick()
        {
            Profiler.BeginSample("Move System");
            base.Tick();
            Profiler.EndSample();
        }
        
        public override void Start(Entity entity)
        {
            var transform = entity.GetComponent<TransformComponent>();
            transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
        }

        public override void Update(Entity e)
        {
            var move = e.GetComponent<MoveComponent>();
            var transform = e.GetComponent<TransformComponent>();
            
            Profiler.BeginSample("move system update");
            var speed = move.moveSpeed;
            var pos = transform.position;
            var delta = Time.deltaTime;

            if (move.moveTime < move.maxMoveTime) {
                if (move.turnAround) {
                    pos.x += speed * delta;
                }
                else {
                    pos.x -= speed * delta;
                }

                move.moveTime += delta;
            }
            else {
                move.turnAround = !move.turnAround;
                move.moveTime = 0;
            }

            transform.position = pos;
            Profiler.EndSample();
        }
    }
}