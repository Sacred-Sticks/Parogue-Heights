using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class LoseConditionDetector : MonoBehaviour
    {
        [Inject] private ILoseCondition loseCondition;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == loseCondition.GameObject)
                loseCondition.Lose();
        }
    }
}
