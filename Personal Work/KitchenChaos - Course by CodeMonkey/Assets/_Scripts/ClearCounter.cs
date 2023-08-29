using UnityEngine;

namespace NikolayTabalyov
{
    public class ClearCounter : MonoBehaviour {
    
        [Header("Components")]
        [SerializeField] private Transform _tomatoPrefab;
        [SerializeField] private Transform _counterTopPoint;

        public void Interact() {
            Transform tomatoTransform = Instantiate(_tomatoPrefab, _counterTopPoint.position, Quaternion.identity);
        }
    }
}
