using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NikolayTabalyov
{
    public class GameInputManager : MonoBehaviour {

        public event EventHandler OnInteract;
        private PlayerInputActions _playerInputActions;

        private void Awake() {
            _playerInputActions = new();
            _playerInputActions.Enable();
            _playerInputActions.Player.Interact.performed += InteractPerformed;
        }

        private void InteractPerformed(InputAction.CallbackContext context) {
            OnInteract?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetInputVectorNormalized() {
            Vector2 input = _playerInputActions.Player.Move.ReadValue<Vector2>(); 
            return input.normalized;
        }
    }
}
