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
        [SerializeField] private GameObject _hasProgressGameObject; 
        private IHasProgress _hasProgress;    
        #endregion
    
        #region Unity Methods
        private void Start() {
            _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>(); 
            if (_hasProgress is null) 
                Debug.LogError("No IHasProgress component found on " + _hasProgressGameObject);

            _hasProgress.OnProgressChanged += IHasProgress_OnCuttingProgressChanged;
            _progressBarImage.fillAmount = _emptyProgressAmount;
            Hide(); 
        }

        private void IHasProgress_OnCuttingProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
            _progressBarImage.fillAmount = e.progressNormalized; 

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
