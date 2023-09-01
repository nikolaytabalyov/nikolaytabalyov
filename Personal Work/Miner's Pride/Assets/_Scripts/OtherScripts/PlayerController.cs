using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _health;
    [SerializeField] private float _primaryAttackDamage;
    [SerializeField] private float _secondaryAttackDamage;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private Vector2 _movement;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private PlayerDataSO _playerData;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _pickaxeBoomerangPrefab;
    private Rigidbody _rb;
    #endregion
    
    #region Unity Methods
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _health = _playerData.maxHealth;
        _primaryAttackDamage = _playerData.primaryAttackDamage;
        _secondaryAttackDamage = _playerData.secondaryAttackDamage;
    }

    private void Update() {
        _movement = GetNormalizedMovementInput();
        HandleMovement(_movement);
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    private void Attack() {
        Instantiate(_pickaxeBoomerangPrefab, _attackPoint.position, _attackPoint.rotation);
    }
    #endregion

    #region Other Methods

    private Vector3 GetNormalizedMovementInput() {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(_horizontalInput, _verticalInput, 0f).normalized;
        return movement;
    }

    private void HandleMovement(Vector3 movement) {
        if (!movement.Equals(Vector3.zero)) {
            float xVelocity = movement.x * _speed * Time.deltaTime;
            float yVelocity = movement.y * _speed * Time.deltaTime;
            transform.position += new Vector3(xVelocity, yVelocity, 0f);
        } else {
            _rb.velocity = Vector3.zero;
        }
    }
    #endregion
}
