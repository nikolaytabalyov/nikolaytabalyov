using System;
using UnityEngine;

namespace NikolayTabalyov {
    public class Player : MonoBehaviour, IKitchenObjectParent {

        [field: Header("Events")]
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        public class OnSelectedCounterChangedEventArgs : EventArgs {
            public BaseCounter selectedCounter;
        }

        [field: Header("Properties")]
        public static Player Instance { get; private set;}

        [Header("Variables")]
        private float PLAYER_RADIUS = 0.7f;
        private float PLAYER_HEIGHT = 2f;
        [SerializeField] private float _speed = 5f;
        private float rotationSpeed = 10f;
        private bool _isWalking;
        private Vector3 _lastInteractionDirection;

        [Header("Components")]
        [SerializeField] private GameInputManager _gameInputManager;
        [SerializeField] private LayerMask _countersLayerMask;
        [SerializeField] private Transform _playerObjectHoldPoint;
        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;


        #region Unity Methods
        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
        }

        private void Start() {
            _gameInputManager.OnInteract += GameInputManager_OnInteract; 
            _gameInputManager.OnInteractAlternate += GameInputManager_OnInteractAlternate;
        }


        private void Update() {
            if (IsWalking()) {
                HandlePlayerMovement();
                RotatePlayer();
            }
            HandleInteractions();
                
        }
        #endregion
        
        #region Event Methods
        private void GameInputManager_OnInteractAlternate(object sender, EventArgs e){
            if (_selectedCounter != null) {
                _selectedCounter.InteractAlternate(this);
            }
        }
        private void GameInputManager_OnInteract(object sender, EventArgs e) {
            if (_selectedCounter != null) {
                _selectedCounter.Interact(this);
            }
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
            if (Physics.Raycast(transform.position, _lastInteractionDirection, out RaycastHit raycastHit, maxInteractableDistance, _countersLayerMask)) {
                if (raycastHit.collider.TryGetComponent(out BaseCounter baseCounter)) {
                    SetSelectedCounter(baseCounter);
                } else if (_selectedCounter != null) {
                    SetSelectedCounter(null);
                }
            } else if (_selectedCounter != null) {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(BaseCounter baseCounter) {
            _selectedCounter = baseCounter;

            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
                selectedCounter = _selectedCounter
            });
        }
        #endregion

        public Transform GetNewKitchenObjectParentPoint() => _playerObjectHoldPoint;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject is not null;
    }
}
