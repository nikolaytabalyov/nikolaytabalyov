using UnityEngine;

namespace NikolayTabalyov
{
    public class ClearCounter : MonoBehaviour {
    
        [Header("Components")]
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        [SerializeField] private Transform _counterTopPoint;

        public void Interact() {
            Transform kitchenObject = Instantiate(_kitchenObjectSO.prefab, _counterTopPoint.position, Quaternion.identity);
            Debug.Log(kitchenObject.GetComponent<KitchenObject>().GetKitchenObjectSOName.objectName);
        }
    }
}
