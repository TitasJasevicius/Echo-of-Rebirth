using UnityEngine;
using System;
public class BurningBlade : MonoBehaviour
{
  public int BurningDamage { get; set; }
  public int BurningDuration { get; set; }
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
      playerResources.IncreaseBurnDamageAndDuration(BurningDamage, BurningDuration);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      playerResources.burnActive = true; //update players burning blade boon status
      Debug.Log($"Burning Blade damage increased by {BurningDamage}.");
    }
  }
  public void SetBurningDamage(int amount)
  {
    BurningDamage = amount;
  }
  public void SetBurningDuration(int amount)
  {
    BurningDuration = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }

}
