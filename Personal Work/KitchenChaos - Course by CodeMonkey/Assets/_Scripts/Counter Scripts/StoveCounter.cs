using System.Collections.Generic;
using UnityEngine;

namespace NikolayTabalyov
{
    public class StoveCounter : BaseCounter {
    
        #region Variables
        [Header("Variables")]
        [SerializeField] private List<FryingRecipeSO> _fryingRecipeSOList;
        #endregion
    
        #region Components
        //[Header("Components")]
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        public override void Interact(Player player) {
            if (!HasKitchenObject()) { // if counter is empty
                if (player.HasKitchenObject() && HasFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO)) { 
                    // if player is holding something and it can be fried
                    player.GetKitchenObject().SetNewKitchenObjectParent(this);
                }
            } else if (!player.HasKitchenObject()){ // if counter is not empty and player is not holding anything
                GetKitchenObject().SetNewKitchenObjectParent(player);
            }
        }

        private bool HasFryingRecipe(KitchenObjectSO inputKitchenObjectSO) {
            return _fryingRecipeSOList.Find(fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO) is not null;  
        }   // returns true if there is a frying recipe for the input

        private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _fryingRecipeSOList.Find(fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO).output;  
        }   

        private FryingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO) {
            return _fryingRecipeSOList.Find(fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO);  
        }
        #endregion
    }
}
