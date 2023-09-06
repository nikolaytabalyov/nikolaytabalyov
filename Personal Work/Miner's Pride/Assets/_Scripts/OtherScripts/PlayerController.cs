
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private float _primaryAttackRange;
    [SerializeField] private float _secondaryAttackRange;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldown = 1f;
    private bool _isDashing = false;
    private bool _canDash = true;

    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private PlayerDataSO _playerData;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _pickaxeBoomerangPrefab;
    private PickaxeBoomerang _pickaxeBoomerangScript;
    private Rigidbody2D _rb;
    private SpawnManager _spawnManager;
    #endregion

    #region Unity Methods
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _pickaxeBoomerangScript = _pickaxeBoomerangPrefab.GetComponent<PickaxeBoomerang>();
        _health = _playerData.maxHealth;
        _primaryAttackDamage = _playerData.primaryAttackDamage;
        _secondaryAttackDamage = _playerData.secondaryAttackDamage;
    }
    
    private void Start() {
        _spawnManager = SpawnManager.Instance;
        _spawnManager.OnWaveStateChanged += PlayerController_OnWaveStateChanged;    
    }

    private void PlayerController_OnWaveStateChanged(object sender, SpawnManager.OnWaveOverEventArgs e) {
        if (e.newWaveState == SpawnManager.WaveState.Running) {
            _throwState = ThrowState.CanThrow;
        }
    }

    private void Update() {
        _movement = GetNormalizedMovementInput();
        HandleAttack();
        HandleDash();
        if (_dashCooldown >= 0 && _dashCooldown < 1f) {
            _dashCooldown += Time.deltaTime;
            _isDashing = false;
        } else if (_dashCooldown >= 1f) {
            _canDash = true;
        }
    }

    private void FixedUpdate() {
       if (!_isDashing && _dashCooldown >= 0.2f) {
            _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);
        }
    }
    #endregion

    #region Other Methods
    private void RangedAttack() {
        if (_throwState == ThrowState.CanThrow) {
            _pickaxeBoomerangScript.MaxDistance = _primaryAttackRange;
            Instantiate(_pickaxeBoomerangPrefab, _attackPoint.position, _attackPoint.rotation);
            _throwState = ThrowState.CannotThrow;
        }
    }
    private void SecondaryAttack() {
        if (_throwState == ThrowState.CanThrow) {
            _pickaxeBoomerangScript.MaxDistance = _secondaryAttackRange;
            Instantiate(_pickaxeBoomerangPrefab, _attackPoint.position, _attackPoint.rotation * Quaternion.Euler(0f, 0f, 25f));
            Instantiate(_pickaxeBoomerangPrefab, _attackPoint.position, _attackPoint.rotation * Quaternion.Euler(0f, 0f, -25f));
            _throwState = ThrowState.CannotThrow;
        }
    }
    private void HandleDash() {
        if (Input.GetKeyDown(KeyCode.Space) && _canDash) {
            _isDashing = true;
            _dashCooldown = 0f;
            _rb.velocity = _movement * _dashSpeed;
            _canDash = false;
        } else if (Input.GetKeyUp(KeyCode.Space) || !_canDash) {
            //_isDashing = false;
        }
    }
    private void HandleAttack() {
        if (Input.GetMouseButtonDown(0)) {
            RangedAttack();
        } else if (Input.GetMouseButtonDown(1)) {
            SecondaryAttack();
        }
    }
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
    #endregion
}
