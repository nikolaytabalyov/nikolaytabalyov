using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NikolayTabalyov
{
    public class GameInputManager : MonoBehaviour {

        public event EventHandler OnInteract;
        public event EventHandler OnInteractAlternate;
        private PlayerInputActions _playerInputActions;

        private void Awake() {
            _playerInputActions = new();
            _playerInputActions.Enable();
            _playerInputActions.Player.Interact.performed += InteractPerformed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
        }

        private void InteractAlternatePerformed(InputAction.CallbackContext context) {
            OnInteractAlternate?.Invoke(this, EventArgs.Empty);
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
