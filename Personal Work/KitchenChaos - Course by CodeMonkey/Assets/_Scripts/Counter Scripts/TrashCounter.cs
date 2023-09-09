using Unity.VisualScripting;
using UnityEngine;

namespace NikolayTabalyov
{
    public class TrashCounter : BaseCounter {
    
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
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().DestroySelf();
            }
        }
        #endregion
    }
}
