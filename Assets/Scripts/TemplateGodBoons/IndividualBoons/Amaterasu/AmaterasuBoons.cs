using UnityEngine;

public class AmaterasuBoons : MonoBehaviour
{
  [SerializeField] public BlindingLight blindingLight;
  [SerializeField] public BurningBlade burningBlade;
  [SerializeField] public LightsSpeed lightsSpeed;
  [SerializeField] public MorningSunshine morningSunshine;
  [SerializeField] public SunsReach sunsReach;

  public AmaterasuBoonType[] boons;
  public enum AmaterasuBoonType
  {
    BlindingLight,
    BurningBlade,
    LightsSpeed,
    MorningSunshine,
    SunsReach
  }
  private void Start()
  {
    boons = new AmaterasuBoonType[]
    {
      AmaterasuBoonType.BlindingLight,
      AmaterasuBoonType.BurningBlade,
      AmaterasuBoonType.LightsSpeed,
      AmaterasuBoonType.MorningSunshine,
      AmaterasuBoonType.SunsReach
    };

  }
}
