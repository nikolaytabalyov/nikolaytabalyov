using System;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public event EventHandler<OnEnemyProjectileHitEventArgs> OnEnemyProjectileHit;
    public class OnEnemyProjectileHitEventArgs : EventArgs {
        public float damageArgs;
    }
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    #endregion
    
    #region Components
    [Header("Components")]
    private SpawnManager _spawnManager;
    #endregion
    private void EnemyProjectile_OnWaveStateChanged(object sender, SpawnManager.OnWaveOverEventArgs e) {
        if (e.newWaveState == SpawnManager.WaveState.Over && this != null) {
            Destroy(gameObject);
        }
    }

    #region Unity Methods
    private void Start() {
        _spawnManager = SpawnManager.Instance;
        _spawnManager.OnWaveStateChanged += EnemyProjectile_OnWaveStateChanged;
    }

    private void Update() {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
    #endregion
    
    #region Other Methods
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            OnEnemyProjectileHit?.Invoke(this, new OnEnemyProjectileHitEventArgs {damageArgs = _damage});
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
    #endregion
}
