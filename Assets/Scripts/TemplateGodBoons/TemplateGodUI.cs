using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class TemplateGodUI : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  private Transform container;
  private Transform template;

  [SerializeField] public BoonAssets boonAssets;
  [SerializeField] public PlayerResources playerResources;
  [SerializeField] public InariBoons inariBoons;

  [SerializeField] public BanteringBoon banteringBoon;
  [SerializeField] public EasyFindBoon easyFindBoon;
  [SerializeField] public FoxsLuckBoon foxsLuckBoon;
  [SerializeField] public LuckyStrikesBoon luckyStrikesBoon;
  [SerializeField] public ResoursfulnesBoon resoursfulnesBoon;

  private void Awake()
  {
    container = transform.Find("Container");
    template = container.Find("GodBoonTemplate");
    template.gameObject.SetActive(false);
  }

  //If the boon price changes, it doesnt update the price in the UI right now
  private void Start()
  {
    Hide();
    //Default sprite, change later
    Sprite resourceSprite = boonAssets.GetMoneySprite();
    Sprite luckyStrikeSprite = boonAssets.GetMoneySprite();
    Sprite foxLuckSprite = boonAssets.GetMoneySprite();
    Sprite easyFindSprite = boonAssets.GetMoneySprite();
    Sprite banteringSprite = boonAssets.GetMoneySprite();
    
    CreateBoonButton(resourceSprite, "Increase healing items effectivness by 10%", resoursfulnesBoon.GetPrice(), 1, InariBoons.InariBoonType.ResoursfulnesBoon);
    CreateBoonButton(luckyStrikeSprite, "Increase your crit chance by 5%", luckyStrikesBoon.GetPrice(), 2, InariBoons.InariBoonType.LuckyStrikesBoon);
    CreateBoonButton(foxLuckSprite, "Set your life steal to 5% while above 50% hp", foxsLuckBoon.GetPrice(), 3, InariBoons.InariBoonType.FoxsLuckBoon);
    CreateBoonButton(easyFindSprite, "Increase gold dropped from monsters by 10%", easyFindBoon.GetPrice(), 4, InariBoons.InariBoonType.EasyFindBoon);
    CreateBoonButton(banteringSprite, "Decrease shop prices by 20%", banteringBoon.GetPrice(), 5, InariBoons.InariBoonType.BanteringBoon);
  }

  public void CreateBoonButton(Sprite boonSprite, string boonName, int boonCost, int positionIndex, InariBoons.InariBoonType boonType)
  {
    Transform boonTransform = Instantiate(template, container);
    boonTransform.gameObject.SetActive(true);
    RectTransform boonRectTransform = boonTransform.GetComponent<RectTransform>();

    float initialHeight = -400;
    float heightOffset = 130f;
    boonRectTransform.anchoredPosition = new Vector2(0, -initialHeight-(heightOffset * positionIndex));

    boonRectTransform.Find("GodBoonPrice").GetComponent<TextMeshProUGUI>().SetText(boonCost.ToString());
    boonRectTransform.Find("GodBoonName").GetComponent<TextMeshProUGUI>().SetText(boonName);
    boonRectTransform.Find("GodBoonImage").GetComponent<Image>().sprite = boonSprite;

    Button buyButton = boonRectTransform.Find("BuyButton").GetComponent<Button>();
    buyButton.onClick.AddListener(() => BuyBoon(boonType));

  }

  private void BuyBoon(InariBoons.InariBoonType boonType)
  {
    switch (boonType)
    {
      case InariBoons.InariBoonType.BanteringBoon:
        banteringBoon.SetPriceDecreasePercentage(20);
        banteringBoon.ApplyBoon(playerResources);
        break;
      case InariBoons.InariBoonType.EasyFindBoon:
        easyFindBoon.SetGoldFindMultilplier(10f);
        easyFindBoon.ApplyBoon(playerResources);
        break;
      case InariBoons.InariBoonType.FoxsLuckBoon:      
        foxsLuckBoon.SetLifeStealMultiplier(5f);
        foxsLuckBoon.ApplyBoon(playerResources);
        break;
      case InariBoons.InariBoonType.LuckyStrikesBoon:
        luckyStrikesBoon.SetBaseCriticalHitChanceMultiplier(5f);
        luckyStrikesBoon.ApplyBoon(playerResources);
        break;
      case InariBoons.InariBoonType.ResoursfulnesBoon:
        resoursfulnesBoon.SetBaseHealingMultiplier(10f);
        resoursfulnesBoon.ApplyBoon(playerResources);
        break;
    }
  }

  public void Show()
  {
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}
