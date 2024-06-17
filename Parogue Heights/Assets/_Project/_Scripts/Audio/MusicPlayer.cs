using Kickstarter.Audio;
using Kickstarter.DependencyInjection;
using Kickstarter.Observer;
using UnityEngine;

namespace Parogue_Heights
{
    public class MusicPlayer : Observable
    {
        [Inject]
        private NotificationType audioNotification
        {
            set
            {
                NotifyObservers(new AudioPlayer.AudioEvent(value));
            }
        }

        public enum NotificationType
        {
            Menu,
            Gameplay,
        }
    }
}
