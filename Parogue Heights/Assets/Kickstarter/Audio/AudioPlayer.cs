using UnityEngine;
using Kickstarter.Observer;
using Kickstarter.SerializableTypes;

namespace Kickstarter.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [TypeFilter(typeof(Observable))]
        [SerializeField] private SerializableType listenTarget;


    }
}
