using UnityEngine;

namespace NikolayTabalyov {
    public class Player : MonoBehaviour {
        
        [Header("Variables")]
        private float PLAYER_RADIUS = 0.7f;
        private float PLAYER_HEIGHT = 2f;
        [SerializeField] private float _speed = 5f;
        private float rotationSpeed = 10f;
        private bool _isWalking;

        [Header("Components")]
        [SerializeField] private GameInputManager _gameInputManager;

        #region Unity Methods
        private void Update() {
            if (IsWalking()) {
                HandlePlayerMovement();
                RotatePlayer();
            }
                
        }
        #endregion

        #region Movement Methods
        
        private void HandlePlayerMovement() {
            float moveDistance = _speed * Time.deltaTime;
            if (!DoCapsuleCast(GetDirection(), moveDistance)) {
                transform.position += GetDirection() * moveDistance;
            } else if (!DoCapsuleCast(GetDirection('x'), moveDistance)) {
                transform.position += GetDirection('x') * moveDistance;
            } else if (!DoCapsuleCast(GetDirection('z'), moveDistance)) {
                transform.position += GetDirection('z') * moveDistance;
            }
        }
        private void RotatePlayer() {
            transform.forward = Vector3.Slerp(transform.forward, GetDirection(), rotationSpeed * Time.deltaTime);
        }
        #endregion

        #region Return Value Methods
        private bool DoCapsuleCast(Vector3 direction, float moveDistance) {
            return Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PLAYER_HEIGHT, PLAYER_RADIUS, direction, moveDistance);
        }

        public bool IsWalking() {
            this._isWalking = _gameInputManager.GetInputVectorNormalized() != Vector2.zero;
            return _isWalking;
        }

        private Vector3 GetDirection() {
            Vector2 input = _gameInputManager.GetInputVectorNormalized();
            return new Vector3(input.x, 0, input.y);
        }
        private Vector3 GetDirection(char axis) {
            Vector2 input = _gameInputManager.GetInputVectorNormalized();
            if (axis == 'x') {
                return new Vector3(input.x, 0, 0).normalized;
            } else if (axis == 'z') {
                return new Vector3(0, 0, input.y).normalized;
            } else {
                return Vector3.zero;
                    
            }
        }
        
        #endregion
    }
}
