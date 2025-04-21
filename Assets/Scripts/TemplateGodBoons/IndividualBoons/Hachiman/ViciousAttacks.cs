using UnityEngine;
using System;

public class ViciousAttacks : MonoBehaviour
{
  public int BaseAttackDamageIncrease { get; set; }
  public int BleedDamage { get; set; }
  public int BleedDuration { get; set; }
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
      playerResources.IncreaseBleedDamage(BleedDamage);
      playerResources.IncreaseBleedDuration(BleedDuration);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      playerResources.viciousAttackActive = true; //update players vicious attacks status
      Debug.Log($"Vicious Attacks damage increased by {BaseAttackDamageIncrease}.");
    }
  }
  public void SetBaseAttackDamageIncrease(int amount)
  {
    BaseAttackDamageIncrease = amount;
  }
  public void SetBleedDamage(int amount)
  {
    BleedDamage = amount;
  }
  public void SetBleedDuration(int amount)
  {
    BleedDuration = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }



}
