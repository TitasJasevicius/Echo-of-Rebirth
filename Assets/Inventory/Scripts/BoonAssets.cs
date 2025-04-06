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

  public Sprite GetMoneySprite()
  {
    return MoneyIncreaseSprite;
  }
}
