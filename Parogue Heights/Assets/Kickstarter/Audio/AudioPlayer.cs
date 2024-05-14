using UnityEngine;
using Kickstarter.Observer;
using Kickstarter.SerializableTypes;
using System;

namespace Kickstarter.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour, Observer.IObserver<AudioPlayer.AudioEvent>
    {
        [SerializeField, TypeFilter(typeof(Observable))]
        private SerializableType listenTarget;

        public SerializableType ListenTarget => listenTarget;

        [Space(10)]
        [SerializeField]
        private ClipGroup[] clips;

        private void Awake()
        {
            var component = GetComponent(listenTarget.Type) as Observable;
            component.AddObserver(this);
            Type type = component.GetType();
            Debug.Log(type);
        }

        public void OnNotify(AudioEvent argument)
        {
            throw new System.NotImplementedException();
        }

        public abstract class AudioEvent : Observable.INotification
        {
            
        }

        public class AudioEvent<T> : AudioEvent
        {

        }

        [Serializable]
        public class ClipGroup
        {
            [SerializeField] private AudioClip[] clips;
        }
    }
}
