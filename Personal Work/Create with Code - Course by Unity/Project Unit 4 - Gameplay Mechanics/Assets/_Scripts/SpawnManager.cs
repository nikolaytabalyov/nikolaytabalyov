using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private float _spawnRange = 9.0f;

    void Start()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0, Random.Range(-_spawnRange, _spawnRange));
        Instantiate(_enemyPrefab, spawnPosition, _enemyPrefab.transform.rotation);
    }

    void Update()
    {

    }
}
