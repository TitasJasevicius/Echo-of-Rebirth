using UnityEngine;
using System;
using System.Collections.Generic;

public class ResoursfulnesBoon : MonoBehaviour
{
  public float BaseHealingMultiplier { get; set; }
  public int Price = 200;
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
    
    Debug.Log($"disc rice {DiscountedPrice}");
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
      
      playerResources.IncreaseBaseHealingEffectiveness(BaseHealingMultiplier);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Base healing effectiveness increased by {BaseHealingMultiplier}%.");
    }
    
  }
  public void SetBaseHealingMultiplier(float percentage)
  {
    BaseHealingMultiplier = percentage;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }
}
