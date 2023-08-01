using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    //variables
    private float topBound = 30.0f;
    private float lowerBound = -10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if object hits top border => Destroy, if hits bottom border => Game Over
        if (transform.position.z > topBound)
            Destroy(gameObject);
        else if (transform.position.z < lowerBound)
            Debug.Log("Game over");
    }
}
