using UnityEngine;

public class Hitodama : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 10;
    public Animator animator;

    // Floating parameters
    public float floatAmplitude = 0.2f; // How far up and down
    public float floatFrequency = 1.0f; // How fast

    private Vector3 startPosition;
    private bool isExploding = false;
    private bool isDead = false;
    public float explosionDelay = 0.3f; // Short delay before exploding
    public int explosionDamage = 50;

    private Coroutine explodeCoroutine;

    void Start()
    {
        health = maxHealth;
        startPosition = transform.position;
    }

    void Update()
    {
        // Floating effect
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            // If it's about to explode, cancel the explosion and just disappear
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
            explodeCoroutine = StartCoroutine(ExplodeAfterDelay(player));
        }
    }

    private System.Collections.IEnumerator ExplodeAfterDelay(PlayerResources player)
    {
        isExploding = true;
        yield return new WaitForSeconds(explosionDelay);

        // If killed during delay, do not explode
        if (isDead) yield break;

        Explode(player);
    }

    private void Explode(PlayerResources player)
    {
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        if (player != null)
        {
            player.PlayerTakeDamage(explosionDamage);
        }

        // Destroy after animation (match your animation length)
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
