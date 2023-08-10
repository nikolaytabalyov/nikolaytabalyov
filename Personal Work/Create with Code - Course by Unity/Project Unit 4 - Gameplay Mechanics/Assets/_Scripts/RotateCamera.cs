using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 70;
    private float hInput;
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down, hInput * rotationSpeed * Time.deltaTime);
    }
}
