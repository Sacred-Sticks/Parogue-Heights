using UnityEngine;

namespace Parogue_Heights
{
    public interface ILoseCondition
    {
        public void Lose();
        public GameObject GameObject { get; }
    }
}
