using Kickstarter.Observer;
using UnityEngine;
using static Parogue_Heights.LocomotionController;

namespace Parogue_Heights
{
    public class PlayerAnimator : MonoBehaviour, IObserver<MovementChange>
    {
        private Animator _animator;
        private readonly int _velocityX = Animator.StringToHash("X Velocity");
        private readonly int _velocityZ = Animator.StringToHash("Z Velocity");

        private void Start()
        {
            _animator = transform.root.GetComponentInChildren<Animator>();
            transform.root.GetComponentInChildren<LocomotionController>().AddObserver(this);
        }

        public void OnNotify(MovementChange argument)
        {
            var direction = argument.LocalDirection;
            _animator.SetFloat(_velocityX, direction.x);
            _animator.SetFloat(_velocityZ, direction.z);
        }
    }
}
