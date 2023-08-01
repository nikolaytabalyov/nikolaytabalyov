using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    private float speed = 12.5f;
    private float xRange = 20;
    private float zRange = 15;
    private float hInput;
    private float vInput;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInputAndMove();

        KeepPlayerInbounds();

        ShootProjectiles();
    }

    void KeepPlayerInbounds()
    {
        // stop at left border
        if (transform.position.x < -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        // stop at right border
        else if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        // stop at top border
        if (transform.position.z > zRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        // stop at bottom border
        else if (transform.position.z < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void GetInputAndMove()
    {
        // get input
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        // move the player
        transform.Translate(Vector3.right * speed * Time.deltaTime * hInput);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * vInput);
    }

    void ShootProjectiles()
    {
        //fire projectile on key input
        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(projectilePrefab, transform.position + new Vector3(0, 0, 2), projectilePrefab.transform.rotation);
    }
}
