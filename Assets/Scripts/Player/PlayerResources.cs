using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public float baselifeStealMultiplier = 1.0f;
    [SerializeField] public float baseLifeSteal = 0.0f;
    [SerializeField] public float baseGoldMultiplier = 1.0f;
    [SerializeField] public int dashCooldown = 10;
    [SerializeField] public float baseDamageReductionMultiplier = 1.0f;
    [SerializeField] public float damageReduction = 1.0f;
    [SerializeField] public int invulnerabilityFrames = 2;
    [SerializeField] public int sinceDamageTaken = int.MaxValue;
    [SerializeField] public int heatlhRegenerationAmount = 0;
    [SerializeField] public int healthRegenerationDuration = 0;

    private bool isRegenerating = false;



    public bool foxLuck = false; //foxluck boon status
    public bool resolve = false; //resolve boon status
    public bool lifeStealActive = false;
    public bool damageReductionActive = false;
    public bool regenerationActive = false;
    public bool parryActive = false;

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
    //Reset base life steal first then update 

      UpdateLifeSteal();
      UpdateDamageReduction();
      UpdateRegeneration();




  }
    public void UpdateLifeSteal()
    {
      
      
      if (foxLuck == false || health <= maxHealth / 2)
      {
        baseLifeSteal = 0.0f;
        lifeStealActive = false;

      }
      if (foxLuck == true && health > maxHealth / 2 && lifeStealActive == false) //lifesteal active above 50% hp
      {
        baseLifeSteal += baselifeStealMultiplier;
        lifeStealActive = true;

      }
    }
    public void UpdateDamageReduction()
    {
      
      if (resolve == false || health <= maxHealth / 2)
      {
        damageReduction = 1.0f;
      damageReductionActive = false;
    }
      if (resolve == true && health > maxHealth / 2 && damageReductionActive == false) //while under 50%hp gain damage reduction if resolve boon is active
      {
        damageReduction *= baseDamageReductionMultiplier;
        damageReductionActive = true;
      }
    }
    public void UpdateRegeneration()
    {
      if (regenerationActive == true && sinceDamageTaken >= 5) //if the boon is active and player didint take damage for 5 seconds
      {
      IncreaseRegeneration(heatlhRegenerationAmount, healthRegenerationDuration);
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

        if (isRegenerating) //reset regen if player took damage
        {
          StopCoroutine("RegenerateHealth");
          isRegenerating = false;
        }
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
      baselifeStealMultiplier += 1 * value / 100;
      Debug.Log($"New life steal: {baselifeStealMultiplier}");
    }
    public void SetFoxLuckStatus(bool value)
    {
      foxLuck = value;
     Debug.Log($"Fox luck status: {foxLuck}");
    }
    public void DecreaseDashCooldown(int value)
    {
      dashCooldown -= value;
      Debug.Log($"New dash cooldown: {dashCooldown}");
    }
    public void IncreaseBaseDamageReduction(float value)
    {
      baseDamageReductionMultiplier *= (1 + value / 100);
      Debug.Log($"New damage reduction: {baseDamageReductionMultiplier}");
    }
    public void IncreaseInvulnerabilityFrames(int value)
    {
      invulnerabilityFrames += value;
      Debug.Log($"New invulnerability frames: {invulnerabilityFrames}");
    }
    public void IncreaseRegenerationMultipliers(int value, int seconds)
    {
      heatlhRegenerationAmount += value;
    healthRegenerationDuration += seconds;
    }
    public void IncreaseRegeneration(int value, int seconds)
    {
      if (!isRegenerating) 
      {
        StartCoroutine(RegenerateHealth(value, seconds));
      }
    }

    private IEnumerator RegenerateHealth(int value, int seconds)
    {
    isRegenerating = true;

    int totalTicks = seconds; // Number of ticks (1 tick per second)
      float tickInterval = 1.0f; // Time between each tick in seconds

      for (int i = 0; i < totalTicks; i++)
      {
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth); 
        healthBar.SetHealth(health);

        yield return new WaitForSeconds(tickInterval); 
      }
      isRegenerating = false;
  }


}
