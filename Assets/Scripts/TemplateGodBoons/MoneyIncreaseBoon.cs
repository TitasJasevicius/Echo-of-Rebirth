using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
public class MoneyIncreaseBoon : MonoBehaviour
{
  decimal moneyIncreaseRatio = 2.5M;
  bool activated { get; set; } = false;

  [SerializeField] PlayerResources playerResources;
  public static int GetPrice()
  {
    return 300;
  }

  public static Sprite GetSprite()
  {
    return BoonAssets.Instance.MoneyIncreaseSprite;

  }

  public void IncreaseMoney()
  {
    if (activated)
    {
      return;
    }

    activated = true;

    playerResources.money = (int)(playerResources.money * moneyIncreaseRatio);
    Debug.Log($"New player money: {playerResources.money}");
  }
}


