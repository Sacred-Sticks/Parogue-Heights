using Kickstarter.Inputs;
using UnityEngine;
using UnityEngine.Windows;

namespace Dodge_Bots
{
    public class PlayerRotationController : RotationController, IInputReceiver
    {
        [SerializeField] private float cameraSpeed;
        [SerializeField] private Vector2Input rotationInput;
        [SerializeField] private float verticalRange;

        Vector2 rawInput;
        
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
            var rotation = transform.rotation.eulerAngles;
            rotation.x -= direction * cameraSpeed;
            if (rotation.x < 180 && rotation.x > verticalRange)
                rotation.x = verticalRange;
            if (rotation.x > 180 && rotation.x < 360 - verticalRange)
                rotation.x = 360 - verticalRange;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
