using UnityEngine;

namespace NikolayTabalyov
{
    public class KitchenObject : MonoBehaviour {
        
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        private IKitchenObjectParent _kitchenObjectParent;
        public KitchenObjectSO GetKitchenObjectSO => _kitchenObjectSO;


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

        public void DestroySelf() {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
            if (this is PlateKitchenObject) {
                plateKitchenObject = this as PlateKitchenObject;
                return true;
            } else {
                plateKitchenObject = null;
                return false;
            }
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetNewKitchenObjectParent(kitchenObjectParent);

            return kitchenObject;
        }
    }
}
