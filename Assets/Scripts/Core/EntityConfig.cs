using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "ECS/EntityConfig")]
    public class EntityConfig : ScriptableObject
    {
        public string entityType;
        public List<EntityComponent> components = new List<EntityComponent>();
    }
}