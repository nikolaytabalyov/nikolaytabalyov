using UnityEngine;

namespace NikolayTabalyov {
    public class PlayerAnimator : MonoBehaviour {
    
        [Header("Variables")]
        private const string IS_WALKING = "IsWalking";
        private bool _isWalking;
        
        [Header("Components")]
        [SerializeField] private Player _player;
        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }
        private void Update() {
            _animator.SetBool(IS_WALKING, _player.IsWalking());
        }
    }
}
