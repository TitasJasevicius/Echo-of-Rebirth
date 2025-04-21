using UnityEngine;
using System;
public class MoonBlade : MonoBehaviour
{
  public int SleepChance { get; set; }
  public int SleepDuration { get; set; }
  public int BaseDamageIncrease { get; set; }
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
      playerResources.IncreaseSleepChance(SleepChance);
      playerResources.IncreaseSleepDuration(SleepDuration);
      playerResources.IncreaseBaseDamage(BaseDamageIncrease);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Moon Blade chance increased by {SleepChance}.");
    }
  }
  public void SetSleepChance(int amount)
  {
    SleepChance = amount;
  }
  public void SetSleepDuration(int amount)
  {
    SleepDuration = amount;
  }
  public void SetBaseDamageIncrease(int amount)
  {
    BaseDamageIncrease = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }



}
