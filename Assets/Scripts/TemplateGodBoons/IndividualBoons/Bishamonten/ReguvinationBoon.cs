using System;
using UnityEngine;

public class ReguvinationBoon : MonoBehaviour
{
  public int RegenerationAmount { get; set; }
  public int RegenerationDuration { get; set; }
  public int Price = 300;
  public int DiscountedPrice { get; set; }
  public bool Activated { get; set; } = false;
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
      playerResources.IncreaseRegenerationMultipliers(RegenerationAmount, RegenerationDuration);
      Activated = true;
      playerResources.regenerationActive = true; 
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Regeneration amount increased by {RegenerationAmount}.");
    }
  }
  public void SetRegenerationAmount(int amount)
  {
    RegenerationAmount = amount;
  }

  public void SetRegenerationDuration(int duration)
  {
    RegenerationDuration = duration;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }
}
