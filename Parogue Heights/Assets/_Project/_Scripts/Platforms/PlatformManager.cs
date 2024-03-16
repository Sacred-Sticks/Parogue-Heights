using System.Collections.Generic;
using UnityEngine;

namespace Parogue_Heights
{
    public class PlatformManager : MonoBehaviour
    {
        private static Dictionary<Vector3, IPlatform> platformStorage;

        #region UnityEvents
        private void Awake()
        {
            platformStorage = new Dictionary<Vector3, IPlatform>();
        }
        #endregion

        public static void RegisterPlatform(Vector3 key, IPlatform platform)
            => platformStorage.Add(key, platform);

        public static void ActivatePlatform(Vector3 key)
        {
            if (platformStorage.ContainsKey(key))
                platformStorage[key].OnPlayerEnter();
        }
    }
}
