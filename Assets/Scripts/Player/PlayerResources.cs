using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int health;
    public int maxHealth = 100;

    public int mana;
    public int maxMana = 6; // 5 stages- 0 is none, 1,2,3,4,5 are the stages

    public int money; // money that is only available during single play round
    public int metaMoney; // money that's saved between game rounds in unity for next time

    private PlayerDeath playerDeath; // reference to the PlayerDeath script

    public HealthBar healthBar;
    public ManaBar manaBar;
    public Money moneyUI;

    void Start()
    {
        health = maxHealth;
        mana = 0;
        money = 0;


        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(6);
        moneyUI.SetMoney(0);
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.SetMoney(money);
        healthBar.SetHealth(health);
        manaBar.SetMana(mana);
    }

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && playerDeath != null)
        {
            playerDeath.HandlePlayerDeath();
        }

        healthBar.SetHealth(health);
    }

    public void UseMana(int amount)
    {
        mana -= amount;
        manaBar.SetMana(mana);
    }
}
