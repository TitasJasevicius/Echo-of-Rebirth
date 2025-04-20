using System;
using UnityEngine;

public class QuickfeetBoon : MonoBehaviour
{
  public int InvFrameCount { get; set; }
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
      playerResources.IncreaseInvulnerabilityFrames(InvFrameCount);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Invincibility frames increased by {InvFrameCount}.");
    }
  }

  public void SetInvFrameCount(int count)
  {
    InvFrameCount = count;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }
}
