using UnityEngine;

namespace NikolayTabalyov
{
    public class KitchenObject : MonoBehaviour {
        
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        public ClearCounter ClearCounter { get; set; }
        public KitchenObjectSO GetKitchenObjectSOName => _kitchenObjectSO;
    }
}
