using Cinemachine;
using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class CameraMediator : MonoBehaviour, IDependencyProvider
    {
        [Provide] private CameraMediator Mediator() => this;

        [SerializeField] private AnimationCurve cameraShakeRate;
        [SerializeField] private float cameraShakeMultiplier;
        [SerializeField] private float cameraShakeHighSpeed;

        private Rigidbody body;
        private CinemachineVirtualCamera virtualCamera;
        private Cinemachine3rdPersonFollow follow;
        private CinemachineBasicMultiChannelPerlin noise;

        private RigSettings highRig;
        private RigSettings midRig;
        private RigSettings lowRig;

        #region UnityEvents
        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            highRig = new RigSettings(4.0f, virtualCamera.LookAt.transform.localPosition + Vector3.up * 0.25f);
            midRig = new RigSettings(6.0f, virtualCamera.LookAt.transform.localPosition);
            lowRig = new RigSettings(3.0f, virtualCamera.LookAt.transform.localPosition - Vector3.up * 0.5f);

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

        public void CalibrateByXAngle(float angle, float verticalRange)
        {
            if (angle > 180)
                angle = angle - 360;
            float lowValue = midRig.CameraDistance;
            float highValue = lowRig.CameraDistance;
            if (angle > 0)
            {
                lowValue = midRig.CameraDistance;
                highValue = highRig.CameraDistance;
            }

            float range = Mathf.Lerp(lowValue, highValue, Mathf.Abs(angle / verticalRange));
            follow.CameraDistance = range;

            virtualCamera.LookAt.transform.localPosition = Vector3.Lerp(lowRig.Position, highRig.Position, Mathf.Abs(angle + verticalRange) / (verticalRange * 2));
        }

        private struct RigSettings
        {
            public RigSettings(float cameraDistance, Vector3 position)
            {
                CameraDistance = cameraDistance;
                Position = position;
            }

            public float CameraDistance { get; }
            public Vector3 Position { get; }
        }
    }
}
