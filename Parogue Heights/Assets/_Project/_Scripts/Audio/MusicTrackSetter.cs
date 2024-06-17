using Kickstarter.DependencyInjection;
using UnityEngine;

namespace Parogue_Heights
{
    public class MusicTrackSetter : MonoBehaviour, IDependencyProvider
    {
        [SerializeField, Provide] private MusicPlayer.NotificationType musicTrackType;
    }
}
