using UnityEngine;

namespace NikolayTabalyov
{
    public class GameInputManager : MonoBehaviour {

        private PlayerInputActions _playerInputActions;

        private void Awake() {
            _playerInputActions = new();
            _playerInputActions.Enable();
        }
        public Vector2 GetInputVectorNormalized() {
            Vector2 input = _playerInputActions.Player.Move.ReadValue<Vector2>(); 
            return input.normalized;
        }
    }
}
