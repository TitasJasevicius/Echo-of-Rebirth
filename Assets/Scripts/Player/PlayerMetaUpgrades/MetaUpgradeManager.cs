using UnityEngine;
using System.Collections.Generic;

public class MetaUpgradeManager : MonoBehaviour
{
  public List<PlayerMetaUpgrade> upgrades = new List<PlayerMetaUpgrade>();
  public PlayerResources playerResources;

  void Start()
  {
    // Try to find the PlayerResources component in the scene
    playerResources = FindFirstObjectByType<PlayerResources>();
    if (playerResources != null)
    {
      InitializeUpgrades();
      //ApplyAllUpgrades();
    }
    else
    {
      Debug.LogError("PlayerResources component is missing in the scene!");
    }
  }

  private void InitializeUpgrades()
  {
    foreach (PlayerMetaUpgrade upgrade in upgrades)
    {
      upgrade.Initialize(playerResources);
    }
  }

  public void ApplyAllUpgrades()
  {
    foreach (PlayerMetaUpgrade upgrade in upgrades)
    {
      upgrade.ApplyMetaUpgrade();
    }
  }
}
