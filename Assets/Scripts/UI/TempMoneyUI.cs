using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void SetMoney(int money)
    {
        moneyText.text = money.ToString();
    }
}
