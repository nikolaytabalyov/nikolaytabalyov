using UnityEngine;

public class PickaxeBoomerang : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _maxDistance = 10f;
    private Vector3 _startPosition;
    private bool _isReturning = false;
    private Vector3 _targetPosition;
    private float _step;
    #endregion
    
    #region Components
    [Header("Components")]
    private Transform _player;
    #endregion

    #region Unity Methods
    private void Start() {
        _startPosition = transform.localPosition;
        _targetPosition = _startPosition + transform.up * _maxDistance;
        _player = GameObject.Find("Player").transform;
    }

    private void Update() {
        RotatePickaxeBoomerang();

        if (!_isReturning)
            LauchPickaxeBoomerang();
        else
            ReturnPickaxeBoomerang();
    }
    #endregion
    
    #region Other Methods
    private void LauchPickaxeBoomerang() {
        if (Vector3.Distance(transform.position, _targetPosition) > 0.1f) {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);
            _isReturning = false;
        } else {
            _isReturning = true;
        }
    }

    private void ReturnPickaxeBoomerang() {
        if (_isReturning) {
            _targetPosition = _player.position;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _returnSpeed * Time.deltaTime);
        }    
    }
    
    private void RotatePickaxeBoomerang() {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Wall")) {
            _isReturning = true;
        }
    }

    #endregion
}
