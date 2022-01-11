using UnityEngine;

namespace Core.Components
{
    [CreateAssetMenu(menuName = "ECS/Components/HelloWorldComponent")]
    public class HelloWorldComponent : EntityComponent
    {
        public string saySomething;
    }
}