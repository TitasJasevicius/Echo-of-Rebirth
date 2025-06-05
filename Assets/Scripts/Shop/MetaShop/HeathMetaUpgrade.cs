using UnityEngine;

public class HeathMetaUpgrade : MonoBehaviour
{
  public int upgradeCost = 10;
  public int healthIncrease = 10;
  private const string HealthUpgradeKey = "PermanentHealthUpgradeLevel";
  public AudioManager audioManager;

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

      int currentLevel = PlayerPrefs.GetInt(HealthUpgradeKey, 0);
      currentLevel++;
      PlayerPrefs.SetInt(HealthUpgradeKey, currentLevel);
      PlayerPrefs.Save();

      ApplyPermanentUpgrade();
      audioManager.PlaySFX(audioManager.purchaseItem);
      Debug.Log($"Permanent health upgrade purchased! New level: {currentLevel}");
    }
    else
    {
      Debug.LogWarning("Not enough metaMoney to buy health upgrade.");
    }
  }

  // Apply the upgrade at game start or after purchase
  public void ApplyPermanentUpgrade()
  {
    int upgradeLevel = PlayerPrefs.GetInt(HealthUpgradeKey, 0);
    if (playerResources != null)
    {
      playerResources.maxHealth = 100 + (healthIncrease * upgradeLevel); // 100 is the default maxHealth
      playerResources.health = playerResources.maxHealth; // Optionally heal to full
      if (playerResources.healthBar != null)
        playerResources.healthBar.SetMaxHealth(playerResources.maxHealth);
    }
  }

  public int GetUpgradeCost()
  {
    return this.upgradeCost;
  }

  public int GetHealthIncrease()
  {
    return this.healthIncrease;
  }
}
