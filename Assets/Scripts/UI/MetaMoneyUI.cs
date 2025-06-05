using TMPro;
using UnityEngine;

public class MetaMoney : MonoBehaviour
{
  public TextMeshProUGUI metaMoneyText;
  public PlayerResources playerResources;

  private const string MetaMoneyKey = "MetaMoney";

  public void SetMetaMoney(int money)
  {
    metaMoneyText.text = string.Format("{0}", money);
  }

  public void AddMetaMoney(int amount)
  {
    if (playerResources == null)
    {
      Debug.LogError("PlayerResources reference not set in MetaMoney!");
      return;
    }

    // Update the player's metaMoney
    playerResources.metaMoney += amount;

    // Save to PlayerPrefs for persistence between games
    PlayerPrefs.SetInt(MetaMoneyKey, playerResources.metaMoney);
    PlayerPrefs.Save();

    // Update the UI
    SetMetaMoney(playerResources.metaMoney);
  }
}
