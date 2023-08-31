using UnityEngine;

namespace NikolayTabalyov
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    
        #region Variables
        //[Header("Variables")]
        #endregion
    
        #region Components
        [Header("Components")]
        [SerializeField] private Transform _counterTopPoint;
        private KitchenObject _kitchenObject;
        #endregion
    
        #region Other Methods
        public virtual void Interact(Player player) {
            Debug.LogError("Interacting with base counter");
        }
        
        public virtual void InteractAlternate(Player player) {
            //Debug.LogError("Interacting with base counter"); commented out because stove doesn't have an alternate interaction
        }

        public Transform GetNewKitchenObjectParentPoint() => _counterTopPoint;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject is not null;
        #endregion
    }
}
