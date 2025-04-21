using UnityEngine;

public class HachimanBoons : MonoBehaviour
{
  [SerializeField] public Focus focus;
  [SerializeField] public Mastery mastery;
  [SerializeField] public Bloodthirst bloodthirst;
  [SerializeField] public GrandStand grandStand;
  [SerializeField] public ViciousAttacks viciousAttacks;

  public HachimanBoonType[] boons;
  public enum HachimanBoonType
  {
    Focus,
    Mastery,
    Bloodthirst,
    GrandStand,
    ViciousAttacks
  }
  
  private void Start()
  {
    boons = new HachimanBoonType[]
    {
      HachimanBoonType.Focus,
      HachimanBoonType.Mastery,
      HachimanBoonType.Bloodthirst,
      HachimanBoonType.GrandStand,
      HachimanBoonType.ViciousAttacks
    };

  }


}
