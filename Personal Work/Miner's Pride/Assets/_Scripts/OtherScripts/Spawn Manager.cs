using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    private int _enemiesPerWave;
    [SerializeField] private Vector2 _playerSpawnPoint = new(0, -6); 
    [SerializeField] private float _timeBeforeWave = 3.0f;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Transform _player;
    private PlayerController _playerController;
    #endregion
    
    #region Unity Methods
    private void Start() {
        _enemiesPerWave = 3;
        _playerController = _player.GetComponent<PlayerController>();
    }
    
    private void Update() {
        if (CheckForEnemyCount() == 0) {
            _player.position = _playerSpawnPoint;
            _playerController.enabled = false;
            if (_timeBeforeWave <= 0) {
                _timeBeforeWave = 3.0f;
                _playerController.enabled = true;
                StartWave();
                _enemiesPerWave += 2;
            } else {
                _timeBeforeWave -= Time.deltaTime;
            }
        }
    }
    #endregion
    
    #region Other Methods
    private int CheckForEnemyCount() {
        return FindObjectsOfType<Enemy>().Length;
    }
    private void StartWave() {
        for (int i = 0; i < _enemiesPerWave; i++) {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy() {
        if (CheckForEnemyCount() < _enemiesPerWave) {
            int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);
            int randomEnemy = Random.Range(0, _enemyPrefabs.Length);
            GameObject spawnedEnemy = Instantiate(_enemyPrefabs[randomEnemy], _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
    #endregion
}
