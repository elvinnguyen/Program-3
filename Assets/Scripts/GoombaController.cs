using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    private float horizontal; 
    private float speed = 2f; // Speed of goomba
    private float direction = -1f; // Set direction 
    Collider2D coll; // To keep the collider object
    Vector2 initialPosition;
    [SerializeField] private Rigidbody2D rb; // Initialize rigidbody2d
    [SerializeField] private SpriteRenderer sr; // Initialize sprite rendered
    [SerializeField] private Transform groundCheck; // Initialize ground check
    [SerializeField] private LayerMask groundLayer; // Initialize ground layer

    void Start()
    {
        // Get rb 
        rb = GetComponent<Rigidbody2D>();

        // Get the player collider
        coll = GetComponent<Collider2D>();

        // Get sr
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sr.flipX = horizontal < 0f;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    // Check if Goomba is on ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Detect collision 
    private void OnCollisionEnter2D(Collision2D collider)
    {
        // Switch directions upon collision
        if (collider.gameObject.tag == "Obstacle")
        {
            direction *= -1;
        }
    }
}
