using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using TMPro;

namespace NikolayTabalyov 
{
    public class GameManager : MonoBehaviour {
    
        [Header("Variables")]
        [SerializeField] private List<GameObject> _targets = new();
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        private int _score;
        private float _spawnRate = 1.0f;
        public State GameState { get; private set; }

        public static GameManager Instance { get; private set; }
        
        public enum State {
            Running,
            GameOver
        }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }
        private void Start() {
            GameState = State.Running;
            _score = 0;
            UpdateScore(0);
            StartCoroutine(SpawnTarget());
        }

        public void GameOver() {
            GameState = State.GameOver;
            _gameOverText.gameObject.SetActive(true);
        }

        public void UpdateScore(int scoreToAdd) {
            _score += scoreToAdd;
            _scoreText.text = "Score: " + _score;
        }

        IEnumerator SpawnTarget() {
            while (GameState == State.Running) {
                yield return new WaitForSeconds(_spawnRate);
                Instantiate(_targets[Random.Range(0, _targets.Count)]);
            }
        }
    }
}
