using UnityEngine;
using System;
using System.Collections.Generic;

public class BanteringBoon : MonoBehaviour
{
  
  private float PriceDecreasePercentage { get; set; }
  public int Price = 275;
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

    if(Activated)
    {
      Debug.Log("Boon already activated.");
      return;
    }
    if(Activated == false && playerResources.money < DiscountedPrice)
    {
      Debug.Log("Not enough money to activate the boon.");
      return;
    }
    if(Activated == false && playerResources.money >= DiscountedPrice)
    {
      playerResources.DecreaseShopPrices(PriceDecreasePercentage);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Shop prices decreased by {PriceDecreasePercentage}%.");
    }
    
  }
  public void SetPriceDecreasePercentage(float percentage)
  {
    PriceDecreasePercentage = percentage;
  }

  public int GetPrice()
  {
    return DiscountedPrice;
  }

}
