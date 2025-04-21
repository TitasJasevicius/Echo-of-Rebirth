using UnityEngine;

public class TsukuyomiBoons : MonoBehaviour
{
  [SerializeField] public MoonBlade MoonBlade;
  [SerializeField] public ShadowDash ShadowDash;
  [SerializeField] public SecondaryLight SecondaryLight;
  [SerializeField] public FullMoon FullMoon;
  [SerializeField] public MoonlightTrickery MoonlightTrickery;

  public TsukuyomiBoonType[] boons;
  public enum TsukuyomiBoonType
  {
    MoonBlade,
    ShadowDash,
    SecondaryLight,
    FullMoon,
    MoonlightTrickery
  }
  private void Start()
  {
    boons = new TsukuyomiBoonType[]
    {
            TsukuyomiBoonType.MoonBlade,
            TsukuyomiBoonType.ShadowDash,
            TsukuyomiBoonType.SecondaryLight,
            TsukuyomiBoonType.FullMoon,
            TsukuyomiBoonType.MoonlightTrickery
    };
  }

}
