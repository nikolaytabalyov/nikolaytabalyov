using UnityEngine;

namespace NikolayTabalyov
{
    public class ClearCounter : MonoBehaviour {
    
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        [SerializeField] private Transform _counterTopPoint;
        [SerializeField] private ClearCounter _secondClearCounter;
        private KitchenObject _kitchenObject;

        [Header("Variables")]
        [SerializeField] private bool _testing;

        #region Unity Methods
        private void Update() {
            if (_testing && Input.GetKeyDown(KeyCode.Space)) {
                if (_kitchenObject is not null) {
                    _kitchenObject.SetNewClearCounter(_secondClearCounter);
                }
            }
        }
        #endregion

        #region Other Methods
        public void Interact() {
            if (_kitchenObject is null) {
                Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetNewClearCounter(this);
            } else {
                Debug.Log(_kitchenObject.GetClearCounter());
            }
        }

        public Transform GetNewCounterTopPoint() {
            return _counterTopPoint;
        }
        public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
        public KitchenObject GetKitchenObject() => _kitchenObject;
        public void ClearKitchenObject() => _kitchenObject = null;
        public bool HasKitchenObject() => _kitchenObject is not null;
        #endregion
    }
}
