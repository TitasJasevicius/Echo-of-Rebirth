using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int health;
    public int maxHealth = 100;

    public int mana;
    public int maxMana = 6; // (0 is none, 6 is 5 AKA full)

    public int money; // money that is only available during single play round
    public int metaMoney; // money that's saved between game rounds in unity for next time
    private const string MetaMoneyKey = "MetaMoney"; // key to save metaMoney in PlayerPrefs (unitys way of saving data between game sessions)

    private PlayerDeath playerDeath; // reference to the PlayerDeath script

    public HealthBar healthBar; // health bar UI element
    public ManaBar manaBar; // mana bar UI element
    public Money moneyUI; // money counter UI element
    public MetaMoney metaMoneyUI; // meta money counter UI element


    void Start()
    {
        health = maxHealth;
        mana = 0;
        money = 0;

        // Load metaMoney from PlayerPrefs
        metaMoney = PlayerPrefs.GetInt(MetaMoneyKey, 0);

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(6);
        moneyUI.SetMoney(0);
        metaMoneyUI.SetMetaMoney(metaMoney);
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.SetMoney(money);
        healthBar.SetHealth(health);
        manaBar.SetMana(mana);

        // cheat to add meta money for testing
        if (Input.GetKeyDown(KeyCode.F))
        {
            metaMoney += 1;
            metaMoneyUI.SetMetaMoney(metaMoney);
            SaveMetaMoney();
        }

        //exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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

    public void SpendMetaMoney(int amount)
    {
        if (metaMoney >= amount)
        {
            metaMoney -= amount;
            SaveMetaMoney();
        }
        else
        {
            Debug.LogWarning("Not enough metaMoney to spend.");
        }
    }

    private void SaveMetaMoney()
    {
        PlayerPrefs.SetInt(MetaMoneyKey, metaMoney);
        PlayerPrefs.Save();
    }

}
