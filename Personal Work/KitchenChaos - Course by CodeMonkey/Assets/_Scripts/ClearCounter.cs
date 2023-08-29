using UnityEngine;

namespace NikolayTabalyov
{
    public class ClearCounter : MonoBehaviour {
    
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        [SerializeField] private Transform _counterTopPoint;
        private KitchenObject _kitchenObject;
        public void Interact() {
            if (_kitchenObject is null) {
                Transform kitchenObject = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint.position, Quaternion.identity);
                _kitchenObject = kitchenObject.GetComponent<KitchenObject>();
                _kitchenObject.ClearCounter = this;
            } else {
                Debug.Log(_kitchenObject.ClearCounter);
            }
        }
    }
}
