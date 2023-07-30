using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private variables
    private float speed = 10.0f;
    private float turnSpeed = 35.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //getting input
        float input = Input.GetAxis("Vertical");
        float horizontal_input = Input.GetAxis("Horizontal");

        //moving
        transform.Translate(UnityEngine.Vector3.forward * Time.deltaTime * speed * input);
        transform.Rotate(UnityEngine.Vector3.up * Time.deltaTime * turnSpeed * horizontal_input);
    }
}
