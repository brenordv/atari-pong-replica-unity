using System;
using Project.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    [Serializable]
    public class VectorSpeed
    {
        public float minSpeed = .8f;
        public float maxSpeed = 1.2f;
    }

    public class Ball : MonoBehaviour
    {
        public VectorSpeed horizontalSpeed;
        public VectorSpeed verticalSpeed;
        public float difficultyMultiplier = 1.3f;
        public SoundPlayer soundPlayerPrefab;
        public AudioClip[] audioClips;

        private Rigidbody2D _rigidbody;
        private SoundPlayer _soundPlayer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _soundPlayer = Instantiate(soundPlayerPrefab, transform);
            _soundPlayer.audioClips = audioClips;
        }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody.velocity = new Vector2(
                Random.Range(horizontalSpeed.minSpeed, horizontalSpeed.maxSpeed) * (Random.value > 0.5f ? -1 : 1),
                Random.Range(verticalSpeed.minSpeed, verticalSpeed.maxSpeed) * (Random.value > 0.5f ? -1 : 1));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var velocity = _rigidbody.velocity;
            if (other.CompareTag(Constants.Tags.Limit))
            {
                // Collided with top or bottom limits, respectively.
                if ((other.transform.position.y > transform.position.y && velocity.y > 0) ||
                    (other.transform.position.y < transform.position.y && velocity.y < 0))
                {
                    _soundPlayer.Play();
                    _rigidbody.velocity = new Vector2(velocity.x, -velocity.y);
                }
            }
            else if (other.CompareTag(Constants.Tags.Paddle))
            {
                // Collided with left or right paddles, respectively.
                if ((other.transform.position.x < transform.position.x && velocity.x < 0) ||
                    (other.transform.position.x > transform.position.x && velocity.x > 0))
                {
                    _soundPlayer.Play();
                    _rigidbody.velocity = new Vector2(-velocity.x, velocity.y) * difficultyMultiplier;
                }
            }
        }
    }
}