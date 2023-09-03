using System;
using UnityEngine;

public class AISensor : MonoBehaviour {

    public event EventHandler OnTargetEnterAttackRange;
    public event EventHandler OnTargetExitAttackRange;
    public event EventHandler<OnTargetDetectedEventArgs> OnTargetDetected;
    public class OnTargetDetectedEventArgs : EventArgs {
        public Collider2D targetColliderArgs;
    } 
    public event EventHandler OnTargetLost;
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private float _detectRadius;
    [SerializeField] private float _attackRange;
    [SerializeField] private bool _isTargetDetected;
    [SerializeField] private bool _isTargetOutOfAttackRange = true;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    private Enemy.EnemyState _enemyState;
    #endregion

    #region Unity Methods
    private void Update() {
        _enemyState = _enemy.GetEnemyState;
        if (CheckForTargetInDetectRange(out Collider2D targetCollider)) { 
            CheckIfTargetBehindObstacle(targetCollider, out RaycastHit2D hit);
            CheckForTargetInAttackRange();  
        }
    }
    #endregion
    
    #region Other Methods
    private bool CheckForTargetInAttackRange() {
        bool isInAttackRange = Physics2D.OverlapCircle(transform.position, _attackRange, _targetLayer);
        
        if (isInAttackRange) {
            if (_isTargetOutOfAttackRange) {
                OnTargetEnterAttackRange?.Invoke(this, EventArgs.Empty);
                _isTargetOutOfAttackRange = false;
            }
            return true;
        } else {
            if (!_isTargetOutOfAttackRange) {
                OnTargetExitAttackRange?.Invoke(this, EventArgs.Empty);
                _isTargetOutOfAttackRange = true;
            }
            return false;
        }
        
    }
    
    private bool CheckForTargetInDetectRange(out Collider2D targetCollider) {
        bool isInDetectRange = Physics2D.OverlapCircle(transform.position, _detectRadius, _targetLayer);
        Collider2D targetColliderStart = Physics2D.OverlapCircle(transform.position, _detectRadius, _targetLayer);
        if (!_isTargetDetected) {
            if (isInDetectRange) {
                targetCollider = targetColliderStart;
                return true;
            } else {
                //targetCollider = null;
                targetCollider = GetComponent<Collider2D>();
                return false;
            }
        } else {
            if (targetColliderStart != null) {
                targetCollider = targetColliderStart;
                return true;
            } else {
                targetCollider = null;
                _isTargetDetected = false;
                OnTargetLost?.Invoke(this, EventArgs.Empty);
                return false;
            }
        }
    }

    private bool CheckIfTargetBehindObstacle(Collider2D targetCollider, out RaycastHit2D hit) {
        hit = Physics2D.Linecast(transform.position, targetCollider.transform.position, _obstacleLayer);
        if (hit.collider == null) { // * If there is no obstacle between the enemy and the player
                Debug.DrawLine(transform.position, targetCollider.transform.position, Color.green);
                _isTargetDetected = true;
                OnTargetDetected?.Invoke(this, new OnTargetDetectedEventArgs {targetColliderArgs = targetCollider});
                return false;
            } else { // * If there is an obstacle between the enemy and the player
                Debug.DrawLine(transform.position, hit.point, Color.red);
                _isTargetDetected = false;
                OnTargetLost?.Invoke(this, EventArgs.Empty);
                return true;
            }
    }
    void OnDrawGizmos() {
        // Display the detection radius 
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
        // Display the attack radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange); 
    }
    #endregion
}
