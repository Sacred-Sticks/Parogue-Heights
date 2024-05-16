using Kickstarter.Audio;
using Kickstarter.Bootstrapper;
using Kickstarter.DependencyInjection;
using Kickstarter.Observer;
using UnityEngine;

namespace Parogue_Heights
{
    public class MusicListener : Observable
    {
        [Inject] private SceneLoader sceneLoader
        {
            set
            {
                NotifyObservers(new AudioPlayer.AudioEvent(notificationType));
            }
        }

        [SerializeField] private NotificationType notificationType;

        public enum NotificationType
        {
            Baseline,
            Percussion,
            Highlights,
            Melody,
        }
    }
}
