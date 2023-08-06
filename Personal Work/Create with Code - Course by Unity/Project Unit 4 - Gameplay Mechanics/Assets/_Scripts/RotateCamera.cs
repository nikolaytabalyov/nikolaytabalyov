using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 75;
    private float hInput;
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down, hInput * rotationSpeed * Time.deltaTime);
    }
}
