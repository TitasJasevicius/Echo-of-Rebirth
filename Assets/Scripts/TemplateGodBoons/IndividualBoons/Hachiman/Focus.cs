using UnityEngine;
using System;

public class Focus : MonoBehaviour
{
  public int ExtraDamage { get; set; }
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
      playerResources.IncreaseFocusExtraDamage(ExtraDamage);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      playerResources.focusActive = true; //update players focus status
      Debug.Log($"Focus damage increased by {ExtraDamage}.");
    }
  }
  public void SetExtraDamage(int amount)
  {
    ExtraDamage = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }


}
