using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State{
        InAir,
        OnGround
    }
    
    [SerializeField] private float decelerationSpeed;
    [SerializeField] private State jumpState = State.OnGround;
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float jumpForce;
    private bool _jumpPressed = false;
    private float _horizontalInput;
    private Rigidbody2D _playerRb;

    private void Start() {
        _playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            _jumpPressed = true;
        if (Input.GetKeyUp(KeyCode.Space))
            _jumpPressed = false;
        _horizontalInput = Input.GetAxis("Horizontal");        
    }

    private void FixedUpdate() {
        if (_jumpPressed && jumpState == State.OnGround) 
            Jump();
        MoveLeftOrRight();
    }

    private void MoveLeftOrRight() {
        if (_horizontalInput == 1 || _horizontalInput == -1) {
            _playerRb.AddForce(Vector2.right * speed * _horizontalInput, ForceMode2D.Impulse);
        } 
        else if (_horizontalInput == 0) {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x * decelerationSpeed, _playerRb.velocity.y);
        }

    }

    private void Jump() {
        _playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpState = State.InAir;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) 
            jumpState = State.OnGround;
    }
}
