using UnityEngine;

namespace NikolayTabalyov {
    public interface IKitchenObjectParent {
        
        #region Methods
        public Transform GetNewKitchenObjectParentPoint(); 
        public void SetKitchenObject(KitchenObject kitchenObject);
        public KitchenObject GetKitchenObject(); 
        public void ClearKitchenObject(); 
        public bool HasKitchenObject();
        
        #endregion
        
    }
}