using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class PlatformManager : MonoBehaviour
    {
        private static Dictionary<Vector3, IPlatform> platformStorage;

        private static float radius = 3.5f;

        #region UnityEvents
        private void Awake()
        {
            platformStorage = new Dictionary<Vector3, IPlatform>();
        }
        #endregion

        public static void RegisterPlatform(Vector3 key, IPlatform platform)
            => platformStorage.Add(key, platform);

        public static void DeregisterPlatform(Vector3 key)
            => platformStorage.Remove(key);

        public static void ActivatePlatform(Vector3 key, Rigidbody body)
        {
            if (platformStorage.ContainsKey(key))
                platformStorage[key].OnPlayerEnter(body);
        }

        public static bool IsWithinRadius(Vector3 position)
        {
            foreach (var key in platformStorage.Keys)
                if (Vector3.SqrMagnitude(key - position) < radius * radius)
                    return true;
            return false;
        }
    }
}
