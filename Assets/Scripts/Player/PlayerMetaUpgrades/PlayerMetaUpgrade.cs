using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMetaUpgrade", menuName = "Scriptable Objects/PlayerMetaUpgrade")]
public abstract class PlayerMetaUpgrade : MonoBehaviour
{
  public string metaUpgradeName;
  public int metaUpgradeId = 0;
  protected PlayerResources playerResources;
  public void Initialize(PlayerResources playerResourcesObject)
  {
    playerResources = playerResourcesObject;
  }

  public abstract void ApplyMetaUpgrade();
}
