using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MetaShopUI : MonoBehaviour
{
  public Transform container;
  public Transform template;

  [SerializeField] public PlayerResources playerResources;
  [SerializeField] public PlayerMovement playerMovement;

  [SerializeField] public DamageMetaUpgrade damageMetaUpgrade;
  [SerializeField] public SpeedMetaUpgrade speedMetaUpgrade;
  [SerializeField] public HeathMetaUpgrade healthMetaUpgrade;
  public AudioManager audioManager;

  public BoonAssets boonAssets;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
    {
    Hide();
    SpawnMetaUpgrades();

  }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void Awake()
  {
    container = transform.Find("MetaShopContainer");
    template = container.Find("MetaShopTemplate");

    template.gameObject.SetActive(false);


  }

  public void Hide()
  {

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }
  }

  public void Show()
  {

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(true);
    }
  }
  public void SpawnMetaUpgrades()
  {
    Sprite metaSprite = boonAssets.GetMoneySprite();

    CreateMetaShopItem(metaSprite, "Speed Upgrade: +2", 0, speedMetaUpgrade, container);
    CreateMetaShopItem(metaSprite, "Damage Upgrade: +10", 1, damageMetaUpgrade, container);
    CreateMetaShopItem(metaSprite, "Health Upgrade: +5", 2, healthMetaUpgrade, container);
  }

  public void CreateMetaShopItem<T>(Sprite itemSprite, string upgradeName, int positionIndex, T upgrade, Transform parentContainer)
    where T : class
  {
    Transform shopItemTransform = Instantiate(template, parentContainer);
    shopItemTransform.gameObject.SetActive(true);
    RectTransform shopRectTransform = shopItemTransform.GetComponent<RectTransform>();

    float initialHeight = -300;
    float heightOffset = 130f;
    shopRectTransform.anchoredPosition = new Vector2(0, -initialHeight - (heightOffset * positionIndex));

    // Set image and name
    shopItemTransform.Find("MetaShopUpgradeImage").GetComponent<Image>().sprite = itemSprite;

    

    shopItemTransform.Find("MetaShopUpgradeName").GetComponent<TextMeshProUGUI>().SetText(upgradeName);
    TextMeshProUGUI priceText = shopItemTransform.Find("MetaShopUpgradePrice").GetComponent<TextMeshProUGUI>();

    // Buy button logic
    Button buyButton = shopItemTransform.Find("MetaShopUpgradeBuyButton").GetComponent<Button>();
    buyButton.onClick.RemoveAllListeners();

    if (upgrade is SpeedMetaUpgrade speedUpgrade)
    {
      int upgradePrice = speedUpgrade.GetUpgradeCost();
      priceText.SetText(upgradePrice.ToString());
      buyButton.onClick.AddListener(() => {
        BuySpeedUpgrade(speedUpgrade);
        
      });
    }
    else if (upgrade is DamageMetaUpgrade damageUpgrade)
    {
      int upgradePrice = damageUpgrade.GetUpgradeCost();
      priceText.SetText(upgradePrice.ToString());
      buyButton.onClick.AddListener(() => {
        BuyDamageUpgrade(damageUpgrade);
        
      });
    }
    else if (upgrade is HeathMetaUpgrade healthUpgrade)
    {
      int upgradePrice = healthUpgrade.GetUpgradeCost();
      priceText.SetText(upgradePrice.ToString());
      buyButton.onClick.AddListener(() => {
        BuyHealthUpgrade(healthUpgrade);
        
      });
    }

  }

  public void BuySpeedUpgrade(SpeedMetaUpgrade upgrade)
  {
    upgrade.BuyUpgrade();


  }
  public void BuyDamageUpgrade(DamageMetaUpgrade upgrade)
  {
    upgrade.BuyUpgrade();

  }
  public void BuyHealthUpgrade(HeathMetaUpgrade upgrade)
  {
    upgrade.BuyUpgrade();


  }


}
