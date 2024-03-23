using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class PlatformManager : MonoBehaviour
    {
        private static float radius = 3.5f;
        private static Dictionary<Vector3, IPlatform> registry;

        private void Awake()
        {
            registry = GenericRegistry<Vector3, IPlatform>.Registry;
            registry.Clear();
        }

        public static void ActivatePlatform(Vector3 key, Rigidbody body)
        {
            if (registry.ContainsKey(key))
                registry[key].OnPlayerEnter(body);
        }

        public static bool IsWithinRadius(Vector3 position)
        {
            foreach (var key in registry.Keys)
                if (Vector3.SqrMagnitude(key - position) < radius * radius)
                    return true;
            return false;
        }
    }
}
