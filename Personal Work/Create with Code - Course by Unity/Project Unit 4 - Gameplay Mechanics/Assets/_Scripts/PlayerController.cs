using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public State state = State.Normal;
    private GameObject _focalPoint;
    private Rigidbody _playerRb;
    private GameObject _powerUpIndicator;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float poweredUpStrength = 15.0f;
    private float _forwardInput;

    private void Start() {
        _focalPoint = GameObject.Find("Focal Point");
        _playerRb = GetComponent<Rigidbody>();
        _powerUpIndicator = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate() {
        _playerRb.AddForce(_focalPoint.transform.forward * speed * _forwardInput);
    }

    private void Update() {
        _forwardInput = Input.GetAxis("Vertical");
        // PowerUpIndicator follows player
        _powerUpIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0); // Vector3 is offset to y axis so it doesnt float
        _powerUpIndicator.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup")) {
            state = State.PoweredUp;
            Destroy(other.gameObject);
            _powerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCountdown());
        }
    }

    IEnumerator PowerUpCountdown() {
        yield return new WaitForSeconds(7);
        state = State.Normal;
        _powerUpIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && state == State.PoweredUp) {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = (enemyRb.gameObject.transform.position - transform.position).normalized;
            enemyRb.AddForce(direction * poweredUpStrength, ForceMode.Impulse);
            Debug.Log($"Collided with {other.gameObject.name} while state = {state}");
        }
    }

    public enum State {
        Normal,
        PoweredUp
    }
}
