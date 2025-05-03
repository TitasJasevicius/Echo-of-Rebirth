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

  private Image godBoonBackground;
  private Image godBoonShopTitle;
  private TextMeshProUGUI godBoonShopText;

  [SerializeField] public BoonAssets boonAssets;
  [SerializeField] public PlayerResources playerResources;
  [SerializeField] public PlayerMovement playerMovement;

  //Boons containers
  [SerializeField] private Transform inariBoonsContainer;
  [SerializeField] private Transform bishamontenBoonsContainer;
  [SerializeField] private Transform amaterasuBoonsContainer;
  [SerializeField] private Transform tsukuyomiBoonsContainer;
  [SerializeField] private Transform hachimanBoonsContainer;

  //probably just rewrite this to use the class that holds all boons at some time...
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

  //Amaterasu boons
  [SerializeField] public BlindingLight blindingLight;
  [SerializeField] public BurningBlade burningBlade;
  [SerializeField] public LightsSpeed lightsSpeed;
  [SerializeField] public MorningSunshine morningSunshine;
  [SerializeField] public SunsReach sunsReach;

  //Tsukuyomi boons
  [SerializeField] public MoonBlade moonBlade;
  [SerializeField] public ShadowDash shadowDash;
  [SerializeField] public SecondaryLight secondaryLight;
  [SerializeField] public FullMoon fullMoon;
  [SerializeField] public MoonlightTrickery moonlightTrickery;

  //Hachiman Boons
  [SerializeField] public Focus focus;
  [SerializeField] public Mastery mastery;
  [SerializeField] public Bloodthirst bloodthirst;
  [SerializeField] public GrandStand grandStand;
  [SerializeField] public ViciousAttacks viciousAttacks;

  private Dictionary<InariBoons.InariBoonType, Func<int>> inariBoonPriceGetters;
  private Dictionary<BishamontenBoons.BishamontenBoonType, Func<int>> bishamontenBoonPriceGetters;
  private Dictionary<AmaterasuBoons.AmaterasuBoonType, Func<int>> amaterasuBoonPriceGetters;
  private Dictionary<TsukuyomiBoons.TsukuyomiBoonType, Func<int>> tsukuyomiBoonPriceGetters;
  private Dictionary<HachimanBoons.HachimanBoonType, Func<int>> hachimanBoonPriceGetters;


  private Dictionary<InariBoons.InariBoonType, Action> inariBoonBuyActions;
  private Dictionary<BishamontenBoons.BishamontenBoonType, Action> bishamontenBoonBuyActions;
  private Dictionary<AmaterasuBoons.AmaterasuBoonType, Action> amaterasuBoonBuyActions;
  private Dictionary<TsukuyomiBoons.TsukuyomiBoonType, Action> tsukuyomiBoonBuyActions;
  private Dictionary<HachimanBoons.HachimanBoonType, Action> hachimanBoonBuyActions;

  public List<(TextMeshProUGUI priceText, InariBoons.InariBoonType boonType)> inariBoonPriceTexts = new List<(TextMeshProUGUI, InariBoons.InariBoonType)>();
  public List<(TextMeshProUGUI priceText, BishamontenBoons.BishamontenBoonType boonType)> bishamontenBoonPriceTexts = new List<(TextMeshProUGUI, BishamontenBoons.BishamontenBoonType)>();
  public List<(TextMeshProUGUI priceText, AmaterasuBoons.AmaterasuBoonType boonType)> amaterasuBoonPriceTexts = new List<(TextMeshProUGUI, AmaterasuBoons.AmaterasuBoonType)>();
  public List<(TextMeshProUGUI priceText, TsukuyomiBoons.TsukuyomiBoonType boonType)> tsukuyomiBoonPriceTexts = new List<(TextMeshProUGUI, TsukuyomiBoons.TsukuyomiBoonType)>();
  public List<(TextMeshProUGUI priceText, HachimanBoons.HachimanBoonType boonType)> HachimanBoonPriceTexts = new List<(TextMeshProUGUI, HachimanBoons.HachimanBoonType)>();


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

    //Spawns all boons
    SpawnInariBoons();
    SpawnBishamontenBoons();
    SpawnAmaterasuBoons();
    SpawnTsukuyomiBoons();
    SpawnHachimanBoons();



  }

  public void Update()
  {

    if(Input.GetKeyDown(KeyCode.L))
    {
      //BuyBoon(InariBoons.InariBoonType.BanteringBoon);
      //BuyQuickfeetBoon();
      //BuyLightsSpeedBoon();
      BuyFoxsLuckBoon(); 
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

    Sprite inariBackground = boonAssets.InariShopBackground;


    CreateBackgroundTitle(inariBackground, "Inari boons", inariBoonsContainer);

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

    Sprite bishamontenBackground = boonAssets.BishamontenShopBackground;
    CreateBackgroundTitle(bishamontenBackground, "Bishamonten boons", bishamontenBoonsContainer);

    CreateBoonButton(staminaSprite, "Decrease your dash cooldown by 3 seconds", staminaBoon.GetPrice(), 1, BishamontenBoons.BishamontenBoonType.StaminaBoon, bishamontenBoonsContainer);
    CreateBoonButton(parrySprite, "Activate parry ability and increase your base damage by 10", parryBoon.GetPrice(), 2, BishamontenBoons.BishamontenBoonType.ParryBoon, bishamontenBoonsContainer);
    CreateBoonButton(quickfeetSprite, "Increase your invulnerability frames by 4", quickfeetBoon.GetPrice(), 3, BishamontenBoons.BishamontenBoonType.QuickfeetBoon, bishamontenBoonsContainer);
    CreateBoonButton(resolveSprite, "Reduce incoming damage by 10% while below 50% hp", resolveBoon.GetPrice(), 4, BishamontenBoons.BishamontenBoonType.ResolveBoon, bishamontenBoonsContainer);
    CreateBoonButton(reguvinationSprite, "Regen 10 hp every second for 5 seconds while below 50% hp", reguvinationBoon.GetPrice(), 5, BishamontenBoons.BishamontenBoonType.ReguvinationBoon, bishamontenBoonsContainer);
  }
  public void SpawnAmaterasuBoons()
  {
    Sprite blindingLightSprite = boonAssets.GetMoneySprite();
    Sprite burningBladeSprite = boonAssets.GetMoneySprite();
    Sprite lightsSpeedSprite = boonAssets.GetMoneySprite();
    Sprite morningSunshineSprite = boonAssets.GetMoneySprite();
    Sprite sunsReachSprite = boonAssets.GetMoneySprite();

    Sprite amaterasuBackground = boonAssets.AmaterasuShopBackground;

    CreateBackgroundTitle(amaterasuBackground, "Amaterasu boons", amaterasuBoonsContainer);

    CreateBoonButton(blindingLightSprite, "After dashing gain 5% chance to cause enemies to miss 5% of attacks", blindingLight.GetPrice(), 1, AmaterasuBoons.AmaterasuBoonType.BlindingLight, amaterasuBoonsContainer);
    CreateBoonButton(burningBladeSprite, "Set your burning damage to 10 and burning duration to 5 seconds", burningBlade.GetPrice(), 2, AmaterasuBoons.AmaterasuBoonType.BurningBlade, amaterasuBoonsContainer);
    CreateBoonButton(lightsSpeedSprite, "Increase your speed by 20", lightsSpeed.GetPrice(), 3, AmaterasuBoons.AmaterasuBoonType.LightsSpeed, amaterasuBoonsContainer);
    CreateBoonButton(morningSunshineSprite, "Start every room with 100 shield", morningSunshine.GetPrice(), 4, AmaterasuBoons.AmaterasuBoonType.MorningSunshine, amaterasuBoonsContainer);
    CreateBoonButton(sunsReachSprite, "Decrease your attack damage by 5 but increase your attack range by 2", sunsReach.GetPrice(), 5, AmaterasuBoons.AmaterasuBoonType.SunsReach, amaterasuBoonsContainer);
  }

  public void SpawnTsukuyomiBoons()
  {
    Sprite moonBladeSprite = boonAssets.GetMoneySprite();
    Sprite shadowDashSprite = boonAssets.GetMoneySprite();
    Sprite secondaryLightSprite = boonAssets.GetMoneySprite();
    Sprite fullMoonSprite = boonAssets.GetMoneySprite();
    Sprite moonlightTrickerySprite = boonAssets.GetMoneySprite();

    Sprite tsukuyomiBackground = boonAssets.TsukoyamiShopBackground;

    CreateBackgroundTitle(tsukuyomiBackground, "Tsukuyomi boons", tsukuyomiBoonsContainer);

    CreateBoonButton(moonBladeSprite, "10% chance to put enemies to sleep for 5 seconds and increase base damage by 5", moonBlade.GetPrice(), 1, TsukuyomiBoons.TsukuyomiBoonType.MoonBlade, tsukuyomiBoonsContainer);
    CreateBoonButton(shadowDashSprite, "Empower your dash, every 15 seconds your dash gives you 5 seconds of invisibility", shadowDash.GetPrice(), 2, TsukuyomiBoons.TsukuyomiBoonType.ShadowDash, tsukuyomiBoonsContainer);
    CreateBoonButton(secondaryLightSprite, "Your attacks pierce enemies", secondaryLight.GetPrice(), 3, TsukuyomiBoons.TsukuyomiBoonType.SecondaryLight, tsukuyomiBoonsContainer);
    CreateBoonButton(fullMoonSprite, "Double your critical strike chance", fullMoon.GetPrice(), 4, TsukuyomiBoons.TsukuyomiBoonType.FullMoon, tsukuyomiBoonsContainer);
    CreateBoonButton(moonlightTrickerySprite, "Gain 5% chance to dodge enemy attacks", moonlightTrickery.GetPrice(), 5, TsukuyomiBoons.TsukuyomiBoonType.MoonlightTrickery, tsukuyomiBoonsContainer);
  }
  public void SpawnHachimanBoons()
  {
    Sprite focusSprite = boonAssets.GetMoneySprite();
    Sprite masterySprite = boonAssets.GetMoneySprite();
    Sprite bloodthirstSprite = boonAssets.GetMoneySprite();
    Sprite grandStandSprite = boonAssets.GetMoneySprite();
    Sprite viciousAttacksSprite = boonAssets.GetMoneySprite();

    Sprite hacimanBackground = boonAssets.HachimanShopBackground;


    CreateBackgroundTitle(hacimanBackground, "Hachiman boons", hachimanBoonsContainer);

    CreateBoonButton(focusSprite, "Increase your subsequent attacks damage on the same enemy by 5", focus.GetPrice(), 1, HachimanBoons.HachimanBoonType.Focus, hachimanBoonsContainer);
    CreateBoonButton(masterySprite, "Increase your critical damage by 20%", mastery.GetPrice(), 2, HachimanBoons.HachimanBoonType.Mastery, hachimanBoonsContainer);
    CreateBoonButton(bloodthirstSprite, "Increase your movement speed by 15% for each bleeding enemy alive", bloodthirst.GetPrice(), 3, HachimanBoons.HachimanBoonType.Bloodthirst, hachimanBoonsContainer);
    CreateBoonButton(grandStandSprite, "Increase your attack speed by 10% for each enemy alive", grandStand.GetPrice(), 4, HachimanBoons.HachimanBoonType.GrandStand, hachimanBoonsContainer);
    CreateBoonButton(viciousAttacksSprite, "Increase your base damage by 5, your attacks bleed enemies for 5 seconds for 5 damage every second", viciousAttacks.GetPrice(), 5, HachimanBoons.HachimanBoonType.ViciousAttacks, hachimanBoonsContainer);

  }
  public void CreateBackgroundTitle(Sprite backgroundSprite, string boonShopText, Transform parentContainer)
  {
    
    Transform godBoonBackgroundTransform = container.Find("GodBoonBackground");
    if (godBoonBackgroundTransform != null)
    {
      Transform clonedBackground = Instantiate(godBoonBackgroundTransform, parentContainer);
      clonedBackground.GetComponent<Image>().sprite = backgroundSprite;
    }

    
    Transform godBoonTitleTransform = container.Find("GodBoonShopTitle");
    Transform clonedTitle = null; 
    if (godBoonTitleTransform != null)
    {
      clonedTitle = Instantiate(godBoonTitleTransform, parentContainer);
      
    }

   
    if (clonedTitle != null) 
    {
      Transform godBoonShopTextTransform = clonedTitle.Find("GodBoonShopText");
      if (godBoonShopTextTransform != null)
      {
        godBoonShopTextTransform.GetComponent<TextMeshProUGUI>().SetText(boonShopText);
      }
    }
  }

  public void CreateBoonButton<T>(Sprite boonSprite, string boonName, int boonCost, int positionIndex, T boonType, Transform parentContainer) where T : Enum
  {
    Transform boonTransform = Instantiate(template, parentContainer); // Use the parent container
    boonTransform.gameObject.SetActive(true);
    RectTransform boonRectTransform = boonTransform.GetComponent<RectTransform>();


    float initialHeight = -480;
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
    else if (typeof(T) == typeof(AmaterasuBoons.AmaterasuBoonType))
    {
      var amaterasuBoon = (AmaterasuBoons.AmaterasuBoonType)(object)boonType;
      switch (amaterasuBoon)
      {
        case AmaterasuBoons.AmaterasuBoonType.BlindingLight:
          buyButton.onClick.AddListener(BuyBlindingLightBoon);
          break;
        case AmaterasuBoons.AmaterasuBoonType.BurningBlade:
          buyButton.onClick.AddListener(BuyBurningBladeBoon);
          break;
        case AmaterasuBoons.AmaterasuBoonType.LightsSpeed:
          buyButton.onClick.AddListener(BuyLightsSpeedBoon);
          break;
        case AmaterasuBoons.AmaterasuBoonType.MorningSunshine:
          buyButton.onClick.AddListener(BuyMorningSunshineBoon);
          break;
        case AmaterasuBoons.AmaterasuBoonType.SunsReach:
          buyButton.onClick.AddListener(BuySunsReachBoon);
          break;
      }
    }
    else if (typeof(T) == typeof(TsukuyomiBoons.TsukuyomiBoonType))
    {
      var tsukuyomiBoon = (TsukuyomiBoons.TsukuyomiBoonType)(object)boonType;
      switch (tsukuyomiBoon)
      {
        case TsukuyomiBoons.TsukuyomiBoonType.MoonBlade:
          buyButton.onClick.AddListener(BuyMoonBladeBoon);
          break;
        case TsukuyomiBoons.TsukuyomiBoonType.ShadowDash:
          buyButton.onClick.AddListener(BuyShadowDashBoon);
          break;
        case TsukuyomiBoons.TsukuyomiBoonType.SecondaryLight:
          buyButton.onClick.AddListener(BuySecondaryLightBoon);
          break;
        case TsukuyomiBoons.TsukuyomiBoonType.FullMoon:
          buyButton.onClick.AddListener(BuyFullMoonBoon);
          break;
        case TsukuyomiBoons.TsukuyomiBoonType.MoonlightTrickery:
          buyButton.onClick.AddListener(BuyMoonlightTrickeryBoon);
          break;
      }
    }
    else if (typeof(T) == typeof(HachimanBoons.HachimanBoonType))
    {
      var hachimanBoon = (HachimanBoons.HachimanBoonType)(object)boonType;
      switch (hachimanBoon)
      {
        case HachimanBoons.HachimanBoonType.Focus:
          buyButton.onClick.AddListener(BuyFocusBoon);
          break;
        case HachimanBoons.HachimanBoonType.Mastery:
          buyButton.onClick.AddListener(BuyMasteryBoon);
          break;
        case HachimanBoons.HachimanBoonType.Bloodthirst:
          buyButton.onClick.AddListener(BuyBloodthirstBoon);
          break;
        case HachimanBoons.HachimanBoonType.GrandStand:
          buyButton.onClick.AddListener(BuyGrandStandBoon);
          break;
        case HachimanBoons.HachimanBoonType.ViciousAttacks:
          buyButton.onClick.AddListener(BuyViciousAttacksBoon);
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
    else if (typeof(T) == typeof(AmaterasuBoons.AmaterasuBoonType))
    {
      var amaterasuBoon = (AmaterasuBoons.AmaterasuBoonType)(object)boonType;
      if (amaterasuBoonBuyActions.TryGetValue(amaterasuBoon, out var action))
      {
        action.Invoke();
      }
      else
      {
        Debug.LogWarning($"No buy action found for Amaterasu boon type: {amaterasuBoon}");
      }
    }
    else if (typeof(T) == typeof(TsukuyomiBoons.TsukuyomiBoonType))
    {
      var tsukuyomiBoon = (TsukuyomiBoons.TsukuyomiBoonType)(object)boonType;
      if (tsukuyomiBoonBuyActions.TryGetValue(tsukuyomiBoon, out var action))
      {
        action.Invoke();
      }
      else
      {
        Debug.LogWarning($"No buy action found for Tsukuyomi boon type: {tsukuyomiBoon}");
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
    // Update Amaterasu Boon Prices
    foreach (var (priceText, boonType) in amaterasuBoonPriceTexts)
    {
      if (amaterasuBoonPriceGetters.TryGetValue(boonType, out var getPrice))
      {
        int updatedPrice = getPrice.Invoke();
        Debug.Log($"Updating price for Amaterasu boon {boonType}: {updatedPrice}");
        priceText.SetText(updatedPrice.ToString());
      }
      else
      {
        Debug.LogWarning($"No price getter found for Amaterasu boon type: {boonType}");
      }
    }
    // Update Tsukuyomi Boon Prices
    foreach (var (priceText, boonType) in tsukuyomiBoonPriceTexts)
    {
      if (tsukuyomiBoonPriceGetters.TryGetValue(boonType, out var getPrice))
      {
        int updatedPrice = getPrice.Invoke();
        Debug.Log($"Updating price for Tsukuyomi boon {boonType}: {updatedPrice}");
        priceText.SetText(updatedPrice.ToString());
      }
      else
      {
        Debug.LogWarning($"No price getter found for Tsukuyomi boon type: {boonType}");
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
    amaterasuBoonsContainer.gameObject.SetActive(false);


    // Show the specified container
    container.gameObject.SetActive(true);
    foreach (Transform child in container)
    {
      child.gameObject.SetActive(true);
    }
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
  public void BuyBlindingLightBoon()
  {
    BuyBoon(AmaterasuBoons.AmaterasuBoonType.BlindingLight);
  }
  public void BuyBurningBladeBoon()
  {
    BuyBoon(AmaterasuBoons.AmaterasuBoonType.BurningBlade);
  }
  public void BuyLightsSpeedBoon()
  {
    BuyBoon(AmaterasuBoons.AmaterasuBoonType.LightsSpeed);
  }
  public void BuyMorningSunshineBoon()
  {
    BuyBoon(AmaterasuBoons.AmaterasuBoonType.MorningSunshine);
  }
  public void BuySunsReachBoon()
  {
    BuyBoon(AmaterasuBoons.AmaterasuBoonType.SunsReach);
  }
  public void BuyMoonBladeBoon()
  {
    BuyBoon(TsukuyomiBoons.TsukuyomiBoonType.MoonBlade);
  }
  public void BuyShadowDashBoon()
  {
    BuyBoon(TsukuyomiBoons.TsukuyomiBoonType.ShadowDash);
  }
  public void BuySecondaryLightBoon()
  {
    BuyBoon(TsukuyomiBoons.TsukuyomiBoonType.SecondaryLight);
  }
  public void BuyFullMoonBoon()
  {
    BuyBoon(TsukuyomiBoons.TsukuyomiBoonType.FullMoon);
  }
  public void BuyMoonlightTrickeryBoon()
  {
    BuyBoon(TsukuyomiBoons.TsukuyomiBoonType.MoonlightTrickery);
  }

  public void BuyFocusBoon()
  {
    BuyBoon(HachimanBoons.HachimanBoonType.Focus);
  }
  public void BuyMasteryBoon()
  {
    BuyBoon(HachimanBoons.HachimanBoonType.Mastery);
  }
  public void BuyBloodthirstBoon()
  {
    BuyBoon(HachimanBoons.HachimanBoonType.Bloodthirst);
  }
  public void BuyGrandStandBoon()
  {
    BuyBoon(HachimanBoons.HachimanBoonType.GrandStand);
  }
  public void BuyViciousAttacksBoon()
  {
    BuyBoon(HachimanBoons.HachimanBoonType.ViciousAttacks);
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
    // Amaterasu Boons
    amaterasuBoonPriceGetters = new Dictionary<AmaterasuBoons.AmaterasuBoonType, Func<int>>
    {
        { AmaterasuBoons.AmaterasuBoonType.BlindingLight, () => blindingLight.GetPrice() },
        { AmaterasuBoons.AmaterasuBoonType.BurningBlade, () => burningBlade.GetPrice() },
        { AmaterasuBoons.AmaterasuBoonType.LightsSpeed, () => lightsSpeed.GetPrice() },
        { AmaterasuBoons.AmaterasuBoonType.MorningSunshine, () => morningSunshine.GetPrice() },
        { AmaterasuBoons.AmaterasuBoonType.SunsReach, () => sunsReach.GetPrice() }
    };

    // Tsukuyomi Boons
    tsukuyomiBoonPriceGetters = new Dictionary<TsukuyomiBoons.TsukuyomiBoonType, Func<int>>
    {
        { TsukuyomiBoons.TsukuyomiBoonType.MoonBlade, () => moonBlade.GetPrice() },
        { TsukuyomiBoons.TsukuyomiBoonType.ShadowDash, () => shadowDash.GetPrice() },
        { TsukuyomiBoons.TsukuyomiBoonType.SecondaryLight, () => secondaryLight.GetPrice() },
        { TsukuyomiBoons.TsukuyomiBoonType.FullMoon, () => fullMoon.GetPrice() },
        { TsukuyomiBoons.TsukuyomiBoonType.MoonlightTrickery, () => moonlightTrickery.GetPrice() }
    };

    // Hachiman Boons
    hachimanBoonPriceGetters = new Dictionary<HachimanBoons.HachimanBoonType, Func<int>>
    {
        { HachimanBoons.HachimanBoonType.Focus, () => focus.GetPrice() },
        { HachimanBoons.HachimanBoonType.Mastery, () => mastery.GetPrice() },
        { HachimanBoons.HachimanBoonType.Bloodthirst, () => bloodthirst.GetPrice() },
        { HachimanBoons.HachimanBoonType.GrandStand, () => grandStand.GetPrice() },
        { HachimanBoons.HachimanBoonType.ViciousAttacks, () => viciousAttacks.GetPrice() }
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
    // Amaterasu Boons
    amaterasuBoonBuyActions = new Dictionary<AmaterasuBoons.AmaterasuBoonType, Action>
    {
        { AmaterasuBoons.AmaterasuBoonType.BlindingLight, () => {
            blindingLight.SetBlindChance(5);
            blindingLight.SetMissChance(5);
            blindingLight.ApplyBoon(playerResources);
        }},
        { AmaterasuBoons.AmaterasuBoonType.BurningBlade, () => {
            burningBlade.SetBurningDamage(10);
            burningBlade.SetBurningDuration(5);
            burningBlade.ApplyBoon(playerResources);
        }},
        { AmaterasuBoons.AmaterasuBoonType.LightsSpeed, () => {
            lightsSpeed.SetSpeedIncrease(20);
            lightsSpeed.ApplyBoon(playerResources, playerMovement);
        }},
        { AmaterasuBoons.AmaterasuBoonType.MorningSunshine, () => {
            morningSunshine.SetShieldValue(100);
            morningSunshine.ApplyBoon(playerResources);
        }},
        { AmaterasuBoons.AmaterasuBoonType.SunsReach, () => {
            sunsReach.SetAttackDamageDecrease(5);
            sunsReach.SetAttackRangeIncrease(2);
            sunsReach.ApplyBoon(playerResources);
        }}
    };

    // Tsukuyomi Boons
    tsukuyomiBoonBuyActions = new Dictionary<TsukuyomiBoons.TsukuyomiBoonType, Action>
    {
        { TsukuyomiBoons.TsukuyomiBoonType.MoonBlade, () => {
            moonBlade.SetSleepChance(10);
            moonBlade.SetSleepDuration(5);
            moonBlade.SetBaseDamageIncrease(5);
            moonBlade.ApplyBoon(playerResources);
        }},
        { TsukuyomiBoons.TsukuyomiBoonType.ShadowDash, () => {
            shadowDash.SetShadowDashCooldown(15);
            shadowDash.SetInvisibilityDuration(5);
            shadowDash.ApplyBoon(playerResources);
        }},
        { TsukuyomiBoons.TsukuyomiBoonType.SecondaryLight, () => {
            secondaryLight.ApplyBoon(playerResources);
        }},
        { TsukuyomiBoons.TsukuyomiBoonType.FullMoon, () => {
            fullMoon.SetCritChanceMultiplier(100); //2x
            fullMoon.ApplyBoon(playerResources);
        }},
        { TsukuyomiBoons.TsukuyomiBoonType.MoonlightTrickery, () => {
            moonlightTrickery.SetDodgeChance(5);
            moonlightTrickery.ApplyBoon(playerResources);
        }}
    };
    // Hachiman Boons
    hachimanBoonBuyActions = new Dictionary<HachimanBoons.HachimanBoonType, Action>
    {
        { HachimanBoons.HachimanBoonType.Focus, () => {
            focus.SetExtraDamage(5); 
            focus.ApplyBoon(playerResources);
        }},
        { HachimanBoons.HachimanBoonType.Mastery, () => {
            mastery.SetCritMultiplier(20.0f); 
            mastery.ApplyBoon(playerResources);
        }},
        { HachimanBoons.HachimanBoonType.Bloodthirst, () => {
            bloodthirst.SetMovementSpeedIncrease(15f); // Example speed increase
            bloodthirst.ApplyBoon(playerResources);
        }},
        { HachimanBoons.HachimanBoonType.GrandStand, () => {
            grandStand.SetAttackSpeedMultiplier(10.0f); // Example defense bonus
            grandStand.ApplyBoon(playerResources);
        }},
        { HachimanBoons.HachimanBoonType.ViciousAttacks, () => {
            viciousAttacks.SetBaseAttackDamageIncrease(5); // Example attack speed increase
            viciousAttacks.SetBleedDamage(10);
            viciousAttacks.SetBleedDuration(5);
            viciousAttacks.ApplyBoon(playerResources);
        }}
    };

  }

}
