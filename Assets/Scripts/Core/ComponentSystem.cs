using System.Collections.Generic;

namespace Core
{
    public class ComponentSystem
    {
        public List<Entity> entities;
        
        public virtual void Start(Entity entity){}
        public virtual void Initialize(){}
        public virtual void Update(){}
    }
}