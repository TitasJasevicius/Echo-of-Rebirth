using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void SetMoney(int money)
    {
        moneyText.text = money.ToString();
    }
}
