using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal; 
    private float walkSpeed = 30f; // Mario walk speed
    private float jumpSpeed = 30f; // Mario jump speed
    private bool isFacingRight = true; // Boolean for facing right
    public AudioSource coinAudioSource; // Coin collecting audio source
    public AudioSource jumpAudioSource; // Jumping audio source
    public AudioSource marioDeathAudioSource; // Mario death audio source
    public HudManager hud; // Get HUD
    [SerializeField] private Rigidbody2D rb; // Initialize rigidbody2d
    [SerializeField] private SpriteRenderer sr; // Initialize sprite rendered
    [SerializeField] private Transform groundCheck; // Initialize ground check
    [SerializeField] private LayerMask groundLayer; // Initialize ground layer
    [SerializeField] private LayerMask deathCheck;
    [SerializeField] float deathBounceSpeed = 16f;
    
    void Start()
    {
        // Get rb 
        rb = GetComponent<Rigidbody2D>();

        // Get sr
        sr = GetComponent<SpriteRenderer>();

        // Refresh HUD
        hud.Refresh();

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        sr.flipX = horizontal < 0f;

        // Jump if on the ground
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpAudioSource.Play();
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
        return Physics2D.OverlapCircle(groundCheck.position, 1.5f, groundLayer);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // End game when colliding with enemy
        if (collision.gameObject.CompareTag("EnemySide"))
        {
            marioDeathAudioSource.Play();

            // Game over
            print("Game Over");
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.IncreaseScore(1);

            hud.Refresh();

            coinAudioSource.Play();

            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "Goal")
        {
            print("Goal");
        }
    }

    public void Die()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, deathBounceSpeed);
        DisableControl();
    }

    public void DisableControl()
    {
        enabled = false;
    }
}
