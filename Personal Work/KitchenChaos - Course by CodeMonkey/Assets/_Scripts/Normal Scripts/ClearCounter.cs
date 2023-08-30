using UnityEngine;

namespace NikolayTabalyov {
    public class ClearCounter : BaseCounter {
    
        //[Header("Components")]

        //[Header("Variables")]

        #region Unity Methods
       
        #endregion

        #region Other Methods
        public override void Interact(Player player) {
            if (!HasKitchenObject()) { // if counter is empty
                if (player.HasKitchenObject())
                    player.GetKitchenObject().SetNewKitchenObjectParent(this);
            } else if (!player.HasKitchenObject()){ // if counter is not empty and player is not holding anything
                GetKitchenObject().SetNewKitchenObjectParent(player);
            }
        }

        
        #endregion
    }
}
