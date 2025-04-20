using System.IO;
using System;
using UnityEngine;

public class StaminaBoon : MonoBehaviour
{
   public int CooldownReduction { get; set; }
   public int Price = 350;
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
      playerResources.DecreaseDashCooldown(CooldownReduction);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Dash cooldown reduced by {CooldownReduction} seconds.");
    }

  }
  public void SetCooldownReduction(int reduction)
  {
    CooldownReduction = reduction;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }
}
