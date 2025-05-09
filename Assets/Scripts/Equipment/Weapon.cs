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
  public void Awake()
  {
    container = transform.Find("Player");
    

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
    ActivateWeapon();
  }

  public void ActivateWeapon()
  {
    if (isActive) return;

    isActive = true;
    gameObject.SetActive(true);
    //buff playerstats
    playerResources.IncreaseBaseDamage(damage);
    playerResources.IncreaseBaseAttackSpeed(attackSpeed);
    playerResources.IncreaseBaseDamage(range);

    Weapon weaponScript = container.Find("Equipment").GetComponent<Weapon>();
    if (weaponScript != null)
    {
      weaponScript.SetWeaponStats(this);
    }

    Debug.Log("Weapon activated: " + weaponName); 


  }

  public void DeactivateWeapon()
  {
    if (!isActive) return;

    isActive = false;
    gameObject.SetActive(false);
    //debuff playerstats
    playerResources.IncreaseBaseDamage(-damage);
    playerResources.IncreaseBaseAttackSpeed(-attackSpeed);
    playerResources.IncreaseBaseDamage(-range);

    Weapon weaponScript = container.Find("Equipment").GetComponent<Weapon>();
    if (weaponScript != null)
    {
      weaponScript.SetWeaponStats(this);
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
