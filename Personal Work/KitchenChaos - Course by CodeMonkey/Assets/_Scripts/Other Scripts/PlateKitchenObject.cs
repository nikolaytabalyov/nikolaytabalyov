using System.Collections.Generic;
using UnityEngine;

namespace NikolayTabalyov
{
    public class PlateKitchenObject : KitchenObject {
    
        #region Variables
        [Header("Variables")]
        private List<KitchenObjectSO> _ingredientsSOList;
        #endregion
    
        #region Components
        //[Header("Components")]
        #endregion
    
        #region Unity Methods
        private void Awake() {
            _ingredientsSOList = new();
        }
        #endregion
    
        #region Other Methods
        public bool TryAddIngredient(KitchenObjectSO ingredient) {
            if (_ingredientsSOList.Contains(ingredient)) { // List already contains this ingredient
                return false;
            } else { // List doesn't contain this ingredient
                _ingredientsSOList.Add(ingredient);
                return true;
            }
        }
        #endregion
    }
}
