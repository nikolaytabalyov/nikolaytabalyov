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
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private Button _restartButton; 
        [SerializeField] private GameObject _titleScreen;
        [Header("Variables")]
        private int _score;
        private float _spawnRate = 1.0f;
        public State GameState { get; private set; }

        public static GameManager Instance { get; private set; }
        
        public enum State {
            Running,
            GameOver
        }

        #region Unity Methods
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
        }
        #endregion 

        #region Other Methods
        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameOver() {
            GameState = State.GameOver;
            _restartButton.gameObject.SetActive(true);
            _gameOverText.gameObject.SetActive(true);
        }

        public void UpdateScore(int scoreToAdd) {
            _score += scoreToAdd;
            _scoreText.text = "Score: " + _score;
        }

        public void StartGame(int difficulty) {
            GameState = State.Running;
            _score = 0;
            UpdateScore(0);
            // diff = Easy -> spawn every 1s, diff = Normal -> spawn every 0.66s, diff = Hard -> spawn every 0.45s
            _spawnRate /=  difficulty == 1 ? difficulty : difficulty == 2 ? difficulty - 0.5f : difficulty - 0.8f;
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
