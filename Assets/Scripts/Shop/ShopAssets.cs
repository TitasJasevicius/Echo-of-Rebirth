using UnityEngine;

public class ShopAssets : MonoBehaviour
{
  public static ShopAssets Instance { get; private set; }
  

  public void Awake()
  {
    Instance = this;
  }
  public Sprite daggerSprite;

}
