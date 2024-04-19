using Kickstarter.Inputs;
using Kickstarter.Observer;
using UnityEngine;

namespace Dodge_Bots
{
    public class PlayerRotationController : Observable, IInputReceiver
    {
        [SerializeField] protected float rotationSpeed;
        [SerializeField] private float cameraSpeed;
        [SerializeField] private Vector2Input rotationInput;
        [SerializeField] private float verticalRange;
        [SerializeField] private Transform cameraFollow;

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

        private void FixedUpdate()
        {
            RotateCamera(rawInput.y);
            RotateTowards(rawInput.x);
            rawInput = Vector2.zero;
        }

        private void RotateCamera(float direction)
        {
            var rotation = cameraFollow.rotation.eulerAngles;
            rotation.x -= direction * cameraSpeed;
            if (rotation.x < 180 && rotation.x > verticalRange)
                rotation.x = verticalRange;
            if (rotation.x > 180 && rotation.x < 360 - verticalRange)
                rotation.x = 360 - verticalRange;
            cameraFollow.rotation = Quaternion.Euler(rotation);
        }

        private void RotateTowards(float direction)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.y += direction * rotationSpeed;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
