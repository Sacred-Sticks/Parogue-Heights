using UnityEngine;

namespace Parogue_Heights
{
    public class BouncePadPlatform : MonoBehaviour, IPlatform
    {
        public static float bounceForce { private get; set; } = 15f;

        #region UnityEvents
        private void Start()
        {
            Registry.Register<IPlatform>(transform.position, this);
            EnableParticles();
        }
        #endregion

        private void EnableParticles()
        {
            var effectCount = transform.GetChild(0).childCount;
            for (var i = 0; i < effectCount; i++)
                transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }

        #region Platform
        public GameObject GameObject => gameObject;

        public void OnPlayerEnter(Rigidbody body)
        {
            body.AddForce(transform.up * bounceForce, ForceMode.VelocityChange);
        }
        #endregion
    }
}
