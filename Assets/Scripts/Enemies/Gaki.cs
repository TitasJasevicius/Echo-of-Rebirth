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

    public float playerChaseRange = 6f; // How close player must be to chase
    public float idleJumpInterval = 3.5f;
    public float idleJumpHorizontalForce = 3f;
    public float idleJumpForce = 8f;
    public float activeRange = 20f; // How close player must be for Gaki to be active

    private Rigidbody2D rb;
    private Transform player;
    private float jumpTimer = 0f;
    private int groundLayer;
    private bool isChasing = false;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundLayer = LayerMask.NameToLayer("Obstacle");
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

        if (player != null)
        {
            float dx = player.position.x - transform.position.x;
            float distance = Mathf.Abs(dx);

            // If player is outside active range, do nothing
            if (distance > activeRange)
                return;

            // Always face the direction of movement (chasing or idle)
            int faceDir;
            if (distance <= playerChaseRange)
            {
                faceDir = dx < 0 ? -1 : 1;
            }
            else
            {
                faceDir = (int)Mathf.Sign(transform.localScale.x); // keep current facing
            }
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * faceDir;
            transform.localScale = scale;

            if (distance <= playerChaseRange)
            {
                isChasing = true;
                if (jumpTimer >= jumpInterval)
                {
                    JumpTowardsPlayer();
                    jumpTimer = 0f;
                }
            }
            else
            {
                isChasing = false;
                if (jumpTimer >= idleJumpInterval)
                {
                    JumpRandomly();
                    jumpTimer = 0f;
                }
            }
        }
    }

    private void JumpTowardsPlayer()
    {
        if (player == null) return;

        float dx = player.position.x - transform.position.x;
        int direction = dx < 0 ? -1 : 1;

        rb.linearVelocity = new Vector2(direction * jumpHorizontalForce, jumpForce);

        if (animator != null)
            animator.SetTrigger("Jump");
    }

    private void JumpRandomly()
    {
        int direction = Random.value < 0.5f ? -1 : 1;

        // Face the direction of the jump
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;

        rb.linearVelocity = new Vector2(direction * idleJumpHorizontalForce, idleJumpForce);

        if (animator != null)
            animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop all movement when landing on ground
        if (collision.gameObject.layer == groundLayer)
        {
            rb.linearVelocity = Vector2.zero;
        }

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
