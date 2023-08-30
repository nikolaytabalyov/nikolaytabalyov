using System;
using UnityEngine;

namespace NikolayTabalyov
{
    public class ContainerCounter : BaseCounter {

        public event EventHandler OnContainerCounterOpened;
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        #endregion
    
        #region Unity Methods
    
        #endregion
        private void Awake() {
            
        }
        #region Other Methods
        
        public override void Interact(Player player) {
            if (!player.HasKitchenObject()) { // if player is not holding anything
                Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetNewKitchenObjectParent(player);
                OnContainerCounterOpened?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
