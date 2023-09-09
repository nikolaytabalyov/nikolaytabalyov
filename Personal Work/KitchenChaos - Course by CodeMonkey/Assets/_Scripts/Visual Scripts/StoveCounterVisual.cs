using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class StoveCounterVisual : MonoBehaviour {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private StoveCounter _stoveCounter;
        [SerializeField] private GameObject _stoveOnVisual;
        [SerializeField] private GameObject _sizzlingParticles;
        #endregion
    
        #region Unity Methods
        private void Start() {
            _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

        private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
            bool showVisuals = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
            _stoveOnVisual.SetActive(showVisuals);
            _sizzlingParticles.SetActive(showVisuals);
        }
        #endregion

        #region Other Methods

        #endregion
    }
}
