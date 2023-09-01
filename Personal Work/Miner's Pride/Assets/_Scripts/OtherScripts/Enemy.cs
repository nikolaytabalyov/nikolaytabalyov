using UnityEngine;

public class Enemy : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
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
    #endregion
    
    #region Other Methods
    
    #endregion
}
