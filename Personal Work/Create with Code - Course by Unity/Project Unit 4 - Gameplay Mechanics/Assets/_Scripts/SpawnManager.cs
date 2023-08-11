using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _powerUpPrefab;
    private float _spawnRange = 9.0f;
    private Vector3 _spawnPosition;
    private int enemiesPerWave = 1;
    private int _aliveEnemies;

    private void Update() {
        _aliveEnemies = FindObjectsOfType<Enemy>().Length;
        if (_aliveEnemies == 0)
            SpawnEnemyWave(enemiesPerWave);
    }

    void Start() {
        _aliveEnemies = 1;
        SpawnEnemyWave(enemiesPerWave);
    }

    private void SpawnEnemyWave(int enemiesToSpawn) {
        for (int spawnedEnemies = 0; spawnedEnemies < enemiesToSpawn; spawnedEnemies++) {
            Instantiate(_enemyPrefab, GetRandomSpawnPosition(_enemyPrefab), _enemyPrefab.transform.rotation);        
        }
        Instantiate(_powerUpPrefab, GetRandomSpawnPosition(_powerUpPrefab), _powerUpPrefab.transform.rotation);
        enemiesPerWave++;
    }

    private Vector3 GetRandomSpawnPosition(GameObject objectToSpawn){
        _spawnPosition = new Vector3(Random.Range(-_spawnRange, _spawnRange), objectToSpawn.CompareTag("Enemy")? 0:0.35f, Random.Range(-_spawnRange, _spawnRange));
        return _spawnPosition;
    }
}
