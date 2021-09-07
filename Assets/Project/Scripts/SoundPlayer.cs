using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class SoundPlayer : MonoBehaviour
    {
        public AudioClip[] audioClips;
        public float minPitch = 0.7f;
        public float maxPitch = 1.1f;

        public float minVolume = 0.3f;
        public float maxVolume = 1.0f;

        private AudioSource _audioSource;

        private float GetRandomPitch()
        {
            return Random.Range(minPitch, maxPitch);
        }

        private float GetRandomVolume()
        {
            return Random.Range(minVolume, maxVolume);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            var clip = audioClips.Random();
            if (clip == null) return;

            Play(clip);
        }

        private void Play(AudioClip clip)
        {
            _audioSource.pitch = GetRandomPitch();
            _audioSource.volume = GetRandomVolume();
            _audioSource.PlayOneShot(clip);            
        }
    }
}