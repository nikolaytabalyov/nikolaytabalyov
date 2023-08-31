using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace NikolayTabalyov
{
    public class ProgressBarUI : MonoBehaviour {
    
        #region Variables
        [Header("Variables")]
        private float _filledProgressAmount = 1f;
        private float _emptyProgressAmount = 0f;
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private CuttingCounter _cuttingCounter;    
        #endregion
    
        #region Unity Methods
        private void Start() {
            _cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;
            _progressBarImage.fillAmount = _emptyProgressAmount;
            Hide(); 
        }

        private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e) {
            _progressBarImage.fillAmount = e.cuttingProgressNormalized; 

            if (_progressBarImage.fillAmount == _filledProgressAmount || _progressBarImage.fillAmount == _emptyProgressAmount) {
                Hide();
            } else {
                Show();
            }
        }
        #endregion

        #region Other Methods
        private void Show() {
            gameObject.SetActive(true);
        }

        private void Hide() {
            gameObject.SetActive(false);
        }
        #endregion
    }
}
