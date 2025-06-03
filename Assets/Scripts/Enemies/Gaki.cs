using UnityEngine;

public class Gaki : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 100;
    [SerializeField] FloatingHealthbar healthBar;
    public float jumpForce = 12f;
    public float jumpInterval = 2.5f;
    public float jumpHorizontalForce = 6f;
    public int contactDamage = 25;
    public float knockbackForce = 8f;
    public Animator animator;

    private Rigidbody2D rb;
    private Transform player;
    private float jumpTimer = 0f;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
            healthBar.UpdateHealthBar(health, maxHealth);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        jumpTimer += Time.deltaTime;

        if (player != null && jumpTimer >= jumpInterval)
        {
            JumpTowardsPlayer();
            jumpTimer = 0f;
        }
    }

    private void JumpTowardsPlayer()
    {
        if (player == null) return;

        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Set velocity directly to ignore knockback
        rb.linearVelocity = new Vector2(direction * jumpHorizontalForce, jumpForce);

        // Play jump animation using trigger
        if (animator != null)
            animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player collision
        PlayerResources playerRes = collision.gameObject.GetComponent<PlayerResources>();
        if (playerRes != null)
        {
            playerRes.PlayerTakeDamage(contactDamage);

            Vector2 knockbackDir = (playerRes.transform.position - transform.position).normalized;
            Rigidbody2D playerRb = playerRes.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBar != null)
            healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
