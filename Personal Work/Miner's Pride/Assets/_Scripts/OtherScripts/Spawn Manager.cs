using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public enum WaveState {
        Running, 
        Over
    }

    public event EventHandler<OnWaveOverEventArgs> OnWaveStateChanged;
    public class OnWaveOverEventArgs : EventArgs {
        public WaveState newWaveState;
    }
    
    public static SpawnManager Instance { get; private set; }
    #region Variables
    [Header("Variables")]
    [SerializeField] private WaveState _waveState;
    private int _enemiesPerWave;
    [SerializeField] private Vector2 _playerSpawnPoint = new(0, -6); 
    [SerializeField] private float _timeBeforeWave = 3.0f;
    [SerializeField] private int _currentLevel = 0;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private Transform[] _spawnPointsLevel1;
    [SerializeField] private Transform[] _spawnPointsLevel2;
    [SerializeField] private Transform[] _spawnPointsLevel3;
    [SerializeField] private Transform[] _spawnPointsLevel4;
    [SerializeField] private Transform[] _spawnPointsLevel5;
    [SerializeField] private GameObject[] _levels;
    private Dictionary<int, GameObject> _levelsDictionary;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Transform _player;
    private PlayerController _playerController;
    private List<Transform[]> _spawnPoints;
    [SerializeField] private Transform[] _currentSpawnPoints;
    [SerializeField] private GameObject _currentLevelObject;
    #endregion
    
    #region Unity Methods
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
        _spawnPoints = new();
        _levelsDictionary = new();
        AddSpawnPoints();
        SetUpLevelsDictionary(_levels);
    }
    private void Start() {
        _enemiesPerWave = 3;
        _playerController = _player.GetComponent<PlayerController>();
    }
    
    private void Update() {
        HandleWaves();
    }
    #endregion
    
    #region Other Methods
    private void HandleWaves() {
        if (CheckForEnemyCount() == 0) {
            if (_timeBeforeWave == 3.0f) {
                if (_currentLevelObject != null) {
                    _currentLevelObject.SetActive(false);
                }
                _currentLevel++;
                GetRandomLevel();
                _waveState = WaveState.Over;
                OnWaveStateChanged?.Invoke(this, new OnWaveOverEventArgs {newWaveState = _waveState});
                _playerController.enabled = false;
            }
            _player.position = _playerSpawnPoint;

            if (_timeBeforeWave <= 0) {
                _waveState = WaveState.Running;
                OnWaveStateChanged?.Invoke(this, new OnWaveOverEventArgs {newWaveState = _waveState});
                SpawnWaveLevel();
                _timeBeforeWave = 3.0f;
                _playerController.enabled = true;
                _enemiesPerWave += 2;
            } else {
                _timeBeforeWave -= Time.deltaTime;
            }
        }
    }

    private void AddSpawnPoints() {
        _spawnPoints.Add(_spawnPointsLevel1);
        _spawnPoints.Add(_spawnPointsLevel2);
        _spawnPoints.Add(_spawnPointsLevel3);
        _spawnPoints.Add(_spawnPointsLevel4);
        _spawnPoints.Add(_spawnPointsLevel5);
    }

    private void SetUpLevelsDictionary(GameObject[] levels) {
        for (int i = 1; i <= levels.Length; i++) {
            _levelsDictionary.Add(i, levels[i-1]);
        }
    }

    private int CheckForEnemyCount() {
        return FindObjectsOfType<Enemy>().Length;
    }

    private void SpawnWaveLevel() {
        _currentLevelObject.SetActive(true);
        for (int i = 0; i < _enemiesPerWave; i++) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        if (CheckForEnemyCount() < _enemiesPerWave) {
            int randomSpawnPoint = UnityEngine.Random.Range(0, _currentSpawnPoints.Length);
            int randomEnemy = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
            GameObject spawnedEnemy = Instantiate(_enemyPrefabs[randomEnemy], _currentSpawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }

    private void GetRandomLevel() {
        int randomIndex = UnityEngine.Random.Range(1, 6);
        _currentLevelObject = _levelsDictionary[randomIndex];
        _currentSpawnPoints = _spawnPoints[randomIndex-1];
    }
    #endregion
}
