using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // audio
    public AudioSource playerAudioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    // particles
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    // animations
    private Animator playerAnimation;
    // other variables
    private Rigidbody playerRB;
    private float jumpForce = 500;
    private float gravityModifier = 1.5f;
    private bool isOnGround = true;
    public bool isInAir = false;
    public bool gameOver = false;
    public int jumpCounter = 2;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        playerAudioSource = GetComponent<AudioSource>();
        playerAnimation = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0.25) isInAir = true;
        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || (isInAir && jumpCounter == 1)) && !gameOver && jumpCounter > 0)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // animations
            playerAnimation.SetTrigger("Jump_trig");
            // particles
            dirtParticle.Stop();
            // audio
            playerAudioSource.PlayOneShot(jumpSound, 1.0f);
            jumpCounter--;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isInAir = false;
            jumpCounter = 2;
            dirtParticle.Play();
            playerAnimation.SetBool("Jump_b", false);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

            Debug.Log("Game Over");
            gameOver = true;
            // playing death animation
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
            // particles
            explosionParticle.Play();
            dirtParticle.Stop();
            // audio
            playerAudioSource.PlayOneShot(crashSound, 1.0f);
        }

    }
}
