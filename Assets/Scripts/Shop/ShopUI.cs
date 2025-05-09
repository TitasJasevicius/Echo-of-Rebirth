using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
  public Transform container;
  public Transform template;

  public PlayerResources playerResources;
  public PlayerEquipment playerEquipment;
  public ShopAssets shopAssets;
  public Weapon weapon;

  void Awake()
  {
    container = transform.Find("ShopContainer");
    template = container.Find("ItemTemplate");
    

    template.gameObject.SetActive(false);
    container.gameObject.SetActive(false);
  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    Hide();

    SpawnWeapons();

  }

  // Update is called once per frame
  void Update()
  {

  }
  public void SpawnWeapons()
  {
    Sprite daggerSprite = shopAssets.daggerSprite;

    Weapon daggerWeapon = CreateWeapon("Dagger2", 10, 1.5f, 5, 100, playerResources);





    CreateShopItem(daggerSprite, 0, daggerWeapon, container);
    CreateShopItem(daggerSprite, 1, daggerWeapon, container);
    CreateShopItem(daggerSprite, 2, daggerWeapon, container);


  }
  public void Show()
  {

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(true);
    }
  }

  public void Hide()
  {

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }
  }

  public void CreateShopItem<T>(Sprite itemSprite, int positionIndex, T item, Transform parentContainer) where T : class
  {
    Transform shopItemTransform = Instantiate(template, parentContainer);
    shopItemTransform.gameObject.SetActive(true);
    RectTransform shopRectTransform = shopItemTransform.GetComponent<RectTransform>();

    Vector2 templatePosition = template.GetComponent<RectTransform>().anchoredPosition;
    float widthOffset = 500f;

    shopRectTransform.anchoredPosition = new Vector2(templatePosition.x + (widthOffset * positionIndex), templatePosition.y);
    TextMeshProUGUI priceText = shopItemTransform.Find("ItemPrice").GetComponent<TextMeshProUGUI>();

    if (item is Weapon weapon)
    {
      int itemPrice = weapon.GetPrice();
      priceText.SetText(itemPrice.ToString());
      shopItemTransform.Find("ItemImage").GetComponent<Image>().sprite = itemSprite;
      shopItemTransform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(weapon.weaponName);

      Weapon weaponScript = shopItemTransform.Find("ShopWeapon").GetComponent<Weapon>();
      if (weaponScript != null)
      {
        weaponScript.SetWeaponStats(weapon);
      }
      /*Button buyButton = shopItemTransform.Find("ItemBuyButton").GetComponent<Button>();
      buyButton.onClick.RemoveAllListeners();

      buyButton.onClick.AddListener(weapon.ActivateWeapon);*/

      
    }
  }
  public static Weapon CreateWeapon(string weaponName, int damage, float attackSpeed, int range, int price, PlayerResources playerResources)
  {
    Weapon newWeapon = new Weapon();
    newWeapon.weaponName = weaponName;
    newWeapon.damage = damage;
    newWeapon.attackSpeed = attackSpeed;
    newWeapon.range = range;
    newWeapon.price = price;
    newWeapon.playerResources = playerResources;

    return newWeapon;
  }

}
