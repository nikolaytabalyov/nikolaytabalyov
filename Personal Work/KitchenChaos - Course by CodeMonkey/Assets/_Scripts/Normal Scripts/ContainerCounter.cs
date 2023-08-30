using UnityEngine;

namespace NikolayTabalyov
{
    public class ContainerCounter : BaseCounter {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        #endregion
    
        #region Unity Methods
    
        #endregion
    
        #region Other Methods
        
        public override void Interact(Player player) {
            if (!HasKitchenObject()) {
                Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetNewKitchenObjectParent(player);
            }
        }
        #endregion
    }
}
