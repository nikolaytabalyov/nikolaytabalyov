using UnityEngine;
using UnityEngine.UI;

namespace NikolayTabalyov {
    public class PlateIconSingleUI : MonoBehaviour {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private Image _iconImage;
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        public void SetIconImageFromKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
            _iconImage.sprite = kitchenObjectSO.sprite;
        }
        #endregion
    }
}
