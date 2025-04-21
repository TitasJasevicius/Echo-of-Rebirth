using UnityEngine;
using System;
public class LightsSpeed : MonoBehaviour
{
  
  public float SpeedIncrease { get; set; }
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
  public void ApplyBoon(PlayerResources playerResources, PlayerMovement playerMovement)
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
      Activated = true;
      playerResources.money -= DiscountedPrice;
      playerMovement.IncreaseMovementSpeed(SpeedIncrease);

      Debug.Log($"Lightspeed increased by {SpeedIncrease}.");
    }
  }
  public void SetSpeedIncrease(float amount)
  {
    SpeedIncrease = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }



}
