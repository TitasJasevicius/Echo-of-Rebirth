using UnityEngine;

public class DamageMetaUpgrade : MonoBehaviour
{
  public int upgradeCost = 10;
  public int damageIncrease = 1;
  private const string DamageUpgradeKey = "PermanentDamageUpgradeLevel";

  private PlayerResources playerResources;

  void Start()
  {
    playerResources = FindFirstObjectByType<PlayerResources>();
    ApplyPermanentUpgrade();
  }

  // Call this method when the player buys the upgrade
  public void BuyUpgrade()
  {
    if (playerResources == null)
    {
      Debug.LogError("PlayerResources not found!");
      return;
    }

    if (playerResources.metaMoney >= upgradeCost)
    {
      playerResources.SpendMetaMoney(upgradeCost);

      int currentLevel = PlayerPrefs.GetInt(DamageUpgradeKey, 0);
      currentLevel++;
      PlayerPrefs.SetInt(DamageUpgradeKey, currentLevel);
      PlayerPrefs.Save();

      ApplyPermanentUpgrade();
      Debug.Log($"Permanent damage upgrade purchased! New level: {currentLevel}");
    }
    else
    {
      Debug.LogWarning("Not enough metaMoney to buy damage upgrade.");
    }
  }

  // Apply the upgrade at game start or after purchase
  public void ApplyPermanentUpgrade()
  {
    int upgradeLevel = PlayerPrefs.GetInt(DamageUpgradeKey, 0);
    if (playerResources != null)
    {
      playerResources.baseDamage = 2 + (damageIncrease * upgradeLevel); // 2 is the default baseDamage
    }
  }
  public int GetUpgradeCost()
  {
    return this.upgradeCost;

  }

  public int GetDamageIncrease()
  {
    return this.damageIncrease;
  }
}
