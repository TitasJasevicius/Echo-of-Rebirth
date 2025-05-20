using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Animator animator;
    private SpriteRenderer attackPointSpriteRenderer;

    void Awake()
    {
        if (attackPoint != null)
        {
            attackPointSpriteRenderer = attackPoint.GetComponent<SpriteRenderer>();
            attackPointSpriteRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Show the attack point sprite for the attack duration
        ShowAttackPoint();

        // Detect all enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage such enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Show the AttackPoint sprite for the attack duration
    public void ShowAttackPoint()
    {
        if (attackPointSpriteRenderer != null)
        {
            attackPointSpriteRenderer.enabled = true;
            StartCoroutine(HideAttackPointAfterDelay(0.5f / attackRate));
        }
    }

    // Hide the AttackPoint sprite
    public void HideAttackPoint()
    {
        if (attackPointSpriteRenderer != null)
            attackPointSpriteRenderer.enabled = false;
    }

    // Coroutine to hide the sprite after a delay
    private IEnumerator HideAttackPointAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideAttackPoint();
    }
}
