using UnityEngine;

namespace Parogue_Heights
{
    public class BouncePadPlatform : MonoBehaviour, IPlatform
    {
        public static float bounceForce { private get; set; } = 15f;

        #region UnityEvents
        private void Start()
        {
            GenericRegistry<Vector3, IPlatform>.Register(transform.position, this);
        }
        #endregion

        #region Platform
        public GameObject GameObject => gameObject;

        public void OnPlayerEnter(Rigidbody body)
        {
            body.AddForce(transform.up * bounceForce, ForceMode.VelocityChange);
        }
        #endregion
    }
}
