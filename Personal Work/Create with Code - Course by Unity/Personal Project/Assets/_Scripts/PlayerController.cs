using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    // variables
    public float speed = 10;
    public float jumpForce = 100;
    private bool onGround = true;
    public float hInput;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        //playerRb.velocity = new Vector2(speed * hInput * Time.deltaTime, playerRb.velocity.y);

        playerRb.AddForce(Vector2.right * speed * hInput, ForceMode2D.Impulse);
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            onGround = true;
    }
}
