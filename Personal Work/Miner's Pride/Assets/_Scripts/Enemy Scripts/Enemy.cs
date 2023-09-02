using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public enum EnemyState {
        Idle,
        Chase,
        Attack,
        Dead
    }
    #region Variables
    [Header("Variables")]
    private EnemyState _enemyState;
    public EnemyState GetEnemyState => _enemyState;
    private const string PICKAXE_BOOMERANG_TAG = "Pickaxe Boomerang";
    [SerializeField] private string _enemyName;
    [SerializeField] private float _health;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _movementSpeed;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private AISensor _aiSensor;
    [SerializeField] private Transform _targetTransform;
    #endregion

    #region Unity Methods
    private void Awake() {
        _enemyName = _enemyData.enemyName;
        _health = _enemyData.maxHealth;
        _attackDamage = _enemyData.attackDamage;
        _attackRange = _enemyData.attackRange;
        _attackSpeed = _enemyData.attackSpeed;
        _movementSpeed = _enemyData.movementSpeed;
    }

    private void Start() {
        _aiSensor.OnTargetDetected += Enemy_OnTargetDetected;
        _aiSensor.OnTargetLost += Enemy_OnTargetLost;
    }

    private void Update() {
        MoveTowardsTarget();
    }
    #endregion

    #region Other Methods

    private void Enemy_OnTargetLost(object sender, EventArgs e) {
        _enemyState = EnemyState.Idle;
    }

    private void Enemy_OnTargetDetected(object sender, AISensor.OnTargetDetectedEventArgs e) {
        _targetTransform = e.targetColliderArgs.transform;
        _enemyState = EnemyState.Chase;
    }
    private void MoveTowardsTarget() {
        if (_enemyState == EnemyState.Chase)
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _movementSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(PICKAXE_BOOMERANG_TAG)) {
            Destroy(gameObject);
        }
    }
    #endregion
}
