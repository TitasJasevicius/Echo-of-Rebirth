using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float longAttackRange = 1.5f; // Not used for box, but kept for reference
    public Vector2 longAttackBoxSize = new Vector2(2.5f, 1.0f); // Wider in X than Y
    public Vector2 longAttackBoxOffset = new Vector2(1.0f, 0f); // Offset to the right
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    public float attackRate = 2f;
    public float longAttackRate = 0.5f; // Lower = longer cooldown (e.g. 0.5 = 2s)
    private float nextAttackTime = 0f;

    public bool inputLocked = false;

    public Animator animator;
    private SpriteRenderer attackPointSpriteRenderer;
    public AudioManager audioManager;

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
        if (inputLocked || !enabled)
            return;

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                LongAttack();
                nextAttackTime = Time.time + 1f / longAttackRate;
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");
        // Play attack sound effect
        audioManager.PlaySFX(audioManager.attack);
        // Show the attack point sprite for the attack duration
        ShowAttackPoint();

        // Detect all enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage such enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);

            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    void LongAttack()
    {
        // Play long attack animation
        animator.SetTrigger("AttackLong");
        // Play attack sound effect
        audioManager.PlaySFX(audioManager.attack);
        // Show the attack point sprite for the attack duration
        ShowAttackPoint();

        // Calculate the box center in world space
        Vector2 boxCenter = (Vector2)attackPoint.position + (Vector2)(attackPoint.right * longAttackBoxOffset.x) + (Vector2)(attackPoint.up * longAttackBoxOffset.y);

        // Detect all enemies in the oblong (box) area
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(boxCenter, longAttackBoxSize, attackPoint.eulerAngles.z, enemyLayers);

        // Damage such enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);

            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        // Normal attack range (default color)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        // Long attack oblong area (different color)
        Gizmos.color = Color.cyan;
        // Calculate the box center in world space
        Vector3 boxCenter = attackPoint.position + attackPoint.right * longAttackBoxOffset.x + attackPoint.up * longAttackBoxOffset.y;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, attackPoint.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(longAttackBoxSize.x, longAttackBoxSize.y, 1f));
        Gizmos.matrix = Matrix4x4.identity;
    }

    // Show the AttackPoint sprite for the attack duration
    public void ShowAttackPoint()
    {
        //if (attackPointSpriteRenderer != null)
        //{
        //    attackPointSpriteRenderer.enabled = true;
        //    StartCoroutine(HideAttackPointAfterDelay(0.5f / attackRate));
        //}
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
