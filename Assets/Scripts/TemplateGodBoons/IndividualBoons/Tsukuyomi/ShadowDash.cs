using UnityEngine;
using System;
public class ShadowDash : MonoBehaviour
{
  public int InvisibilityDuration { get; set; }
  public int ShadowDashCooldown { get; set; }
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
      playerResources.IncreaseInvisibilityDuration(InvisibilityDuration);
      playerResources.IncreaseShadowDashCooldown(ShadowDashCooldown);
      playerResources.shadowDashActive = true; //update players shadow dash status
      Activated = true;
      playerResources.money -= DiscountedPrice;
      Debug.Log($"Shadow Dash invisibility duration increased by {InvisibilityDuration}.");
    }
  }
  public void SetInvisibilityDuration(int amount)
  {
    InvisibilityDuration = amount;
  }
  public void SetShadowDashCooldown(int amount)
  {
    ShadowDashCooldown = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }



}
