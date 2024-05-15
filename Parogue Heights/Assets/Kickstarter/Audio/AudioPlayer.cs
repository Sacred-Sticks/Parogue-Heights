using UnityEngine;
using Kickstarter.Observer;
using Kickstarter.SerializableTypes;
using System;
using System.Collections.Generic;
using Kickstarter.Extensions;

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

        private Dictionary<Enum, ClipGroup> clipGroups;
        private Type type;
        private AudioSource audioSource;

        #region UnityEvents
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            AddToObservablesWatchlist();

            var notificationTypeEnum = GetNotificationType();

            if (notificationTypeEnum == null)
                Debug.LogError($"{listenTarget.Type} does not have a defined NotificationType Enum");

            clipGroups = new Dictionary<Enum, ClipGroup>();
            clipGroups.LoadDictionary(clips, notificationTypeEnum);
        }
        #endregion

        private void AddToObservablesWatchlist()
        {
            var component = GetComponent(listenTarget.Type) as Observable;
            component.AddObserver(this);
            type = listenTarget.Type;
        }

        private Type GetNotificationType()
        {
            var nestedTypes = type.GetNestedTypes();
            Type notificationTypeEnum = null;

            foreach (var nestedType in nestedTypes)
            {
                if (!nestedType.IsEnum || nestedType.Name != "NotificationType")
                    continue;
                notificationTypeEnum = nestedType;
                break;
            }

            return notificationTypeEnum;
        }

        public void OnNotify(AudioEvent notification)
        {
            var clipGroup = clipGroups[notification.Value];

            if (clipGroup.Clips.Length == 0)
            {
                Debug.LogWarning($"No audio clips found for {notification.Value} on {gameObject.name}");
                return;
            }

            int index = UnityEngine.Random.Range(0, clipGroup.Clips.Length);
            audioSource.clip = clipGroup.Clips[index];
            audioSource.Play();
        }

        #region SubTypes
        public class AudioEvent : Observable.INotification
        {
            public AudioEvent(Enum value)
            {
                Value = value;
            }

            public Enum Value { get; }
        }

        [Serializable]
        public class ClipGroup
        {
            [SerializeField] private AudioClip[] clips;

            public AudioClip[] Clips => clips;
        }
        #endregion
    }
}
