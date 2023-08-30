using UnityEngine;

namespace NikolayTabalyov {
    public class CuttingCounter : BaseCounter {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _cutKitchenObjectSO;
        #endregion
    
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

        public override void InteractAlternate(Player player) {
            if (HasKitchenObject()) {
                GetKitchenObject().DestroySelf();
                Transform kitchenObjectTransform = Instantiate(_cutKitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetNewKitchenObjectParent(this);
            }
        }
        #endregion
    }
}
