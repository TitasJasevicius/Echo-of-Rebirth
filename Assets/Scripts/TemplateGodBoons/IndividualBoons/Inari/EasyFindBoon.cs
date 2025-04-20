using UnityEngine;
using System;
using System.Collections.Generic;

public class EasyFindBoon : MonoBehaviour
{
    public float GoldFindMultiplier { get; set; }
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
      playerResources.IncreaseBaseGoldMultiplier(GoldFindMultiplier);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Gold multiplier increased by {GoldFindMultiplier}%.");
    }
    
  }
  public void SetGoldFindMultilplier(float percentage)
  {
    GoldFindMultiplier = percentage;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }
}
