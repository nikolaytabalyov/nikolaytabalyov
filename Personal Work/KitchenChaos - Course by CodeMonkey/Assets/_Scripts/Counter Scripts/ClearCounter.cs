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
                    if (IsPlayerHoldingPlate(player)) { 
                        TryAddIngredientToPlate(player);
                    }
                    break;
            }
        }
        

        private bool IsPlayerHoldingPlate(Player player) {
            return player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject;
        }

        private bool TryAddIngredientToPlate(Player player) {
            PlateKitchenObject plate = player.GetKitchenObject() as PlateKitchenObject;
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
