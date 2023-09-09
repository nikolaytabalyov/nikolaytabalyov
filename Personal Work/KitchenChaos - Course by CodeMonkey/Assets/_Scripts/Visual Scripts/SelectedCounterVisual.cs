using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class SelectedCounterVisual : MonoBehaviour {
        
        [SerializeField] private GameObject[] _selectedCounterVisuals;
        [SerializeField] private BaseCounter _baseCounter;
        private void Start() {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
            if (e.selectedCounter == _baseCounter) {
                Show();
            } else {
                Hide();
            }
        }
        
        private void Show() {
            foreach (GameObject selectedCounterVisual in _selectedCounterVisuals) {
                selectedCounterVisual.SetActive(true);
            }
        }
        private void Hide() {
            foreach (GameObject selectedCounterVisual in _selectedCounterVisuals) {
                selectedCounterVisual.SetActive(false);
            }
        }
    }
}
