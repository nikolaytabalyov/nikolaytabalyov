using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class PlateIconsUI : MonoBehaviour {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        private void Start() {
            _plateKitchenObject.OnIngredientAdded += PlateIconsUI_OnIngredientAdded;
        }

        private void PlateIconsUI_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            UpdateVisuals();
        }

        private void UpdateVisuals() {

        }
        #endregion
    }
}
