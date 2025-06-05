using UnityEngine;

public class SpeedMetaUpgrade : MonoBehaviour
{
  public int upgradeCost = 10;
  public float speedIncrease = 2.0f; // Amount to increase runSpeed per upgrade
  private const string SpeedUpgradeKey = "PermanentSpeedUpgradeLevel";

  private PlayerMovement playerMovement;

  void Start()
  {
    playerMovement = FindFirstObjectByType<PlayerMovement>();
    ApplyPermanentUpgrade();
  }

  // Call this method when the player buys the upgrade
  public void BuyUpgrade()
  {
    if (playerMovement == null)
    {
      Debug.LogError("PlayerMovement not found!");
      return;
    }

    PlayerResources playerResources = FindFirstObjectByType<PlayerResources>();
    if (playerResources == null)
    {
      Debug.LogError("PlayerResources not found!");
      return;
    }

    if (playerResources.metaMoney >= upgradeCost)
    {
      playerResources.SpendMetaMoney(upgradeCost);

      int currentLevel = PlayerPrefs.GetInt(SpeedUpgradeKey, 0);
      currentLevel++;
      PlayerPrefs.SetInt(SpeedUpgradeKey, currentLevel);
      PlayerPrefs.Save();

      ApplyPermanentUpgrade();
      Debug.Log($"Permanent speed upgrade purchased! New level: {currentLevel}");
    }
    else
    {
      Debug.LogWarning("Not enough metaMoney to buy speed upgrade.");
    }
  }

  // Apply the upgrade at game start or after purchase
  public void ApplyPermanentUpgrade()
  {
    int upgradeLevel = PlayerPrefs.GetInt(SpeedUpgradeKey, 0);
    if (playerMovement != null)
    {
      playerMovement.runSpeed = 40f + (speedIncrease * upgradeLevel); // 40f is the default runSpeed
    }
  }

  public int GetUpgradeCost()
  {
    return this.upgradeCost;
  }

  public float GetSpeedIncrease()
  {
    return this.speedIncrease;
  }
}
