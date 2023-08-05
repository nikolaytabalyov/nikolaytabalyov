using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // variables
    private float speed = 15;
    private float leftBorder = -15;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        DestroyOutOfBounds();
    }

    void DestroyOutOfBounds()
    {
        if (transform.position.x < leftBorder && gameObject.CompareTag("Obstacle"))
            Destroy(gameObject);
    }
}
