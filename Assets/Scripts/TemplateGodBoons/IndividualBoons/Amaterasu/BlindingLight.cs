using UnityEngine;
using System;
public class BlindingLight : MonoBehaviour
{
  public int BlindChance { get; set; }
  public int MissChance { get; set; }
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
      playerResources.IncreaseBlindChance(BlindChance);
      playerResources.IncreaseMissChance(MissChance);
      Activated = true;
      playerResources.money -= DiscountedPrice;
      playerResources.blindingDashActive = true; //update players blinding light boon status
      Debug.Log($"Blinding Light chance increased by {BlindChance}.");
    }
  }
  public void SetBlindChance(int amount)
  {
    BlindChance = amount;
  }
  public void SetMissChance(int amount)
  {
    MissChance = amount;
  }
  public int GetPrice()
  {
    return DiscountedPrice;
  }


}
