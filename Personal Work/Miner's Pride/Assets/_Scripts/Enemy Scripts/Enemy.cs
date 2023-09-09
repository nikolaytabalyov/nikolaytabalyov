using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyType {
        Suicidal,
        Ranged1,
        Ranged2,
        Ranged3,
    }
    
    public enum EnemyState {
        Idle,
        Chase,
        Attack,
        Dead
    }

    public enum EnemySootingState {
        Shooting, 
        NotShooting
    }

    public delegate void EnemyAttackMethod();
    #region Variables
    [Header("Variables")]
    EnemyAttackMethod _enemyAttackMethod;
    [SerializeField] private EnemyState _enemyState;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemySootingState _enemyShootingState;
    public EnemyState GetEnemyState => _enemyState;
    private const string PICKAXE_BOOMERANG_TAG = "Pickaxe Boomerang";
    private string _enemyName;
    [SerializeField] private float _health;
    private float _attackDamage;
    private float _attackRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _shootingMovementSpeed;
    private float _movementSpeed;
    private bool _isTargetInAttackRange;
    private float _attackTimer;
    private float _changeDirectionTimer;
    private bool _isCollidingWithWall;
    [SerializeField] private float _idleMovementSpeed;
    private float _raycastDistance = 2.5f;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private AISensor _aiSensor;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _enemyProjectilePrefab;
    private Rigidbody2D _rb2D;
    private Collision2D _suicideAttackCollision;
    [SerializeField] private LayerMask _obstacleLayerMask;
    [SerializeField] private Transform _enemyVisual;
    #endregion

    #region Unity Methods
    private void Awake() {
        _enemyName = _enemyData.enemyName;
        _health = _enemyData.maxHealth;
        _attackDamage = _enemyData.attackDamage;
        _attackRange = _enemyData.attackRange;
        _attackSpeed = _enemyData.attackSpeed;
        _movementSpeed = _enemyData.movementSpeed;
        _shootingMovementSpeed = _enemyData.shootingMovementSpeed;
        _enemyType = _enemyData.enemyType;
        _idleMovementSpeed = _enemyData.idleMovementSpeed;
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _aiSensor.OnTargetDetected += Enemy_OnTargetDetected;
        _aiSensor.OnTargetLost += Enemy_OnTargetLost;
        _aiSensor.OnTargetEnterAttackRange += Enemy_OnTargetEnterAttackRange;
        _aiSensor.OnTargetExitAttackRange += Enemy_OnTargetExitAttackRange;
        SetEnemyAttackMethod();
        _changeDirectionTimer = 0f;
    }


    private void Update() {
        _attackTimer += Time.deltaTime;
        if (_targetTransform != null && _enemyState == EnemyState.Chase) {
            MoveTowardsTarget();
        } else if (_enemyState == EnemyState.Idle) {
            SetRandomDirection();
            Wander();
        }
        _enemyAttackMethod?.Invoke();
        
    }
    #endregion
    private void Enemy_OnTargetExitAttackRange(object sender, EventArgs e) {
        SwitchState(EnemyState.Chase);
        _isTargetInAttackRange = false;
    }

    private void Enemy_OnTargetEnterAttackRange(object sender, EventArgs e) {
        SwitchState(EnemyState.Attack);
        _isTargetInAttackRange = true;
    }

    #region Other Methods
    private void SetRandomDirection() {
        if (_changeDirectionTimer > 0f) {
            _changeDirectionTimer -= Time.deltaTime;
        }  
        if (_changeDirectionTimer <= 0f) {
            float randomDirection = UnityEngine.Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, 0f, randomDirection);
            _enemyVisual.rotation = Quaternion.identity;
            _changeDirectionTimer = UnityEngine.Random.Range(2f, 4f);
        }
    }

    private void Wander() {
        _rb2D.velocity = transform.up * _idleMovementSpeed * Time.deltaTime;
    }
    private void SetEnemyAttackMethod() {
        switch(_enemyType) {
            case EnemyType.Suicidal:
                _enemyAttackMethod = SuicideAttack;
                break;
            case EnemyType.Ranged1:
                _enemyAttackMethod = RangedAttack1;
                break;
            case EnemyType.Ranged2:
                _enemyAttackMethod = RangedAttack2;
                break;
            case EnemyType.Ranged3:
                _enemyAttackMethod = RangedAttack3;
                break;
        }
    }

    private void RangedAttack3() {
        if (_isTargetInAttackRange && _attackTimer >= _attackSpeed) {
            Instantiate(_enemyProjectilePrefab, _attackPoint.position, _attackPoint.rotation * Quaternion.Euler(0f, 0f, 10f));
            Instantiate(_enemyProjectilePrefab, _attackPoint.position, _attackPoint.rotation);
            Instantiate(_enemyProjectilePrefab, _attackPoint.position, _attackPoint.rotation * Quaternion.Euler(0f, 0f, -10f));
            _attackTimer = 0f;
        } 
        if (_isTargetInAttackRange) {
            _enemyShootingState = EnemySootingState.Shooting;
        } else {
            _enemyShootingState = EnemySootingState.NotShooting;
        } 
    }

    private void RangedAttack2() {
        if (_isTargetInAttackRange && _attackTimer >= _attackSpeed) {
            Instantiate(_enemyProjectilePrefab, _attackPoint.position, _attackPoint.rotation * Quaternion.Euler(0f, 0f, 10f));
            Instantiate(_enemyProjectilePrefab, _attackPoint.position, _attackPoint.rotation * Quaternion.Euler(0f, 0f, -10f));
            _attackTimer = 0f;
        } 
        if (_isTargetInAttackRange) {
            _enemyShootingState = EnemySootingState.Shooting;
        } else {
            _enemyShootingState = EnemySootingState.NotShooting;
        } 
    }

    private void RangedAttack1()
    {
        if (_isTargetInAttackRange && _attackTimer >= _attackSpeed) {
            Instantiate(_enemyProjectilePrefab, _attackPoint.position, _attackPoint.rotation);
            _attackTimer = 0f;
        } 
        if (_isTargetInAttackRange) {
            _enemyShootingState = EnemySootingState.Shooting;
        } else {
            _enemyShootingState = EnemySootingState.NotShooting;
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (_enemyType == EnemyType.Suicidal && other.gameObject.CompareTag("Player")) {
            _suicideAttackCollision = other;
            SuicideAttack();
        } 
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy")) {
            GetOffWallOrEnemy();
        }
    }

    private void GetOffWallOrEnemy() {
        //transform.rotation = Quaternion.Inverse(transform.rotation);
        // Cast a ray in the direction the enemy is facing
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _raycastDistance, _obstacleLayerMask);

        if (hit.collider != null) {
            // Rotate away from the wall or enemy
            transform.rotation = Quaternion.LookRotation(Vector3.forward, hit.normal);
            _enemyVisual.rotation = Quaternion.identity;
        } else {
            // Rotate 180 degrees if no obstacle is detected
            transform.rotation *= Quaternion.Euler(0, 0, 180);
            _enemyVisual.rotation = Quaternion.identity;
        }
    }

    private void SuicideAttack() {
        if (_suicideAttackCollision != null) {
            Destroy(gameObject);
        }
    }


    private void SwitchState(EnemyState enemyState) {
        switch (enemyState) {
            case EnemyState.Idle:
                _enemyState = EnemyState.Idle;
                break;
            case EnemyState.Chase:
                _enemyState = EnemyState.Chase;
                break;
            case EnemyState.Attack:
                _enemyState = EnemyState.Attack;
                break;
            case EnemyState.Dead:
                _enemyState = EnemyState.Dead;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyState), enemyState, null);
        }
    }
    private void Enemy_OnTargetLost(object sender, EventArgs e) {
        SwitchState(EnemyState.Idle);
        _isTargetInAttackRange = false;
    }

    private void Enemy_OnTargetDetected(object sender, AISensor.OnTargetDetectedEventArgs e) {
        _targetTransform = e.targetColliderArgs.transform;
        _enemyState = EnemyState.Chase;
        _isTargetInAttackRange = true;
    }
    private void MoveTowardsTarget() {
        if (_enemyState == EnemyState.Chase && _enemyShootingState == EnemySootingState.NotShooting)
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _movementSpeed * Time.deltaTime);
        else if (_enemyState == EnemyState.Chase && _enemyShootingState == EnemySootingState.Shooting)
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _shootingMovementSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(PICKAXE_BOOMERANG_TAG)) {
            Destroy(gameObject);
        }
    }
    #endregion
}
