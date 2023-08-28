using UnityEngine;

namespace NikolayTabalyov {
    public class Player : MonoBehaviour {
        
        [Header("Variables")]
        [SerializeField] private float _speed = 5f;
        private void Update() {
            Vector2 input = new Vector2(0, 0);    

            if (Input.GetKey(KeyCode.W)) {
                input.y = +1;
            }
            if (Input.GetKey(KeyCode.S)) {
                input.y = -1;
            }
            if (Input.GetKey(KeyCode.A)) {
                input.x = -1;
            }
            if (Input.GetKey(KeyCode.D)) {
                input.x = +1;
            }
            input = input.normalized;

            Vector3 direction = new Vector3(input.x, 0, input.y);
            transform.position += direction * _speed * Time.deltaTime;

            float rotationSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        }
    }
}
