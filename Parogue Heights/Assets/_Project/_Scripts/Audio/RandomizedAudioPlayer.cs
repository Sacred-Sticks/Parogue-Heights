using UnityEngine;

namespace Parogue_Heights
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomizedAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] audioClips;

        private AudioSource audioSource;

        #region UnityEvents
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        #endregion

        public void PlayRandomAudio()
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.resource = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}
