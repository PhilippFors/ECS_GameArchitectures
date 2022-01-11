using System.Collections.Generic;

namespace Core
{
    public class ComponentSystem
    {
        public virtual void Start(){}
        public virtual void Update(){}
        public virtual void OnDisable(){}
    }
}