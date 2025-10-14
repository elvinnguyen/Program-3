using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] float bounceSpeed = 30f;
    public AudioSource marioDeathAudioSource; // Mario death audio source

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*
        if (!CompareTag("EnemySide"))
        {
            return;
        }

        if (!collider.CompareTag("Mario"))
        {
            return;
        }

        var rb = collider.attachedRigidbody;

        bool collide = rb && rb.linearVelocity.y < 0f;

        if (!collide)
        {
            var player = collider.GetComponent<PlayerController>();

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceSpeed);
            marioDeathAudioSource.Play();

            player?.Die();
            GameManager.instance.GameOver();
        }
        */
        /*
        if (collider.CompareTag("Mario"))
        {
            var rb = collider.attachedRigidbody;

            // detect if Mario was falling (stomp)
            if (rb != null && rb.linearVelocity.y < 0f)
            {
                return;
            }

            // side hit → die immediately
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceSpeed);
            marioDeathAudioSource.Play();
            collider.GetComponent<PlayerController>()?.Die();
        }
        */

    }
}
