using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Name of next level (set in Inspector)
    [SerializeField] private AudioSource levelCompleteAudio; // Optional: assign in Inspector
    [SerializeField] private float delayBeforeNextLevel = 2f; // Wait time after flagpole

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCompleted) return; // prevent double trigger

        if (collision.CompareTag("Mario"))
        {
            levelCompleted = true;

            // Play win sound
            if (levelCompleteAudio != null)
                levelCompleteAudio.Play();

            // Stop Mario’s movement
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
                player.enabled = false;

            // Optionally: zero out velocity
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            // Trigger animation or flag lowering (optional)
            // Example: StartCoroutine(LowerFlag());

            
        }
    }
}