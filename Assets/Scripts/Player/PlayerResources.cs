using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int health;
    public int maxHealth = 100;

    [SerializeField] public float baseCritChance = 0.1f;
    [SerializeField] public int baseDamage = 2;
    [SerializeField] public float baseShopPrice = 1.0f;
    [SerializeField] public float baseHealingEffectiveness = 1.0f;
    [SerializeField] public float baselifeSteal = 0.0f;
    [SerializeField] public float baseGoldMultiplier = 1.0f;

    public bool foxLuck = false;

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
    //upgrades
    public MetaUpgradeManager metaUpgradeManager;
    public BoonInventory boonInventory;

    [SerializeField] private BoonInventoryUI boonInventoryUI;
  
  private void Awake()
  {
    boonInventory = new BoonInventory();
    boonInventoryUI.SetBoonInventory(boonInventory);

    //BoonWorld.SpawnBoonWorld(new Vector3(-8, 4), new Boon { boonType = Boon.BoonType.ManaBoon });
    //BoonWorld.SpawnBoonWorld(new Vector3(-10, 10), new Boon { boonType = Boon.BoonType.HealthBoon });

    
  }

  void Start()
    {
        metaUpgradeManager = FindFirstObjectByType<MetaUpgradeManager>(); //this probably shouldnt be findfirstobjectbytype
        if (metaUpgradeManager != null)
        {
          metaUpgradeManager.ApplyAllUpgrades();
        }

        else
        {
          Debug.LogError("MetaUpgradeManager is missing on PlayerResources GameObject!");
        }

        health = maxHealth;
        mana = 0;
        money = 500;

        // Load metaMoney from PlayerPrefs
        metaMoney = PlayerPrefs.GetInt(MetaMoneyKey, 0);

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(6);
        moneyUI.SetMoney(0);
        metaMoneyUI.SetMetaMoney(metaMoney);

        baseCritChance = 0.1f;
        baseDamage = 2;
        baseShopPrice = 1.0f;
        baseHealingEffectiveness = 1.0f;
        baselifeSteal = 0.0f;
        baseGoldMultiplier = 1.0f;
   



  }

    // Update is called once per frame
    void Update()
    {
        float previousLifeSteal = baselifeSteal;
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
        //Reset base life steal first then update 
        if(foxLuck == false || health <= maxHealth / 2) 
        {
          baselifeSteal = 0.0f;
        }
        if (foxLuck == true && health > maxHealth / 2) //lifesteal active above 50% hp
        {
        baselifeSteal = previousLifeSteal;

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
    public void IncreaseMaxHealth(int value)
    {

      maxHealth += value;

    }
    public void DecreaseShopPrices(float value)
    {
      baseShopPrice *= (1 - value / 100);
      Debug.Log($"New shop price: {baseShopPrice}");
    }
    public void IncreaseBaseCrit(float value)
    {
      baseCritChance *= (1 + value / 100);
      Debug.Log($"New crit chance: {baseCritChance}");
    }
    public void IncreaseBaseDamage(int value)
    {
      baseDamage += value;
      Debug.Log($"New base damage: {baseDamage}");
    }
    public void IncreaseBaseGoldMultiplier(float value)
    {
      baseGoldMultiplier *= (1 + value / 100);
      Debug.Log($"New gold multiplier: {baseGoldMultiplier}");
    }
    public void IncreaseBaseHealingEffectiveness(float value)
    {
      baseHealingEffectiveness *= (1 + value / 100);
      Debug.Log($"New healing effectiveness: {baseHealingEffectiveness}");
    }
    public void IncreaseBaseLifeSteal(float value)
    {
      baselifeSteal += 1 * value / 100;
      Debug.Log($"New life steal: {baselifeSteal}");
    }
    public void SetFoxLuckStatus(bool value)
    {
      foxLuck = value;
      Debug.Log($"Fox luck status: {foxLuck}");
    }
    

}
