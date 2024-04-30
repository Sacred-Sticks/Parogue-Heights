using UnityEngine;
using Kickstarter.Observer;
using System.Collections;
using static Parogue_Heights.LocomotionController;

namespace Parogue_Heights
{
    public class PlayerAnimator : MonoBehaviour, IObserver<MovementChange>, IObserver<GroundedStatus>
    {
        [SerializeField] private Transform playerModel;
        private GroundedStatus currentState = GroundedStatus.Landing;

        private Animator _animator;
        private readonly int _speed = Animator.StringToHash("Speed");
        private readonly int jump = Animator.StringToHash("Jump");
        private readonly int falling = Animator.StringToHash("Falling");
        private readonly int landing = Animator.StringToHash("Landing");

        private float activeSpeed;
        Coroutine speedModification;

        #region UnityEvents
        private void Start()
        {
            _animator = transform.root.GetComponentInChildren<Animator>();
            transform.root.GetComponentInChildren<LocomotionController>().AddObserver(this);
        }
        #endregion

        #region Notifications
        public void OnNotify(MovementChange argument)
        {
            playerModel.LookAt(playerModel.position + argument.Direction);
            float newSpeed = argument.Direction.normalized.magnitude;
            if (activeSpeed == newSpeed)
                return;
            if (speedModification != null)
                StopCoroutine(speedModification);
            speedModification = StartCoroutine(ModifySpeed(activeSpeed, newSpeed));
            activeSpeed = newSpeed;
        }

        public void OnNotify(GroundedStatus argument)
        {
            if (argument == currentState)
                return;
            currentState = argument;
            System.Func<IEnumerator> action = argument switch
            {
                GroundedStatus.Jump => Jump,
                GroundedStatus.Falling => Falling,
                GroundedStatus.Landing => Landing,
                _ => throw new System.ArgumentOutOfRangeException()
            };
            StartCoroutine(action());
        }
        #endregion

        private IEnumerator ModifySpeed(float activeSpeed, float newSpeed)
        {
            float transitionDuration = 0.1f;
            float elapsedTime = 0.0f;
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / transitionDuration;
                _animator.SetFloat(_speed, Mathf.Lerp(activeSpeed, newSpeed, t));
                yield return new WaitForSeconds(Time.deltaTime);
            }
            _animator.SetFloat(_speed, newSpeed);
        }

        private IEnumerator Jump()
        {
            const float jumpDuration = 0.5f;
            _animator.Play(jump, 0, jumpDuration);
            yield return new WaitForSeconds(jumpDuration);
            Falling();
        }

        private IEnumerator Falling()
        {
            const float fallDuration = 0.5f;
            _animator.Play(falling, 0, fallDuration);
            yield break;
        }

        private IEnumerator Landing()
        {
            const float landDuration = 0.5f;
            _animator.Play(landing, 0, landDuration);
            yield break;
        }
    }
}
