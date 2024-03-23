using UnityEngine;

namespace Parogue_Heights
{
    public interface IPlatform
    {
        public GameObject GameObject { get; }
        public void OnPlayerEnter(Rigidbody body);
    }
}
