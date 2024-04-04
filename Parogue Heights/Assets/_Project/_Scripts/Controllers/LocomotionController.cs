using Kickstarter.Observer;
using UnityEngine;

namespace Parogue_Heights
{
    public abstract class LocomotionController : Observable
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpHeight;

        protected bool isGrounded;
        private Vector3 airborneVelocity;
        private Vector3 initialAirborneVelocity;

        // Cached References & Constant Values
        protected Rigidbody body;
        private float jumpVelocity;
        private const float accelerationRate = 0.1f;
        private const float radiusMultiplier = 0.5f;
        private const float groundDistance = 1f;
        private float groundRadius;

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
        }
        #endregion

        protected void MoveTowards(Vector3 direction)
        {
            direction = Vector3.ProjectOnPlane(direction, Vector3.up);
            NotifyObservers(new MovementChange(transform.InverseTransformDirection(direction)));
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
            if (Vector3.Dot(direction, initialAirborneVelocity) > 0)
                return;
            if (airborneVelocity.sqrMagnitude > movementSpeed * movementSpeed)
                return;
            var desiredVelocity = direction * movementSpeed;
            var deltaVelocity = (desiredVelocity - airborneVelocity) * accelerationRate;
            airborneVelocity += deltaVelocity;
            body.AddForce(deltaVelocity, ForceMode.VelocityChange);
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
            bool previouslyGrounded = isGrounded;
            isGrounded = Physics.SphereCast(ray, groundRadius, groundDistance);
            NotifyObservers(isGrounded ? GroundedStatus.Landing : GroundedStatus.Falling);
            if (!previouslyGrounded || isGrounded)
                return;
            initialAirborneVelocity = Vector3.ProjectOnPlane(body.velocity, Vector3.up);
            airborneVelocity = Vector3.zero;
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
            public Vector3 LocalDirection { get; }

            public MovementChange(Vector3 localDirection)
            {
                LocalDirection = localDirection.normalized;
            }
        }
        #endregion
    }

}
