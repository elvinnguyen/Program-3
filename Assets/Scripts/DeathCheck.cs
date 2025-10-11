using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    [SerializeField] float bounceSpeed = 12f;

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
            Destroy(enemyRoot.gameObject);
        } else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
