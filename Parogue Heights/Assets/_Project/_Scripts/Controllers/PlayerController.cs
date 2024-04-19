using Kickstarter.Inputs;
using UnityEngine;

namespace Parogue_Heights
{
    public class PlayerController : LocomotionController, IInputReceiver
    {
        [SerializeField] private Vector2Input movementInput;
        [SerializeField] private FloatInput sprintInput;
        [SerializeField] private FloatInput groundPoundInput;
        [SerializeField] private FloatInput jumpInput;

        private Vector3 rawMovementInput;
        
        #region InputHandler
        public void RegisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            movementInput.RegisterInput(OnMovementInputChange, playerIdentifier);
            sprintInput.RegisterInput(OnSprintInputChange, playerIdentifier);
            groundPoundInput.RegisterInput(OnGroundPoundInputChange, playerIdentifier);
            jumpInput.RegisterInput(OnJumpInputChange, playerIdentifier);
        }

        public void DeregisterInputs(Player.PlayerIdentifier playerIdentifier)
        {
            movementInput.DeregisterInput(OnMovementInputChange, playerIdentifier);
            sprintInput.DeregisterInput(OnSprintInputChange, playerIdentifier);
            groundPoundInput.DeregisterInput(OnGroundPoundInputChange, playerIdentifier);
            jumpInput.DeregisterInput(OnJumpInputChange, playerIdentifier);
        }

        private void OnMovementInputChange(Vector2 input)
        {
            rawMovementInput = new Vector3(input.x, 0, input.y);
        }

        private void OnSprintInputChange(float input)
        {
            movementSpeed = input == 0 ? walkingSpeed : sprintSpeed;
        }

        private void OnGroundPoundInputChange(float input)
        {

        }

        private void OnJumpInputChange(float input)
        {
            if (input == 0)
                return;
            Jump();
        }
        #endregion

        #region UnityEvents
        protected override void Start()
        {
            base.Start();
            Registry.Register(RegistryStrings.PlayerRigidbody, body);
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            MoveTowards(rawMovementInput);
        }
        #endregion
    }
}
