using UnityEngine;

namespace NikolayTabalyov
{
    public class KitchenObject : MonoBehaviour {
        
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        private IKitchenObjectParent _kitchenObjectParent;
        public KitchenObjectSO GetKitchenObjectSOName => _kitchenObjectSO;


        public void SetNewKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
            if (_kitchenObjectParent is not null) {
                _kitchenObjectParent.ClearKitchenObject();
            }
            _kitchenObjectParent = kitchenObjectParent;

            if (_kitchenObjectParent.HasKitchenObject()) {
                Debug.LogError("IKitchenObjectParent already has a KitchenObject");
            } 
            _kitchenObjectParent.SetKitchenObject(this);

            transform.parent = _kitchenObjectParent.GetNewKitchenObjectParentPoint();
            transform.localPosition = Vector3.zero;            
        }

        public IKitchenObjectParent GetKitchenObjectParent() {
            return _kitchenObjectParent;
        }
    }
}
