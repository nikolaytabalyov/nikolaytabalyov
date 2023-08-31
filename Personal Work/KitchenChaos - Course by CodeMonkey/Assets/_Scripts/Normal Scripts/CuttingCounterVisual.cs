using System;
using UnityEngine;

namespace NikolayTabalyov {
    public class CuttingCounterVisual : MonoBehaviour {
    
        #region Variables
        [Header("Variables")]
        private const string CUT_TRIGGER = "Cut";
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private CuttingCounter _cuttingCounter;
        private Animator _animator;
        #endregion
    
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            _cuttingCounter.OnCut += CuttingCounter_OnCut;
        }

        private void CuttingCounter_OnCut(object sender, EventArgs e) {
            _animator.SetTrigger(CUT_TRIGGER);
        }
        #endregion

        #region Other Methods

        #endregion
    }
}
