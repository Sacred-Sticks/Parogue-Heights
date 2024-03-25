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
            foreach (var key in Registry.Collection.Keys.Where(v => v is Vector3))
                if (Vector3.SqrMagnitude((Vector3)key - position) < radius * radius)
                    return true;
            return false;
        }
    }
}
