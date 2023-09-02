
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private const string PICKAXE_BOOMERANG_TAG = "Pickaxe Boomerang";

    public enum ThrowState {
        CanThrow,
        CannotThrow
    }

    #region Variables
    [Header("Variables")]
    [SerializeField] private ThrowState _throwState;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _health;
    [SerializeField] private float _primaryAttackDamage;
    [SerializeField] private float _secondaryAttackDamage;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private Vector2 _movement;
    private bool _canMove = true;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private PlayerDataSO _playerData;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _pickaxeBoomerangPrefab;
    private Rigidbody2D _rb;
    #endregion

    #region Unity Methods
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _health = _playerData.maxHealth;
        _primaryAttackDamage = _playerData.primaryAttackDamage;
        _secondaryAttackDamage = _playerData.secondaryAttackDamage;
    }

    private void Update() {
        _movement = GetNormalizedMovementInput();
        //HandleMovement(_movement);
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    private void FixedUpdate() {
        if (_canMove) {
            _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);
        }
    }
    private void Attack() {
        if (_throwState == ThrowState.CanThrow) {
            Instantiate(_pickaxeBoomerangPrefab, _attackPoint.position, _attackPoint.rotation);
            _throwState = ThrowState.CannotThrow;
        }
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
        float xVelocity = movement.x * _speed * Time.deltaTime;
        float yVelocity = movement.y * _speed * Time.deltaTime;
        _rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(PICKAXE_BOOMERANG_TAG)) {
            Destroy(other.gameObject);
            _throwState = ThrowState.CanThrow;
        }
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.CompareTag("Wall")) {
    //         _canMove = false;
    //     }
    // }

    // private void OnCollisionStay2D(Collision2D other) {
    //     Vector3 input = GetNormalizedMovementInput();
    //     if (other.gameObject.CompareTag("Wall") || GetNormalizedMovementInput().Equals(Vector3.zero)) {
    //         _canMove = true;
    //     }
    // }
    #endregion
}
