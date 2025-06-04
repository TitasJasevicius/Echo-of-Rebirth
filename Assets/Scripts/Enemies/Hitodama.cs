using UnityEngine;

public class Hitodama : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 10;
    public Animator animator;

    public float floatAmplitude = 0.2f;
    public float floatFrequency = 1.0f;

    private Vector3 startPosition;
    private bool isExploding = false;
    private bool isDead = false;
    public float explosionDelay = 0.3f;
    public int explosionDamage = 50;

    private Coroutine explodeCoroutine;

    // Track if player is still in trigger
    private bool playerInTrigger = false;
    private PlayerResources playerInRange = null;

    void Start()
    {
        health = maxHealth;
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            if (explodeCoroutine != null)
            {
                StopCoroutine(explodeCoroutine);
            }
            Disappear();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isExploding || isDead) return;

        PlayerResources player = other.GetComponent<PlayerResources>();
        if (player != null)
        {
            playerInTrigger = true;
            playerInRange = player;
            explodeCoroutine = StartCoroutine(ExplodeAfterDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerResources player = other.GetComponent<PlayerResources>();
        if (player != null && player == playerInRange)
        {
            playerInTrigger = false;
            playerInRange = null;
        }
    }

    private System.Collections.IEnumerator ExplodeAfterDelay()
    {
        isExploding = true;
        yield return new WaitForSeconds(explosionDelay);

        if (isDead) yield break;

        Explode();
    }

    private void Explode()
    {
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        // Only damage player if still in trigger
        if (playerInTrigger && playerInRange != null)
        {
            playerInRange.PlayerTakeDamage(explosionDamage);
        }

        Invoke(nameof(DestroyObject), 0.8f);
    }

    private void Disappear()
    {
        isDead = true;
        Destroy(gameObject);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
