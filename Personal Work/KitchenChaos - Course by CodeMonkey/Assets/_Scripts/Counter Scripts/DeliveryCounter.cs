using UnityEngine;

namespace NikolayTabalyov
{
    public class DeliveryCounter : BaseCounter {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        //[Header("Components")]
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        public override void Interact(Player player) {
            if (player.HasKitchenObject()) { // if player is holding something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateOnPlayer)) { // if player is holding a plate
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
        #endregion
    }
}
