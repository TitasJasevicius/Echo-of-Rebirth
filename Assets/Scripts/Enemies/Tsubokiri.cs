using UnityEngine;

public class Tsubokiri : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 2000;
    [SerializeField] FloatingHealthbar healthBar;
    public int contactDamage = 25;
    public float knockbackForce = 8f;
    public Animator animator;

    [Header("Attack Timing")]
    public float attackDuration = 5.0f;   // How long the attack lasts
    public float cooldownDuration = 2.5f; // How long before next attack

    private float attackTimer = 0f;
    private bool isAttacking = false;

    private int groundLayer;
    private Rigidbody2D rb;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundLayer = LayerMask.NameToLayer("Obstacle");
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
            healthBar.UpdateHealthBar(health, maxHealth);

        attackTimer = cooldownDuration; // Start with a cooldown window
        isAttacking = false;
        SetAnimatorAttacking(false);
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (isAttacking)
        {
            if (attackTimer <= 0f)
            {
                // End attack, start cooldown
                isAttacking = false;
                attackTimer = cooldownDuration;
                SetAnimatorAttacking(false);
            }
        }
        else
        {
            if (attackTimer <= 0f)
            {
                // Start attack
                isAttacking = true;
                attackTimer = attackDuration;
                SetAnimatorAttacking(true);
                // TODO: Add projectile shooting logic here
            }
        }
    }

    private void SetAnimatorAttacking(bool value)
    {
        if (animator != null)
            animator.SetBool("Attacking", value);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        GiveGoldToPlayer(500);
        Debug.Log("Tsubokiri (boss) died");
        Destroy(gameObject);
    }

    private void GiveGoldToPlayer(int amount)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerResources playerResources = playerObj.GetComponent<PlayerResources>();
            if (playerResources != null)
            {
                playerResources.AddGold(amount);
            }
        }
    }
}
