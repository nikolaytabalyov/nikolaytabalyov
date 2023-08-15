using UnityEngine;

namespace NikolayTabalyov {
    public class Target : MonoBehaviour {
        
        private Rigidbody targetRb;
        private float _xRange = 4;
        private float _minSpeed = 12;
        private float _maxSpeed = 16;
        private float _torqueRange = 10;
        private float _ySpawnPosition = -6;

        private void Start() {
            targetRb = GetComponent<Rigidbody>();
            LaunchProp();
        }

        private void LaunchProp() {
            transform.position = GenerateRandomSpawnPosition();
            targetRb.AddForce(GenerateRandomForce(), ForceMode.Impulse);
            targetRb.AddTorque(GenerateRandomTorque(), GenerateRandomTorque(), GenerateRandomTorque(), ForceMode.Impulse);
        }

        #region Generate Random Values
        private Vector3 GenerateRandomSpawnPosition() {
            return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPosition);
        }
        private Vector3 GenerateRandomForce() {
            return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
        }
        private float GenerateRandomTorque() {
            return Random.Range(-_torqueRange, _torqueRange);
        }  
        #endregion 
    }
}
