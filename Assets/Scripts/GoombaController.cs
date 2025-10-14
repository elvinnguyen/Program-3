using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    private float horizontal; 
    private float speed = 4f; // Speed of goomba
    private float direction = -1f; // Set direction 
    Collider2D coll; // To keep the collider object
    Vector2 initialPosition;
    [SerializeField] private Rigidbody2D rb; // Initialize rigidbody2d
    [SerializeField] private SpriteRenderer sr; // Initialize sprite rendered
    [SerializeField] private Transform groundCheck; // Initialize ground check
    [SerializeField] private LayerMask groundLayer; // Initialize ground layer
    [SerializeField] float bounceSpeed = 16f;
    public AudioSource marioDeathAudioSource; // Mario death audio source

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Switch directions upon collision
        if (collision.gameObject.tag == "Obstacle")
        {
            direction *= -1;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Mario"))
        {
            var rb = collider.attachedRigidbody;

            // Bounce Mario up
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceSpeed);

            // Play Mario death sound
            marioDeathAudioSource.Play();

            // Trigger Mario death
            collider.GetComponent<PlayerController>()?.Die();

            // Trigger game over
            GameManager.instance.GameOver();
        }
    }


}
