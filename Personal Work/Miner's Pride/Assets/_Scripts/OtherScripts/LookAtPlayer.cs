using System;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    
    #region Variables
    [Header("Variables")]
    private Vector3 _lookDirection;
    #endregion
    
    #region Components
    [Header("Components")]
    [SerializeField] private Vector3 _playerPosition;
    [SerializeField] private AISensor _aiSensor;
    #endregion
    
    #region Unity Methods
    private void Start() {
        _aiSensor.OnTargetDetected += LookAtPlayer_OnTargetDetected;
    }

    private void LookAtPlayer_OnTargetDetected(object sender, AISensor.OnTargetDetectedEventArgs e) {
        _playerPosition =  e.targetColliderArgs.transform.position;
    }
    #endregion

    #region Other Methods
    private void Update() {
        if (_playerPosition != Vector3.zero) {
            _lookDirection = new Vector2(_playerPosition.x - transform.position.x, _playerPosition.y - transform.position.y);
            transform.up = _lookDirection;
        }
    }
    #endregion
}
