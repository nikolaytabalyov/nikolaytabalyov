using UnityEngine;

namespace NikolayTabalyov
{
    public class KitchenObject : MonoBehaviour {
        
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        private ClearCounter _clearCounter;
        public KitchenObjectSO GetKitchenObjectSOName => _kitchenObjectSO;


        public void SetNewClearCounter(ClearCounter clearCounter) {
            if (_clearCounter is not null) {
                Debug.Log("Clearing counter");
                _clearCounter.ClearKitchenObject();
            }
            _clearCounter = clearCounter;

            if (_clearCounter.HasKitchenObject()) {
                Debug.LogError("ClearCounter already has a KitchenObject");
            } 
            _clearCounter.SetKitchenObject(this);

            transform.parent = _clearCounter.GetNewCounterTopPoint();
            transform.localPosition = Vector3.zero;            
        }

        public ClearCounter GetClearCounter() {
            return _clearCounter;
        }
    }
}
