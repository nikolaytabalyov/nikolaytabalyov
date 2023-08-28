using UnityEngine;

namespace NikolayTabalyov {
    public class Player : MonoBehaviour {
        
        [Header("Variables")]
        [SerializeField] private float _speed = 5f;
        private float rotationSpeed = 10f;
        private bool _isWalking;

        [Header("Components")]
        [SerializeField] private GameInputManager _gameInputManager;

        #region Unity Methods
        private void Update() {
            if (IsWalking()) {
                MovePlayer();
                RotatePlayer();
            }
                
        }
        #endregion

        #region Movement Methods
        
        private void MovePlayer() {
            transform.position += GetDirection() * _speed * Time.deltaTime;
        }
        private void RotatePlayer() {
            transform.forward = Vector3.Slerp(transform.forward, GetDirection(), rotationSpeed * Time.deltaTime);
        }
        #endregion

        #region Return Value Methods

        public bool IsWalking() {
            this._isWalking = _gameInputManager.GetInputVectorNormalized() != Vector2.zero;
            return _isWalking;
        }

        private Vector3 GetDirection() {
            Vector2 input = _gameInputManager.GetInputVectorNormalized();
            return new Vector3(input.x, 0, input.y);
        }
        #endregion
    }
}
