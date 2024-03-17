using UnityEngine;

namespace Parogue_Heights
{
    public class PlatformDetector : MonoBehaviour
    {
        #region UnityEvents
        private void OnCollisionEnter(Collision collision)
        {
            PlatformManager.ActivatePlatform(collision.transform.position);
        }
        #endregion
    }
}
