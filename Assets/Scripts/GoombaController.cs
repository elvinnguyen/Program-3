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
        } else if (collision.gameObject.CompareTag("Mario"))
        {
            // Determine where Mario hit the Goomba
            ContactPoint2D contact = collision.contacts[0];
            bool hitFromAbove = contact.normal.y < -0.5f;

            if (!hitFromAbove)
            {
                // Mario hit from side or below → die
                marioDeathAudioSource.Play();
                collision.gameObject.GetComponent<PlayerController>()?.Die();
                GameManager.instance.GameOver();
            }
            // If hitFromAbove == true → handled by DeathCheck script, so do nothing
        }
    }
}



