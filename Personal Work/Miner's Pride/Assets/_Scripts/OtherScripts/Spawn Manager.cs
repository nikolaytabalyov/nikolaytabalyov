using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private int _maxEnemies;
    [SerializeField] private int _currentEnemies;

    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;
    #endregion
    
    #region Unity Methods
    private void Start() {
        _currentEnemies = 0;
        _maxEnemies = 3;
        InvokeRepeating(nameof(SpawnEnemy), 1f, 1f);
    }
    
    private void Update() {
        CheckForEnemyCount();
    }
    #endregion
    
    #region Other Methods
    private void CheckForEnemyCount() {
        _currentEnemies = FindObjectsOfType<Enemy>().Length;
    }
    private void SpawnEnemy() {
        if (_currentEnemies < _maxEnemies) {
            _currentEnemies++;
            int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);
            int randomEnemy = Random.Range(0, _enemyPrefabs.Length);
            GameObject spawnedEnemy = Instantiate(_enemyPrefabs[randomEnemy], _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
    #endregion
}
