using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal; 
    private float walkSpeed = 30f; // Mario walk speed
    private float jumpSpeed = 20f; // Mario jump speed
    private bool isFacingRight = true; // Boolean for facing right

    [SerializeField] private Rigidbody2D rb; // Initialize rigidbody2d
    [SerializeField] private SpriteRenderer sr; // Initialize sprite rendered
    [SerializeField] private Transform groundCheck; // Initialize ground check
    [SerializeField] private LayerMask groundLayer; // Initialize ground layer
    
    void Start()
    {
        // Get rb 
        rb = GetComponent<Rigidbody2D>();

        // Get sr
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        sr.flipX = horizontal < 0f;

        // Jump if on the ground
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        // Hold jump to jump higher; tap to jump lower
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * walkSpeed, rb.linearVelocity.y);
    }

    // Check if Mario is on ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Flip Mario to face left or face right
    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        // End game when colliding with enemy
        if (collider.gameObject.tag == "Enemy")
        {
            // Game over
            print("Game Over");
        }
    }
}
