using UnityEngine;

namespace NikolayTabalyov {
    public class Target : MonoBehaviour {
        
        [Header("Variables")]
        private float _xRange = 4;
        private float _minSpeed = 12;
        private float _maxSpeed = 16;
        private float _torqueRange = 10;
        private float _ySpawnPosition = -2;
        [SerializeField] private int _pointValue; 

        [Header("Components")]
        private Rigidbody targetRb;
        private GameManager _gameManager = GameManager.Instance;
        [SerializeField] private ParticleSystem _explosionParticle;

        private void Start() {
            targetRb = GetComponent<Rigidbody>();
            LaunchProp();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Sensor")) {
                Destroy(gameObject);
                if (!gameObject.CompareTag("Bad")) {
                    _gameManager.UpdateLives();
                }
            } else if (other.CompareTag("Trail") && _gameManager.GameState == GameManager.State.Running) {
                Destroy(gameObject);
                _gameManager.PlaySoundEffect(gameObject);
                Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
                _gameManager.UpdateScore(_pointValue);
            }
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
