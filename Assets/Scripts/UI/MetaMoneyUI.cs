using TMPro;
using UnityEngine;

public class MetaMoney : MonoBehaviour
{
    public TextMeshProUGUI metaMoneyText;

    public void SetMetaMoney(int money)
    {
        metaMoneyText.text = string.Format("Meta: {0}", money);
    }
}
