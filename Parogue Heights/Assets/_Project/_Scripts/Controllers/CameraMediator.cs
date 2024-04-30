using Cinemachine;
using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class CameraMediator : MonoBehaviour, IDependencyProvider
    {
        [Provide] private CinemachineVirtualCamera virtualCamera;
        [Provide] private Cinemachine3rdPersonFollow follow;
        private CinemachineBasicMultiChannelPerlin noise;

        [SerializeField] private AnimationCurve cameraShakeRate;
        [SerializeField] private float cameraShakeMultiplier;
        [SerializeField] private float cameraShakeHighSpeed;

        private Rigidbody body;

        #region UnityEvents
        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            follow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }

        private void Start()
        {
            body = Registry.Get<Rigidbody>(RegistryStrings.PlayerRigidbody);
        }

        private void FixedUpdate()
        {
            float speedSquared = body.velocity.magnitude;
            float amplitude = cameraShakeRate.Evaluate(speedSquared / cameraShakeHighSpeed);
            amplitude *= cameraShakeMultiplier;
            
            noise.m_AmplitudeGain = amplitude;
        }
        #endregion


    }
}
