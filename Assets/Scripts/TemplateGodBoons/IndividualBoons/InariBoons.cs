using UnityEngine;
using static Boon;

public class InariBoons : MonoBehaviour
{
  [SerializeField] public BanteringBoon banteringBoon;
  [SerializeField] public EasyFindBoon easyFindBoon;
  [SerializeField] public FoxsLuckBoon foxsLuckBoon;
  [SerializeField] public LuckyStrikesBoon luckyStrikesBoon;
  [SerializeField] public ResoursfulnesBoon resoursfulnesBoon;

  public InariBoonType[] boons;

  public enum InariBoonType
  {
    BanteringBoon,
    EasyFindBoon,
    FoxsLuckBoon,
    LuckyStrikesBoon,
    ResoursfulnesBoon
  }

  private void Start()
  {
    boons = new InariBoonType[]
    {
            InariBoonType.BanteringBoon,
            InariBoonType.EasyFindBoon,
            InariBoonType.FoxsLuckBoon,
            InariBoonType.LuckyStrikesBoon,
            InariBoonType.ResoursfulnesBoon
    };
  }

}
