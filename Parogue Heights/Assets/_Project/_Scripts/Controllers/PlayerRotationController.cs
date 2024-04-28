using Kickstarter.Inputs;
using Kickstarter.Observer;
using Kickstarter.DependencyInjection;
using UnityEngine;
using Cinemachine;

namespace Parogue_Heights
{
    public class PlayerRotationController : Observable, IInputReceiver
    {
        private CinemachineVirtualCamera vCam;
        [Inject] private CinemachineVirtualCamera VCam
        {
            get => vCam;
            set
            {
                vCam = value;

                highRig = new RigSettings(3.0f, VCam.LookAt.transform.localPosition + Vector3.up * 0.25f);
                midRig = new RigSettings(4.0f, VCam.LookAt.transform.localPosition);
                lowRig = new RigSettings(2.5f, VCam.LookAt.transform.localPosition - Vector3.up * 0.5f);
            }
        }
        [Inject] private Cinemachine3rdPersonFollow vCamFollow;

        [SerializeField] protected float rotationSpeed;
        [SerializeField] private float cameraSpeed;
        [SerializeField] private Vector2Input rotationInput;
        [SerializeField] private float verticalRange;
        [SerializeField] private Transform cameraFollow;

        private RigSettings highRig;
        private RigSettings midRig;
        private RigSettings lowRig;

        private Vector2 rawInput;

        #region InputHandler
        public void RegisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            rotationInput.RegisterInput(OnRotationInputChange, playerIdentifier);
        }

        public void DeregisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            rotationInput.DeregisterInput(OnRotationInputChange, playerIdentifier);
        }

        private void OnRotationInputChange(Vector2 input)
        {
            rawInput += input;
        }
        #endregion

        #region UnityEvents
        private void FixedUpdate()
        {
            RotateXAxis(rawInput.y);
            RotateYAxis(rawInput.x);
            rawInput = Vector2.zero;
        }
        #endregion

        private void RotateXAxis(float direction)
        {
            var rotation = cameraFollow.rotation.eulerAngles;
            rotation.x -= direction * cameraSpeed;
            if (rotation.x < 180 && rotation.x > verticalRange)
                rotation.x = verticalRange;
            if (rotation.x > 180 && rotation.x < 360 - verticalRange)
                rotation.x = 360 - verticalRange;
            cameraFollow.rotation = Quaternion.Euler(rotation);

            if (vCamFollow == null)
                return;

            float angle = rotation.x;

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
            vCamFollow.CameraDistance = range;

            vCam.LookAt.transform.localPosition = Vector3.Lerp(lowRig.Position, highRig.Position, Mathf.Abs(angle + verticalRange) / (verticalRange * 2));
        }

        private void RotateYAxis(float direction)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.y += direction * rotationSpeed;
            transform.rotation = Quaternion.Euler(rotation);
        }

        private class Rig
        {
            public Rig(Transform cameraLookAtTarget, float cameraDistance)
            {
                CameraLookAtTarget = cameraLookAtTarget;
                CameraDistance = cameraDistance;
            }

            public float CameraDistance { get; }
            public Transform CameraLookAtTarget { get; }
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
