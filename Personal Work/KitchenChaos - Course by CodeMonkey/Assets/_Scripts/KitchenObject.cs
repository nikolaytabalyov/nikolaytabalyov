using UnityEngine;

namespace NikolayTabalyov
{
    public class KitchenObject : MonoBehaviour {
        
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;

        public KitchenObjectSO GetKitchenObjectSOName => _kitchenObjectSO;
    }
}
