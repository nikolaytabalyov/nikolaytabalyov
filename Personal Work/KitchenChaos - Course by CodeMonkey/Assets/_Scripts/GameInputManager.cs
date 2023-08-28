using UnityEngine;

namespace NikolayTabalyov
{
    public class GameInputManager : MonoBehaviour {

        public Vector2 GetInputVectorNormalized() {
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
            return input.normalized;
        }
    }
}
