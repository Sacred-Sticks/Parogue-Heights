using Kickstarter.Inputs;
using Kickstarter.Observer;
using Kickstarter.DependencyInjection;
using UnityEngine;
using Cinemachine;

namespace Parogue_Heights
{
    public class PlayerRotationController : Observable, IInputReceiver
    {
        [Inject] private CameraMediator cameraMediator;

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
            rotation.x -= direction * cameraSpeed * Settings.Sensitivity;
            if (rotation.x < 180 && rotation.x > verticalRange)
                rotation.x = verticalRange;
            if (rotation.x > 180 && rotation.x < 360 - verticalRange)
                rotation.x = 360 - verticalRange;
            cameraFollow.rotation = Quaternion.Euler(rotation);

            cameraMediator?.CalibrateByXAngle(rotation.x, verticalRange);
        }

        private void RotateYAxis(float direction)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.y += direction * rotationSpeed * Settings.Sensitivity;
            transform.rotation = Quaternion.Euler(rotation);
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
