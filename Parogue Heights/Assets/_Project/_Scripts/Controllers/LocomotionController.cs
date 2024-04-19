using Kickstarter.Observer;
using UnityEngine;

namespace Parogue_Heights
{
    public abstract class LocomotionController : Observable
    {
        [SerializeField] protected float walkingSpeed;
        [SerializeField] protected float sprintSpeed;
        [SerializeField] private float jumpHeight;

        protected bool isGrounded;
        protected float movementSpeed;

        // Cached References & Constant Values
        protected Rigidbody body;
        private float jumpVelocity;
        private const float radiusMultiplier = 0.5f;
        private const float groundDistance = 1f;
        private const float airborneMovementMultiplier = 40f;
        private float groundRadius;
        private Transform cameraTransform;

        #region UnityEvents
        private void Awake()
        {
            transform.root.TryGetComponent(out body);
        }

        protected virtual void Start()
        {
            jumpVelocity = Mathf.Sqrt(Mathf.Abs(jumpHeight * Physics.gravity.y * 2));
            var capsule = transform.root.GetComponentInChildren<CapsuleCollider>();
            groundRadius = capsule.radius * radiusMultiplier;
            cameraTransform = Camera.main.transform;
            movementSpeed = walkingSpeed;
        }
        #endregion

        protected void MoveTowards(Vector3 direction)
        {
            direction = cameraTransform.TransformDirection(direction);
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;
            NotifyObservers(new MovementChange(direction));
            if (!isGrounded)
            {
                AirborneMoveTowards(direction);
                return;
            }
            var currentVelocity = Vector3.ProjectOnPlane(body.velocity, transform.up);
            if (currentVelocity.sqrMagnitude > movementSpeed * movementSpeed)
                return;
            var desiredVelocity = direction * movementSpeed;
            body.AddForce(desiredVelocity - currentVelocity, ForceMode.VelocityChange);
        }

        private void AirborneMoveTowards(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;
            var currentVelocity = Vector3.ProjectOnPlane(body.velocity, Vector3.up);
            bool velocityExceeds = currentVelocity.sqrMagnitude > walkingSpeed * walkingSpeed;
            bool isGainingSpeed = Vector3.Dot(direction, currentVelocity) > 0;

            if (isGainingSpeed && velocityExceeds)
                return;

            var desiredForce = direction.normalized * airborneMovementMultiplier;
            body.AddForce(desiredForce, ForceMode.Force);
        }

        protected void Jump()
        {
            if (!isGrounded)
                return;
            body.AddForce(jumpVelocity * Vector3.up, ForceMode.VelocityChange);
            NotifyObservers(GroundedStatus.Jump);
        }

        protected void CheckGrounded()
        {
            var ray = new Ray(transform.position + Vector3.up, -Vector3.up);
            isGrounded = Physics.SphereCast(ray, groundRadius, groundDistance);
            NotifyObservers(isGrounded ? GroundedStatus.Landing : GroundedStatus.Falling);
        }

        #region Notifications
        public enum GroundedStatus
        {
            Jump,
            Landing,
            Falling,
        }

        public struct MovementChange : INotification
        {
            public Vector3 Direction { get; }

            public MovementChange(Vector3 localDirection)
            {
                Direction = localDirection.normalized;
            }
        }
        #endregion
    }

}
