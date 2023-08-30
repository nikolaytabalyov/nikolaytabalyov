using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class ContainerCounterVisual : MonoBehaviour {
    
        #region Variables
        [Header("Variables")]
        private const string OPEN_CLOSE_TRIGGER = "OpenClose";
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private ContainerCounter _containerCounter;
        private Animator _animator;
        #endregion
    
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            _containerCounter.OnContainerCounterOpened += ContainerCounter_OnContainerCounterOpened;
        }

        private void ContainerCounter_OnContainerCounterOpened(object sender, EventArgs e) {
            _animator.SetTrigger(OPEN_CLOSE_TRIGGER);
        }
        #endregion

        #region Other Methods

        #endregion
    }
}
