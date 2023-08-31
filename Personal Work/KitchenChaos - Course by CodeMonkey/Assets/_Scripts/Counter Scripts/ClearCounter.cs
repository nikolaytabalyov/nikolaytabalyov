using System;
using UnityEngine;

namespace NikolayTabalyov {
    public class ClearCounter : BaseCounter {
    
        //[Header("Components")]

        //[Header("Variables")]

        #region Unity Methods
       
        #endregion

        #region Other Methods
        public override void Interact(Player player) {
            
            switch (HasKitchenObject()) {
                case false: // if counter is empty
                    if (player.HasKitchenObject()) { // if player is holding something
                        player.GetKitchenObject().SetNewKitchenObjectParent(this);
                    }
                    break;

                case true: // if counter is not empty
                    if (player.HasKitchenObject()) { // if player is holding something
                        TryAddIngredientToCounter(player);
                    } else if (!player.HasKitchenObject()) { // if counter is not empty and player is not holding anything
                        GetKitchenObject().SetNewKitchenObjectParent(player);
                    }
                    break;
            }
        }


        private bool TryAddIngredientToCounter(Player player) {
            if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateOnCounter)) { // if there is a plate on counter
                if (plateOnCounter.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO)) {;
                    player.GetKitchenObject().DestroySelf();
                    return true;
                } else {
                    return false;
                }
            } else { // if there is no plate on counter
                return false;
            }
        }
        private bool TryAddIngredientToPlate(PlateKitchenObject plate) {
            if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO)) {
                GetKitchenObject().DestroySelf();
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }
}
