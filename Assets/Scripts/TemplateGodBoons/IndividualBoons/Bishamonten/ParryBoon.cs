using System;
using UnityEngine;

public class ParryBoon : MonoBehaviour
{
  public int BaseAttackDamageIncrease { get; set; }
  public bool Activated { get; set; } = false;
  public int Price = 300;
  public int DiscountedPrice { get; set; }
  [SerializeField] public PlayerResources PR;
  private float previousBaseShopPrice;

  public void Awake()
  {
    previousBaseShopPrice = PR.baseShopPrice;
    DiscountedPrice = (int)Math.Round(Price * PR.baseShopPrice);
  }
  public void Update()
  {
    if (PR.baseShopPrice != previousBaseShopPrice)
    {
      DiscountedPrice = (int)Math.Round(Price * PR.baseShopPrice);
      previousBaseShopPrice = PR.baseShopPrice;
    }
  }
  public void ApplyBoon(PlayerResources playerResources)
  {
    if (Activated)
    {
      Debug.Log("Boon already activated.");
      return;
    }
    if (Activated == false && playerResources.money < DiscountedPrice)
    {
      Debug.Log("Not enough money to activate the boon.");
      return;
    }
    if (Activated == false && playerResources.money >= DiscountedPrice)
    {
      playerResources.IncreaseBaseDamage(BaseAttackDamageIncrease);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      playerResources.parryActive = true; //update players parry boon status
      Debug.Log($"Parry damage increased by {BaseAttackDamageIncrease}.");
    }
  }
  public void SetBaseAttackDamageIncrease(int amount)
  {
    BaseAttackDamageIncrease = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }


}
