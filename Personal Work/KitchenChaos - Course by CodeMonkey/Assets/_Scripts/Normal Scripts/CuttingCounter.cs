using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NikolayTabalyov {
    public class CuttingCounter : BaseCounter {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        public override void Interact(Player player) {
            if (!HasKitchenObject()) { // if counter is empty
                if (player.HasKitchenObject() && HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO)) // if player is holding something and it can be cut
                    player.GetKitchenObject().SetNewKitchenObjectParent(this);
            } else if (!player.HasKitchenObject()){ // if counter is not empty and player is not holding anything
                GetKitchenObject().SetNewKitchenObjectParent(player);
            }
        }

        public override void InteractAlternate(Player player) {
            if (HasKitchenObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO)) {
                KitchenObjectSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO;
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(GetOutputFromInput(kitchenObjectSO), this);
            }
        }

        private bool HasCuttingRecipe(KitchenObjectSO input) {
            foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipeSOArray) {
                if (cuttingRecipeSO.input == input) {
                    return true;
                }
            }
            return false;
        }

        private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input) {
            foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipeSOArray) {
                if (cuttingRecipeSO.input == input) {
                    return cuttingRecipeSO.output;
                }
            }
            return null;
        }
        #endregion
    }
}
