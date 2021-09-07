using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        public float scoreCoordinates = 3.4f;
        public int lives = 3;
        public Ball ballPrefab;
        public Text scoreP1Text;
        public Text scoreP2Text;
        public SoundPlayer pointScoredSoundPlayerPrefab;
        public SoundPlayer gameOverSoundPlayerPrefab;
        public AudioClip[] pointScoredAudioClips;
        public AudioClip[] gameOverAudioClips;

        private SoundPlayer _pointScoredSoundPlayerPrefab;
        private SoundPlayer _gameOverSoundPlayerPrefab;
        private int _scoreP1;
        private int _scoreP2;
        private Ball _currentBall;
        private int _currentLives;
        private bool _gameOver;

        private void Awake()
        {
            _pointScoredSoundPlayerPrefab = Instantiate(pointScoredSoundPlayerPrefab, transform);
            _pointScoredSoundPlayerPrefab.audioClips = pointScoredAudioClips;

            _gameOverSoundPlayerPrefab = Instantiate(gameOverSoundPlayerPrefab, transform);
            _gameOverSoundPlayerPrefab.audioClips = gameOverAudioClips;

            _currentLives = lives;
        }

        private void UpdateScoreboard()
        {
            scoreP1Text.text = _scoreP1.ToString();
            scoreP2Text.text = _scoreP2.ToString();
        }

        private void LoseLife()
        {
            var remaining = _currentLives - 1;
            _currentLives = remaining < 0 ? 0 : remaining;
        }

        private bool IsGameOver()
        {
            _gameOver = _currentLives == 0;
            return _gameOver;
        }

        void Start()
        {
            SpawnBall();
        }

        private void SpawnBall()
        {
            DestroyCurrentBall();

            _currentBall = Instantiate(ballPrefab, transform);
            _currentBall.transform.position = Vector3.zero;
            UpdateScoreboard();
        }

        private void DestroyCurrentBall()
        {
            if (_currentBall == null) return;
            Destroy(_currentBall.gameObject);
        }

        void Update()
        {
            if (_currentBall == null) return;

            if (_currentBall.transform.position.x > scoreCoordinates)
            {
                _scoreP1++;
                ProcessPostScore();
            }

            if (_currentBall.transform.position.x < -scoreCoordinates)
            {
                _scoreP2++;
                ProcessPostScore();
            }
        }

        private void ProcessPostScore()
        {
            if (_gameOver) return;

            LoseLife();
            if (IsGameOver())
            {
                _gameOverSoundPlayerPrefab.Play();
                DestroyCurrentBall();
                return;
            }

            _pointScoredSoundPlayerPrefab.Play();
            SpawnBall();
        }
    }
}