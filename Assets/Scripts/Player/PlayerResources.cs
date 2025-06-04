using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int health;
    public int maxHealth = 100;

    public float baseCritChance = 0.1f;
    public float baseCritDamage = 1.5f; // 50% more damage
    public int baseDamage = 2;
    public float baseShopPrice = 1.0f;
    public float baseHealingEffectiveness = 1.0f;
    public float baselifeStealMultiplier = 1.0f;
    public float baseLifeSteal = 0.0f;
    public float baseGoldMultiplier = 1.0f;
    public int dashCooldown = 10;
    public float baseDamageReductionMultiplier = 1.0f;
    public float damageReduction = 1.0f; //base damage reduction
    public int invulnerabilityFrames = 2;
    public int sinceDamageTaken = int.MaxValue; // time since last damage taken in seconds
    public int heatlhRegenerationAmount = 0;
    public int healthRegenerationDuration = 0;
    public int chanceToBlindOnDash = 0; // blinding light boon, chance to blind enemies on dash causing them to miss attacks
    public int chanceToDodgeBlindedEnemies = 0; // blinding light boon, chance to dodge enemy attacks
    public int burningDOT = 0; // burning damage over time
    public int burningDuration = 0; // burning duration
    public int attackRange = 1; 
    public int newInstanceShield = 0;
    public int baseDodgeChance = 0; //regular, not conditional dodge chance
    public int baseInvisibilityDuration = 0; // in seconds
    public int shadowDashCooldown = 0;
    public int SleepChance = 0;
    public int SleepDuration = 0;
    public float baseAttackSpeed = 1.0f;
    public int bleedDamage = 0;
    public int bleedDuration = 0; 
    public float grandStandAttackSpeedMultiplier = 0.0f;
    public float bloodThirstMovementSpeed = 0.0f; // max attack speed multiplier
    public int focusExtraDamage = 0; // focus boon, extra damage on next attack

    public bool foxLuck = false; //foxluck boon status
    public bool resolve = false; //resolve boon status
    public bool lifeStealActive = false;
    public bool damageReductionActive = false;
    public bool regenerationActive = false;
    public bool parryActive = false;
    public bool blindingDashActive = false; //blinding dash boon status
    public bool burnActive = false; // burning status
    public bool pierceActive = false; // Secondary light boon status, pierce all enemies with attacks
    public bool shadowDashActive = false; // dash status
    public bool sleepAttackActive = false; // moon blade boon status
    public bool viciousAttackActive = false; // vicious attack boon status / bleed attacks
    public bool grandStandActive = false; // does nothing right now since theres no monster logic, when this is active attack speed should increased based off of the amount of alive mobs
    public bool bloodThirstActive = false; // blood thirst boon status, should increase ms based on bleeding enemies count
    public bool focusActive = false;

    public bool isInvisible = false;
    public bool isRegenerating = false;

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
        //boonInventory = new BoonInventory();
        //boonInventoryUI.SetBoonInventory(boonInventory);

        //BoonWorld.SpawnBoonWorld(new Vector3(-8, 4), new Boon { boonType = Boon.BoonType.ManaBoon });
        //BoonWorld.SpawnBoonWorld(new Vector3(-10, 10), new Boon { boonType = Boon.BoonType.HealthBoon });

        playerDeath = GetComponent<PlayerDeath>();
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
  public void IncreaseRegeneration(int value, int seconds)
  {
    if (!isRegenerating)
    {
      StartCoroutine(RegenerateHealth(value, seconds));
    }
  }

  public void UpdateRegeneration()
  {
    if (regenerationActive == true && sinceDamageTaken >= 5) //if the boon is active and player didint take damage for 5 seconds
    {
      IncreaseRegeneration(heatlhRegenerationAmount, healthRegenerationDuration);
    }

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


    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health); // Always update the health bar immediately

        if (health <= 0 && playerDeath != null)
        {
            playerDeath.HandlePlayerDeath();
            return; // Prevent further logic on death
        }

        if (isRegenerating)
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

    public void AddGold(int amount)
    {
        money += amount;
        if (moneyUI != null)
            moneyUI.SetMoney(money);
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
      
    }
    public void IncreaseBaseCrit(float value)
    {
      baseCritChance *= (1 + value / 100);
      
    }
    public void IncreaseBaseDamage(int value)
    {
      baseDamage += value;
     
    }
    public void IncreaseBaseGoldMultiplier(float value)
    {
      baseGoldMultiplier *= (1 + value / 100);
      
    }
    public void IncreaseBaseHealingEffectiveness(float value)
    {
      baseHealingEffectiveness *= (1 + value / 100);
      
    }
    public void IncreaseBaseLifeSteal(float value)
    {
      baselifeStealMultiplier += 1 * value / 100;
      
    }
    public void SetFoxLuckStatus(bool value)
    {
      foxLuck = value;
    
    }
    public void DecreaseDashCooldown(int value)
    {
      dashCooldown -= value;
     
    }
    public void IncreaseBaseDamageReduction(float value)
    {
      baseDamageReductionMultiplier *= (1 + value / 100);
      
    }
    public void IncreaseInvulnerabilityFrames(int value)
    {
      invulnerabilityFrames += value;
      
    }
    public void IncreaseRegenerationMultipliers(int value, int seconds)
    {
      heatlhRegenerationAmount += value;
      healthRegenerationDuration += seconds;
    }


    public void IncreaseBlindChance(int value)
    {
      chanceToBlindOnDash += value;
      
    }
    public void IncreaseMissChance(int value)
    {
      chanceToDodgeBlindedEnemies += value;
      
    }
    public void IncreaseBurnDamageAndDuration(int value, int seconds)
    {
      burningDOT += value;
      burningDuration += seconds;
      
    }
    public void IncreaseAttackRange(int value)
    {
      attackRange += value;
      
    }
    public void IncreaseNewInstanceShield(int value)
    {
      newInstanceShield += value;
      
    }
    public void IncreaseDodgeChance(int value)
    {
      baseDodgeChance += value;
      
    }
    public void IncreaseInvisibilityDuration(int value)
    {
      baseInvisibilityDuration += value;
      
    }
    public void IncreaseShadowDashCooldown(int value)
    {
      shadowDashCooldown += value;

    }
    public void IncreaseSleepChance(int value)
    {
      SleepChance += value;

    }
    public void IncreaseSleepDuration(int value)
    {
      SleepDuration += value;
    }
    public void IncreaseAttackSpeedMultiplier(float value)
    {
      grandStandAttackSpeedMultiplier += value / 100;

    }
    public void IncreaseCritMultiplier(float value)
    {
    baseCritDamage *= (1 + value / 100);

    }
    public void IncreaseBloodThirstMovementSpeed(float value)
    {
      bloodThirstMovementSpeed += value;
    }
    public void IncreaseFocusExtraDamage(int value)
    {
      focusExtraDamage += value;
    }
    public void IncreaseBleedDamage(int value)
    {
      bleedDamage += value;
    }
    public void IncreaseBleedDuration(int value)
    {
      bleedDuration += value;
    }
    public void IncreaseBaseAttackSpeed(float value)
    {
      baseAttackSpeed += value;
    }



}
