using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace NikolayTabalyov
{
    public class GameManager : MonoBehaviour {
    
        [Header("Variables")]
        [SerializeField] private List<GameObject> targets = new();
        private float _spawnRate = 1.0f;

        private void Start() {
            StartCoroutine(SpawnTarget());
        }
        
        IEnumerator SpawnTarget() {
            while (true) {
                yield return new WaitForSeconds(_spawnRate);
                Instantiate(targets[Random.Range(0, targets.Count)]);
            }
        }
    }
}
