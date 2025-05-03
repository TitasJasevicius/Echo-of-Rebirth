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



  }
  public int GetPrice()
  {
    return price;
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
