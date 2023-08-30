using UnityEngine;

namespace NikolayTabalyov {
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent {
    
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        [SerializeField] private Transform _counterTopPoint;
        private KitchenObject _kitchenObject;

        //[Header("Variables")]

        #region Unity Methods
       
        #endregion

        #region Other Methods
        public void Interact(Player player) {
            if (_kitchenObject is null) {
                Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetNewKitchenObjectParent(this);
            } else {
                _kitchenObject.SetNewKitchenObjectParent(player);
            }
        }

        public Transform GetNewKitchenObjectParentPoint() => _counterTopPoint;
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject is not null;
        #endregion
    }
}
