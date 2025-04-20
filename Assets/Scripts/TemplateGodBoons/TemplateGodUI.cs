using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class TemplateGodUI : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public Transform container;
  public Transform template;

  [SerializeField] public BoonAssets boonAssets;
  [SerializeField] public PlayerResources playerResources;
  [SerializeField] public InariBoons inariBoons;
  [SerializeField] public BishamontenBoons bishamontenBoons;
  [SerializeField] private Transform inariBoonsContainer;
  [SerializeField] private Transform bishamontenBoonsContainer;

  //Inari boons
  [SerializeField] public BanteringBoon banteringBoon;
  [SerializeField] public EasyFindBoon easyFindBoon;
  [SerializeField] public FoxsLuckBoon foxsLuckBoon;
  [SerializeField] public LuckyStrikesBoon luckyStrikesBoon;
  [SerializeField] public ResoursfulnesBoon resoursfulnesBoon;

  //Bishamonten boons
  [SerializeField] public ReguvinationBoon reguvinationBoon;
  [SerializeField] public ResolveBoon resolveBoon;
  [SerializeField] public QuickfeetBoon quickfeetBoon;
  [SerializeField] public ParryBoon parryBoon;
  [SerializeField] public StaminaBoon staminaBoon;

  private Dictionary<InariBoons.InariBoonType, Func<int>> inariBoonPriceGetters;
  private Dictionary<BishamontenBoons.BishamontenBoonType, Func<int>> bishamontenBoonPriceGetters;

  private Dictionary<InariBoons.InariBoonType, Action> inariBoonBuyActions;
  private Dictionary<BishamontenBoons.BishamontenBoonType, Action> bishamontenBoonBuyActions;

  public List<(TextMeshProUGUI priceText, InariBoons.InariBoonType boonType)> inariBoonPriceTexts = new List<(TextMeshProUGUI, InariBoons.InariBoonType)>();
  public List<(TextMeshProUGUI priceText, BishamontenBoons.BishamontenBoonType boonType)> bishamontenBoonPriceTexts = new List<(TextMeshProUGUI, BishamontenBoons.BishamontenBoonType)>();
  public void Awake()
  {
    container = transform.Find("Container");
    template = container.Find("GodBoonTemplate");
    template.gameObject.SetActive(false);

    InitializeBoonPriceGetters();
    InitializeBoonBuyActions();
  }

  //If the boon price changes, it doesnt update the price in the UI right now
  public void Start()
  {
    Hide();

    //Spawns all inari boons
    SpawnInariBoons();
    SpawnBishamontenBoons();

    

  }

  public void Update()
  {

    if(Input.GetKeyDown(KeyCode.L))
    {
      //BuyBoon(InariBoons.InariBoonType.BanteringBoon);
      BuyQuickfeetBoon();
    }


  }

  public void SpawnInariBoons()
  {
    //Default sprite, change later
    Sprite resourceSprite = boonAssets.GetMoneySprite();
    Sprite luckyStrikeSprite = boonAssets.GetMoneySprite();
    Sprite foxLuckSprite = boonAssets.GetMoneySprite();
    Sprite easyFindSprite = boonAssets.GetMoneySprite();
    Sprite banteringSprite = boonAssets.GetMoneySprite();

    CreateBoonButton(resourceSprite, "Increase healing items effectivness by 10%", resoursfulnesBoon.GetPrice(), 1, InariBoons.InariBoonType.ResoursfulnesBoon, inariBoonsContainer);
    CreateBoonButton(luckyStrikeSprite, "Increase your crit chance by 5%", luckyStrikesBoon.GetPrice(), 2, InariBoons.InariBoonType.LuckyStrikesBoon, inariBoonsContainer);
    CreateBoonButton(foxLuckSprite, "Set your life steal to 5% while above 50% hp", foxsLuckBoon.GetPrice(), 3, InariBoons.InariBoonType.FoxsLuckBoon, inariBoonsContainer);
    CreateBoonButton(easyFindSprite, "Increase gold dropped from monsters by 10%", easyFindBoon.GetPrice(), 4, InariBoons.InariBoonType.EasyFindBoon, inariBoonsContainer);
    CreateBoonButton(banteringSprite, "Decrease shop prices by 20%", banteringBoon.GetPrice(), 5, InariBoons.InariBoonType.BanteringBoon, inariBoonsContainer);
  }

  public void SpawnBishamontenBoons()
  {
    Sprite staminaSprite = boonAssets.GetMoneySprite();
    Sprite parrySprite = boonAssets.GetMoneySprite();
    Sprite quickfeetSprite = boonAssets.GetMoneySprite();
    Sprite resolveSprite= boonAssets.GetMoneySprite();
    Sprite reguvinationSprite = boonAssets.GetMoneySprite();

    CreateBoonButton(staminaSprite, "Decrease your dash cooldown by 3 seconds", staminaBoon.GetPrice(), 1, BishamontenBoons.BishamontenBoonType.StaminaBoon, bishamontenBoonsContainer);
    CreateBoonButton(parrySprite, "Activate parry ability and increase your base damage by 10", parryBoon.GetPrice(), 2, BishamontenBoons.BishamontenBoonType.ParryBoon, bishamontenBoonsContainer);
    CreateBoonButton(quickfeetSprite, "Increase your invulnerability frames by 4", quickfeetBoon.GetPrice(), 3, BishamontenBoons.BishamontenBoonType.QuickfeetBoon, bishamontenBoonsContainer);
    CreateBoonButton(resolveSprite, "Reduce incoming damage by 10% while below 50% hp", resolveBoon.GetPrice(), 4, BishamontenBoons.BishamontenBoonType.ResolveBoon, bishamontenBoonsContainer);
    CreateBoonButton(reguvinationSprite, "Regen 10 hp every second for 5 seconds while below 50% hp", reguvinationBoon.GetPrice(), 5, BishamontenBoons.BishamontenBoonType.ReguvinationBoon, bishamontenBoonsContainer);


  }


  public void CreateBoonButton<T>(Sprite boonSprite, string boonName, int boonCost, int positionIndex, T boonType, Transform parentContainer) where T : Enum
  {
    Transform boonTransform = Instantiate(template, parentContainer); // Use the parent container
    boonTransform.gameObject.SetActive(true);
    RectTransform boonRectTransform = boonTransform.GetComponent<RectTransform>();

    float initialHeight = -400;
    float heightOffset = 130f;
    boonRectTransform.anchoredPosition = new Vector2(0, -initialHeight - (heightOffset * positionIndex));

    TextMeshProUGUI priceText = boonTransform.Find("GodBoonPrice").GetComponent<TextMeshProUGUI>();
    priceText.SetText(boonCost.ToString());

    boonTransform.Find("GodBoonName").GetComponent<TextMeshProUGUI>().SetText(boonName);
    boonTransform.Find("GodBoonImage").GetComponent<Image>().sprite = boonSprite;

    Button buyButton = boonTransform.Find("GodBuyButton").GetComponent<Button>();
    buyButton.onClick.RemoveAllListeners();

    // Add listeners based on the boon type (logic remains unchanged)
    if (typeof(T) == typeof(InariBoons.InariBoonType))
    {
      var inariBoon = (InariBoons.InariBoonType)(object)boonType;
      switch (inariBoon)
      {
        case InariBoons.InariBoonType.BanteringBoon:
          buyButton.onClick.AddListener(BuyBanteringBoon);
          break;
        case InariBoons.InariBoonType.EasyFindBoon:
          buyButton.onClick.AddListener(BuyEasyFindBoon);
          break;
        case InariBoons.InariBoonType.FoxsLuckBoon:
          buyButton.onClick.AddListener(BuyFoxsLuckBoon);
          break;
        case InariBoons.InariBoonType.LuckyStrikesBoon:
          buyButton.onClick.AddListener(BuyLuckyStrikesBoon);
          break;
        case InariBoons.InariBoonType.ResoursfulnesBoon:
          buyButton.onClick.AddListener(BuyResoursfulnesBoon);
          break;
      }
    }
    else if (typeof(T) == typeof(BishamontenBoons.BishamontenBoonType))
    {
      var bishamontenBoon = (BishamontenBoons.BishamontenBoonType)(object)boonType;
      switch (bishamontenBoon)
      {
        case BishamontenBoons.BishamontenBoonType.StaminaBoon:
          buyButton.onClick.AddListener(BuyStaminaBoon);
          break;
        case BishamontenBoons.BishamontenBoonType.QuickfeetBoon:
          buyButton.onClick.AddListener(BuyQuickfeetBoon);
          break;
        case BishamontenBoons.BishamontenBoonType.ResolveBoon:
          buyButton.onClick.AddListener(BuyResolveBoon);
          break;
        case BishamontenBoons.BishamontenBoonType.ReguvinationBoon:
          buyButton.onClick.AddListener(BuyReguvinationBoon);
          break;
        case BishamontenBoons.BishamontenBoonType.ParryBoon:
          buyButton.onClick.AddListener(BuyParryBoon);
          break;
      }
    }
  }

  public void BuyBoon<T>(T boonType) where T : Enum
  {
    if (typeof(T) == typeof(InariBoons.InariBoonType))
    {
      var inariBoon = (InariBoons.InariBoonType)(object)boonType;
      if (inariBoonBuyActions.TryGetValue(inariBoon, out var action))
      {
        action.Invoke();
      }
      else
      {
        Debug.LogWarning($"No buy action found for Inari boon type: {inariBoon}");
      }
    }
    else if (typeof(T) == typeof(BishamontenBoons.BishamontenBoonType))
    {
      var bishamontenBoon = (BishamontenBoons.BishamontenBoonType)(object)boonType;
      if (bishamontenBoonBuyActions.TryGetValue(bishamontenBoon, out var action))
      {
        action.Invoke();
      }
      else
      {
        Debug.LogWarning($"No buy action found for Bishamonten boon type: {bishamontenBoon}");
      }
    }
  }
  public void UpdateBoonPrices()
  {
    foreach (var (priceText, boonType) in inariBoonPriceTexts)
    {
      if (inariBoonPriceGetters.TryGetValue(boonType, out var getPrice))
      {
        int updatedPrice = getPrice.Invoke();
        Debug.Log($"Updating price for Inari boon {boonType}: {updatedPrice}");
        priceText.SetText(updatedPrice.ToString());
      }
      else
      {
        Debug.LogWarning($"No price getter found for Inari boon type: {boonType}");
      }
    }

    // Update Bishamonten Boon Prices
    foreach (var (priceText, boonType) in bishamontenBoonPriceTexts)
    {
      if (bishamontenBoonPriceGetters.TryGetValue(boonType, out var getPrice))
      {
        int updatedPrice = getPrice.Invoke();
        Debug.Log($"Updating price for Bishamonten boon {boonType}: {updatedPrice}");
        priceText.SetText(updatedPrice.ToString());
      }
      else
      {
        Debug.LogWarning($"No price getter found for Bishamonten boon type: {boonType}");
      }
    }
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

  public void ShowContainer(Transform container)
  {
    // Hide all containers first
    inariBoonsContainer.gameObject.SetActive(false);
    bishamontenBoonsContainer.gameObject.SetActive(false);

    // Show the specified container
    container.gameObject.SetActive(true);
  }

  public void HideContainer(Transform container)
  {
    // Hide the specified container
    container.gameObject.SetActive(false);
  }

  public void BuyBanteringBoon()
  {
    BuyBoon(InariBoons.InariBoonType.BanteringBoon);
  }

  public void BuyEasyFindBoon()
  {
    BuyBoon(InariBoons.InariBoonType.EasyFindBoon);
  }

  public void BuyFoxsLuckBoon()
  {
    BuyBoon(InariBoons.InariBoonType.FoxsLuckBoon);
  }

  public void BuyLuckyStrikesBoon()
  {
    BuyBoon(InariBoons.InariBoonType.LuckyStrikesBoon);
  }

  public void BuyResoursfulnesBoon()
  {
    BuyBoon(InariBoons.InariBoonType.ResoursfulnesBoon);
  }
  public void BuyStaminaBoon()
  {
    BuyBoon(BishamontenBoons.BishamontenBoonType.StaminaBoon);
  }
  public void BuyQuickfeetBoon()
  {
    BuyBoon(BishamontenBoons.BishamontenBoonType.QuickfeetBoon);
  }
  public void BuyResolveBoon()
  {
    BuyBoon(BishamontenBoons.BishamontenBoonType.ResolveBoon);
  }
  public void BuyReguvinationBoon()
  {
    BuyBoon(BishamontenBoons.BishamontenBoonType.ReguvinationBoon);
  }
  public void BuyParryBoon()
  {
    BuyBoon(BishamontenBoons.BishamontenBoonType.ParryBoon);
  }


  private void InitializeBoonPriceGetters()
  {
    // Inari Boons
    inariBoonPriceGetters = new Dictionary<InariBoons.InariBoonType, Func<int>>
    {
        { InariBoons.InariBoonType.BanteringBoon, () => banteringBoon.GetPrice() },
        { InariBoons.InariBoonType.EasyFindBoon, () => easyFindBoon.GetPrice() },
        { InariBoons.InariBoonType.FoxsLuckBoon, () => foxsLuckBoon.GetPrice() },
        { InariBoons.InariBoonType.LuckyStrikesBoon, () => luckyStrikesBoon.GetPrice() },
        { InariBoons.InariBoonType.ResoursfulnesBoon, () => resoursfulnesBoon.GetPrice() }
    };

    // Bishamonten Boons
    bishamontenBoonPriceGetters = new Dictionary<BishamontenBoons.BishamontenBoonType, Func<int>>
    {
        { BishamontenBoons.BishamontenBoonType.StaminaBoon, () => staminaBoon.GetPrice() },
        { BishamontenBoons.BishamontenBoonType.QuickfeetBoon, () => quickfeetBoon.GetPrice() },
        { BishamontenBoons.BishamontenBoonType.ResolveBoon, () => resolveBoon.GetPrice() },
        { BishamontenBoons.BishamontenBoonType.ReguvinationBoon, () => reguvinationBoon.GetPrice() },
        { BishamontenBoons.BishamontenBoonType.ParryBoon, () => parryBoon.GetPrice() }
    };
  }

  private void InitializeBoonBuyActions()
  {
    // Inari Boons
    inariBoonBuyActions = new Dictionary<InariBoons.InariBoonType, Action>
    {
        { InariBoons.InariBoonType.BanteringBoon, () => {
            banteringBoon.SetPriceDecreasePercentage(20);
            banteringBoon.ApplyBoon(playerResources);
            UpdateBoonPrices();
        }},
        { InariBoons.InariBoonType.EasyFindBoon, () => {
            easyFindBoon.SetGoldFindMultilplier(10f);
            easyFindBoon.ApplyBoon(playerResources);
        }},
        { InariBoons.InariBoonType.FoxsLuckBoon, () => {
            foxsLuckBoon.SetLifeStealMultiplier(5f);
            foxsLuckBoon.ApplyBoon(playerResources);
        }},
        { InariBoons.InariBoonType.LuckyStrikesBoon, () => {
            luckyStrikesBoon.SetBaseCriticalHitChanceMultiplier(5f);
            luckyStrikesBoon.ApplyBoon(playerResources);
        }},
        { InariBoons.InariBoonType.ResoursfulnesBoon, () => {
            resoursfulnesBoon.SetBaseHealingMultiplier(10f);
            resoursfulnesBoon.ApplyBoon(playerResources);
        }}
    };

    // Bishamonten Boons
    bishamontenBoonBuyActions = new Dictionary<BishamontenBoons.BishamontenBoonType, Action>
    {
        { BishamontenBoons.BishamontenBoonType.StaminaBoon, () => {
            staminaBoon.SetCooldownReduction(3);
            staminaBoon.ApplyBoon(playerResources);
        }},
        { BishamontenBoons.BishamontenBoonType.QuickfeetBoon, () => {
            quickfeetBoon.SetInvFrameCount(4);
            quickfeetBoon.ApplyBoon(playerResources);
        }},
        { BishamontenBoons.BishamontenBoonType.ResolveBoon, () => {
            resolveBoon.SetDamageReductionMultiplier(10.0f);
            resolveBoon.ApplyBoon(playerResources);
        }},
        { BishamontenBoons.BishamontenBoonType.ReguvinationBoon, () => {
            reguvinationBoon.SetRegenerationDuration(5);
            reguvinationBoon.SetRegenerationAmount(10);
            reguvinationBoon.ApplyBoon(playerResources);
        }},
        { BishamontenBoons.BishamontenBoonType.ParryBoon, () => {
            parryBoon.SetBaseAttackDamageIncrease(10);
            parryBoon.ApplyBoon(playerResources);
        }}
    };
  }

}
