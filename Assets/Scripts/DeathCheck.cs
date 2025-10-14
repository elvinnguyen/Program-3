using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    [SerializeField] float bounceSpeed = 12f;
    public AudioSource stompAudioSource;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Mario"))
        {
            return;
        }

        var rb = collider.attachedRigidbody;


        if (rb != null && rb.linearVelocity.y >= 0f)
        {
            return;
        }

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceSpeed);
        }
        
        var enemyRoot = GetComponentInParent<GoombaController>();
        if (enemyRoot != null)
        {
            AudioSource.PlayClipAtPoint(stompAudioSource.clip, transform.position, stompAudioSource.volume);
            Destroy(enemyRoot.gameObject);        
        } else
        {
            stompAudioSource.Play();
            Destroy(transform.parent.gameObject);
        }
    }
}
