using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerResources playerResources;
    public Collider2D playerCollider;
    public Rigidbody2D playerRigidbody;
    public float respawnDelay = 2f;
    public Vector3 respawnPosition; // Set this to your map's starting location

    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (playerResources == null) playerResources = GetComponent<PlayerResources>();
        if (playerCollider == null) playerCollider = GetComponent<Collider2D>();
        if (playerRigidbody == null) playerRigidbody = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position; // Set initial spawn point
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(DeathAndRespawnRoutine());
    }

    private IEnumerator DeathAndRespawnRoutine()
    {
        // Trigger Die animation
        if (animator != null)
            animator.SetTrigger("Die");

        // Disable movement and interaction
        if (playerMovement != null)
            playerMovement.enabled = false;
        if (playerResources != null)
            playerResources.enabled = false;

        // Disable collider while dead
        if (playerCollider != null)
            playerCollider.enabled = false;

        // Stop damage flash effect on death
        if (playerResources != null && playerResources.damageImage != null)
            playerResources.damageImage.SetActive(false);

        // Freeze Rigidbody2D
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.bodyType = RigidbodyType2D.Static;
        }

        // Wait for death animation and delay
        yield return new WaitForSeconds(respawnDelay);

        // Respawn at starting location
        transform.position = respawnPosition;

        // Reset health and other resources
        if (playerResources != null)
        {
            playerResources.health = playerResources.maxHealth;
            playerResources.healthBar.SetHealth(playerResources.maxHealth);
            playerResources.enabled = true;
        }

        // Enable movement and interaction
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Unfreeze Rigidbody2D
        if (playerRigidbody != null)
            playerRigidbody.bodyType = RigidbodyType2D.Dynamic;

        // Re-enable collider after respawn
        if (playerCollider != null)
            playerCollider.enabled = true;

        // --- Animation State Reset ---
        if (animator != null)
        {
            animator.ResetTrigger("Die");
            animator.Play("Player_Idle", 0, 0f); // Replace with your actual idle state name if different
            animator.SetTrigger("Respawn");
        }

        // Do NOT re-enable damageImage here; let PlayerTakeDamage handle it
    }
}
