using UnityEngine;
using System;
using System.Collections.Generic;

public class BoonAssets : MonoBehaviour
{
    
  public static BoonAssets Instance { get; private set; }
  public Transform pfBoonWorld;

  public void Awake()
  {
    Instance = this;
  }

  

  public Sprite SwingSpeedBoonSprite;
  public Sprite HealthBoonSprite;
  public Sprite ManaBoonSprite;
  public Sprite DamageBoonSprite;
  public Sprite MoneyIncreaseSprite;

  [Header("Boon Shop Backgrounds")]
  public Sprite InariShopBackground;
  public Sprite BishamontenShopBackground;
  public Sprite AmaterasuShopBackground;
  public Sprite TsukoyamiShopBackground;
  public Sprite HachimanShopBackground;

  public Sprite GetMoneySprite()
  {
    return MoneyIncreaseSprite;
  }
  public Sprite GetHeathSprite()
  {
    return HealthBoonSprite;
  }
  public Sprite GetSwingSpeedSprite()
  {
    return SwingSpeedBoonSprite;
  }
  public Sprite GetManaSprite()
  {
    return ManaBoonSprite;
  }
  public Sprite GetDamageSprite()
  {
    return DamageBoonSprite;
  }
}
