using System.Linq;
using UnityEngine;

namespace Parogue_Heights
{
    public class PlatformManager : MonoBehaviour
    {
        private static float radius = 3.5f;

        #region UnityEvents
        private void Awake()
        {
           Registry.ClearKeyType<Vector3>();
        }
        #endregion

        public static void ActivatePlatform(Vector3 key, Rigidbody body)
        {
            var platform = Registry.Get<IPlatform>(key);
            platform?.OnPlayerEnter(body);
        }

        public static bool IsWithinRadius(Vector3 position)
        {
            foreach (var key in Registry.Collection.Keys.Select(v => (Vector3) v))
                if (Vector3.SqrMagnitude(key - position) < radius * radius)
                    return true;
            return false;
        }
    }
}
