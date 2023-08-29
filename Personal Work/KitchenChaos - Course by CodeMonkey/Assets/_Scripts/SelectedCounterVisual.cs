using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class SelectedCounterVisual : MonoBehaviour {
        
        [SerializeField] private GameObject _selectedCounterVisual;
        [SerializeField] private ClearCounter _clearCounter;
        private void Start() {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
            if (e.selectedCounter == _clearCounter) {
                Show();
            } else {
                Hide();
            }
        }
        
        private void Show() {
            _selectedCounterVisual.SetActive(true);
        }
        private void Hide() {
            _selectedCounterVisual.SetActive(false);
        }
    }
}
