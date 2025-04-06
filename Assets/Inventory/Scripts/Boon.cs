using UnityEngine;

public class Boon
{
  public enum BoonType
  {
    SwingSpeedBoon,
    ManaBoon,
    DamageBoon,
    HealthBoon,
  }

  public BoonType boonType { get; set; }

  public Sprite GetSprite()
  {
    switch (boonType)
    {
      default:
      case BoonType.SwingSpeedBoon: return BoonAssets.Instance.SwingSpeedBoonSprite;
      case BoonType.ManaBoon: return BoonAssets.Instance.ManaBoonSprite;
      case BoonType.HealthBoon: return BoonAssets.Instance.HealthBoonSprite;
      case BoonType.DamageBoon: return BoonAssets.Instance.DamageBoonSprite;
    }
  }

  

}
