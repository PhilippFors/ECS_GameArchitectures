using UnityEngine;
using UsefulCode.Utilities;

namespace Util
{
    public class UnityHelper : SingletonBehaviour<UnityHelper>
    {
        public Object InstantiateObject(Object obj, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            return Instantiate(obj, position, rotation);
        }

        public ScriptableObject InstantiateObject(ScriptableObject obj) => Instantiate(obj);
    }
}