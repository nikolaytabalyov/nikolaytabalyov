using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace NikolayTabalyov 
{
    public class GameManager : MonoBehaviour {
    
        [Header("Components")]
        [SerializeField] private List<GameObject> _targets = new();
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _livesText;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Button _restartButton; 
        [SerializeField] private GameObject _titleScreen;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _trailPrefab;
        [SerializeField] private AudioClip _explosionSound;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private AudioClip _gameOverSound;
        private AudioSource _audioSource;


        [Header("Variables")]
        private int _score;
        private int _lives = 3;
        private float _spawnRate = 1.0f;
        private bool _gameOverSoundPlayed = false;
        private float _minSwipeDistance = 0.5f;
        public bool isSwiping = false;
        private Vector3 _startSwipePosition;
        private Vector3 _currentSwipePosition;

        public State GameState { get; private set; }

        public static GameManager Instance { get; private set; }
        
        public enum State {
            Running,
            GameOver,
            Paused
        }

        #region Unity Methods
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
            
            _audioSource = GetComponent<AudioSource>();
            _livesText.text = "Lives: " + _lives;
        }

        private void Start() {
            PlayMusic();
            if (!PlayerPrefs.HasKey("Volume")) {
                PlayerPrefs.SetFloat("Volume", 1f);
                Load();
            } else {
                Load();
            }
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                ControlPauseState();
            }
            if (Input.GetMouseButtonDown(0)) {
                _startSwipePosition = ConvertMouseToCoords();
            }
            if (Input.GetMouseButton(0) && GameState != State.GameOver) {   
                _currentSwipePosition = ConvertMouseToCoords();
                if (Vector3.Distance(_startSwipePosition, _currentSwipePosition) > _minSwipeDistance && !isSwiping) {
                    Instantiate(_trailPrefab, ConvertMouseToCoords(), Quaternion.identity);
                    isSwiping = true;
                } 
            }   
        }
        #endregion 

        #region Sound Methods
        public void PlaySoundEffect(GameObject target) {
            if (target.CompareTag("Good")) {
                _audioSource.PlayOneShot(_hitSound, 1.5f);
            } else if (target.CompareTag("Bad")) {
                _audioSource.PlayOneShot(_explosionSound, 1.0f);
            }
        }   
        private void PlayMusic() {
            _audioSource.Play();
        }
        private void PlayGameOverSound() {
            if (!_gameOverSoundPlayed) {
                _audioSource.Stop();
                _audioSource.PlayOneShot(_gameOverSound, 1.0f); 
                _gameOverSoundPlayed = true;
            }
        }
        public void UpdateVolume() {
            _audioSource.volume = _volumeSlider.value;
            Save();
        }
        #endregion

        #region Save/Load Methods
        private void Load() {
            _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
        private void Save() {
            PlayerPrefs.SetFloat("Volume", _volumeSlider.value);
        }
        #endregion

        #region Other Methods
        private void UpdateHighScore() {
            if (_score > PlayerPrefs.GetInt("HighScore", 0)) {
                PlayerPrefs.SetInt("HighScore", _score);
                _highScoreText.text = "High Score: " + _score;
            } else {
                _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
            }
        }
        private Vector3 ConvertMouseToCoords() {
            float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float yPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            return new Vector3(xPos, yPos, 0);
        }
        private void ControlPauseState() {
            switch(GameState) {
                case State.Running:
                    _pauseScreen.gameObject.SetActive(true);
                    Time.timeScale = 0;
                    _audioSource.Pause();
                    GameState = State.Paused;
                    break;
                case State.Paused:
                    _pauseScreen.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    _audioSource.Play();
                    GameState = State.Running;
                    break;
            }
        }
        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameOver() {
            UpdateHighScore();
            PlayGameOverSound();
            GameState = State.GameOver;
            _restartButton.gameObject.SetActive(true);
            _gameOverText.gameObject.SetActive(true);
        }

        public void UpdateLives() {
            if (_lives > 0) {
                _lives -= 1;
                _livesText.text = "Lives: " + _lives;
                
            } else if (_lives <= 0) {
                GameOver();
            }
        }
        public void UpdateScore(int scoreToAdd) {
            _score += scoreToAdd;
            _scoreText.text = "Score: " + _score;
        }

        public void StartGame(int difficulty) {
            GameState = State.Running;
            _score = 0;
            UpdateScore(0);
            // diff = Easy -> spawn every 1s, diff = Normal -> spawn every 0.57s, diff = Hard -> spawn every 0.42s
            _spawnRate /=  difficulty; // == 1 ? difficulty : difficulty == 2 ? difficulty - 0.25f : difficulty - 0.6f;
            StartCoroutine(SpawnTarget());
            _titleScreen.gameObject.SetActive(false);
        }

        #endregion
        IEnumerator SpawnTarget() {
            while (GameState == State.Running) {
                yield return new WaitForSeconds(_spawnRate);
                Instantiate(_targets[Random.Range(0, _targets.Count)]);
            }
        }
    }
}
