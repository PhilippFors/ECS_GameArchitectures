using System;

namespace Core
{
    [Flags]
    public enum ComponentMask
    {
        None = 0,
        HelloWorldComponent = 1,
        MoveComponent = 2,
        RenderComponent = 3,
        TransformComponent = 4
    }
}