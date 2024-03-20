using UnityEngine;

namespace Parogue_Heights
{
    public class PlatformDetector : MonoBehaviour
    {
        private Rigidbody body;

        #region UnityEvents
        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlatformManager.ActivatePlatform(collision.transform.position, body);
        }
        #endregion
    }
}