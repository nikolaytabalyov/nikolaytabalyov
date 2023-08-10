using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public State state = State.Normal;
    private GameObject focalPoint;
    private Rigidbody playerRb;
    private float speed = 5;
    private float forwardInput;

    private void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            state = State.PoweredUp;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")) // && state == State.PoweredUp)
        {
            Debug.Log($"Collided with {other.gameObject.name} while state = {state}");
        }
    }

    public enum State
    {
        Normal,
        PoweredUp
    }
}
