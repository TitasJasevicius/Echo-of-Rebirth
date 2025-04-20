using UnityEngine;

public class BishamontenBoons : MonoBehaviour
{
  [SerializeField] public ReguvinationBoon ReguvinationBoon;
  [SerializeField] public ResolveBoon ResolveBoon;
  [SerializeField] public QuickfeetBoon QuickfeetBoon;
  [SerializeField] public ParryBoon ParryBoon;
  [SerializeField] public StaminaBoon StaminaBoon;

  public BishamontenBoonType[] boons;

  public enum BishamontenBoonType
  {
    ReguvinationBoon,
    ResolveBoon,
    QuickfeetBoon,
    ParryBoon,
    StaminaBoon
  }

  private void Start()
  {
    boons = new BishamontenBoonType[]
    {
            BishamontenBoonType.ReguvinationBoon,
            BishamontenBoonType.ResolveBoon,
            BishamontenBoonType.QuickfeetBoon,
            BishamontenBoonType.ParryBoon,
            BishamontenBoonType.StaminaBoon
    };
  }
}
