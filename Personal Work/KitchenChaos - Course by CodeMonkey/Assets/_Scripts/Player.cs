using UnityEngine;

namespace NikolayTabalyov {
    public class Player : MonoBehaviour {
        
        [Header("Variables")]
        private float PLAYER_RADIUS = 0.7f;
        private float PLAYER_HEIGHT = 2f;
        [SerializeField] private float _speed = 5f;
        private float rotationSpeed = 10f;
        private bool _isWalking;
        private Vector3 _lastInteractionDirection;


        [Header("Components")]
        [SerializeField] private GameInputManager _gameInputManager;
        

        #region Unity Methods
        private void Update() {
            if (IsWalking()) {
                HandlePlayerMovement();
                RotatePlayer();
            }
            HandleInteractions();
                
        }
        #endregion

        #region Movement Methods
        
        private void HandlePlayerMovement() {
            float moveDistance = _speed * Time.deltaTime;
            Vector3 directionX = new Vector3(GetDirection().x, 0, 0).normalized;
            Vector3 directionZ = new Vector3(0, 0, GetDirection().z).normalized;

            if (!DoCapsuleCast(GetDirection(), moveDistance)) {    // If there is no collision, move the player
                transform.position += GetDirection() * moveDistance;
            } else if (!DoCapsuleCast(directionX, moveDistance)) { // If there is a collision, check if there is a collision on the X axis 
                transform.position += directionX * moveDistance;
            } else if (!DoCapsuleCast(directionZ, moveDistance)) { // If there is a collision, check if there is a collision on the Z axis
                transform.position += directionZ * moveDistance;
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
        
        
        #endregion

        #region Other Methods
        private void HandleInteractions() {
            Vector3 direction = GetDirection();
            if (direction != Vector3.zero) {
                _lastInteractionDirection = direction;
            }
            float maxInteractableDistance = 2f;
            if (Physics.Raycast(transform.position, _lastInteractionDirection, out RaycastHit raycastHit, maxInteractableDistance)) {
                Debug.Log(raycastHit.transform);
            }

        }
        #endregion
    }
}
