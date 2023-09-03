using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    private int _enemiesPerWave;

    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;
    #endregion
    
    #region Unity Methods
    private void Start() {
        _enemiesPerWave = 3;
    }
    
    private void Update() {
        if (CheckForEnemyCount() == 0) {
            StartWave();
            _enemiesPerWave += 2;
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
