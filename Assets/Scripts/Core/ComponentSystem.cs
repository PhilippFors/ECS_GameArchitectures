using System.Collections.Generic;
using UnityEngine.Profiling;

namespace Core
{
    public class ComponentSystem
    {
        public List<Entity> entities;
        
        public virtual void Start(Entity entity){}
        public virtual void Initialize(){}
        public virtual void Update(Entity entity){}
        public virtual void Tick()
        {
            for (int i = 0; i < entities.Count; i++) {
                Update(entities[i]);
            }
        }
    }
}