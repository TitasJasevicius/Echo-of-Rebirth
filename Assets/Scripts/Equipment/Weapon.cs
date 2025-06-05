using UnityEngine;

public class Weapon : MonoBehaviour
{
  public string weaponName;
  public int damage;
  public float attackSpeed;
  public int range;
  public int price;
  public enum type { knife, dagger }
  [SerializeField] public PlayerResources playerResources;
  public Transform container;
  public Transform equipmentContainer;
  public AudioManager audioManager;
  public void Awake()
  {
    GameObject playerObj = GameObject.FindWithTag("Player");
    if (playerObj != null)
    {
      container = playerObj.transform;
      equipmentContainer = container.Find("Equipment");
      
    }
    else
    {
      Debug.LogError("gg");
    }


  }

  private bool isActive;
  public void BuyWeapon()
  {
    if (isActive)
    {
      Debug.Log("Weapon already purchased.");
      return;
    }
    if (playerResources.money < price)
    {
      Debug.Log("Not enough money to purchase the weapon.");
      return;
    }
    playerResources.money -= price;
    audioManager.PlaySFX(audioManager.purchaseItem);



    if (equipmentContainer != null)
    {
      transform.SetParent(equipmentContainer, false);
    }
    else
    {
      Debug.LogError("Equipment container not set!");
    }

    DeactivateAllEquippedWeapons();
    ActivateWeapon();
  }

  public void ActivateWeapon()
  {
    if (isActive) return;

    isActive = true;
    gameObject.SetActive(true);
    playerResources.IncreaseBaseDamage(damage);
    playerResources.IncreaseBaseAttackSpeed(attackSpeed);
    playerResources.IncreaseBaseDamage(range);

    if (equipmentContainer != null)
    {
      Weapon weaponScript = equipmentContainer.GetComponent<Weapon>();
      if (weaponScript != null)
      {
        weaponScript.SetWeaponStats(this);
      }
    }

    Debug.Log("Weapon activated: " + weaponName);
  }

  public void DeactivateWeapon()
  {
    //if (!isActive) return;

    isActive = false;
    gameObject.SetActive(false);
    playerResources.IncreaseBaseDamage(-damage);
    playerResources.IncreaseBaseAttackSpeed(-attackSpeed);
    playerResources.IncreaseBaseDamage(-range);

    if (equipmentContainer != null)
    {
      Weapon weaponScript = equipmentContainer.GetComponent<Weapon>();
      if (weaponScript != null)
      {
        weaponScript.SetWeaponStats(this);
      }
    }

    Debug.Log("Weapon deactivated: " + weaponName);
  }
  private void DeactivateAllEquippedWeapons()
  {
    if (equipmentContainer != null)
    {
      
      Weapon[] equippedWeapons = equipmentContainer.GetComponentsInChildren<Weapon>(true);
      foreach (var w in equippedWeapons)
      {
        if (w != this) 
          w.DeactivateWeapon();
      }
    }
  }
  public void SetWeaponStats(Weapon weapon)
  {
    this.weaponName = weapon.weaponName;
    this.damage = weapon.damage;
    this.attackSpeed = weapon.attackSpeed;
    this.range = weapon.range;
    this.price = weapon.price;
    this.playerResources = weapon.playerResources;

  }
  public int GetPrice()
  {
    return price;
  }
  public int GetDamage()
  {
    return damage;
  }
  public Weapon GetWeapon()
  {
    return this;
  }
  public void SetPrice(int newPrice)
  {
    price = newPrice;
  }
  public void SetDamage(int newDamage)
  {
    damage = newDamage;
  }
  public void SetAttackSpeed(float newAttackSpeed)
  {
    attackSpeed = newAttackSpeed;
  }
  public void SetRange(int newRange)
  {
    range = newRange;
  }
  public Weapon GetCurrentWeapon()
  {
    return this;
  }


}
