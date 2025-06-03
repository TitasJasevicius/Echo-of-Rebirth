using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 100;
    [SerializeField] FloatingHealthbar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
            healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBar != null)
            healthBar.UpdateHealthBar(health, maxHealth);

        // play damage animation

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        // play death animation
        Destroy(gameObject);
    }
}