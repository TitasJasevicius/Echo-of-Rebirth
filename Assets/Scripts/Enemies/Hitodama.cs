using UnityEngine;

public class Hitodama : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] FloatingHealthbar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth); // Update the health bar
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth); // Update the health bar

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
