using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerHealthUpgrade", menuName = "Scriptable Objects/PlayerHealthUpgrade")]
public class PlayerHealthUpgrade : PlayerMetaUpgrade
{
  int healthUpgradeRatio = 1; 

  public override void ApplyMetaUpgrade()
  {
    if (playerResources != null)
    {
      double upgradeValue = GetUpgradeHealthAmount();
      playerResources.maxHealth = (int)(playerResources.maxHealth + playerResources.maxHealth * upgradeValue);
      playerResources.healthBar.SetMaxHealth(playerResources.maxHealth);
      Debug.Log($"New maxHealth: {playerResources.maxHealth}");
    }
  }

  public double GetUpgradeHealthAmount()
  {
    double upgradeAmount = healthUpgradeRatio / 100.0;
    return upgradeAmount;
  }
}
