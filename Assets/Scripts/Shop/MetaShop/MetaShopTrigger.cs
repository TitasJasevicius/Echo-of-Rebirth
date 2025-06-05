using UnityEngine;
using TMPro;

public class MetaShopTrigger : MonoBehaviour
{
  [SerializeField] public TextMeshProUGUI metaShopMessageText;
  [SerializeField] public MetaShopUI metaShopUI;

  private bool playerInTrigger = false;

  private void Start()
  {
    if (metaShopMessageText != null)
    {
      metaShopMessageText.gameObject.SetActive(false);
    }
  }

  private void Update()
  {
    if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
    {
      if (metaShopUI != null)
      {
        if (metaShopUI.container.gameObject.activeSelf)
        {
          metaShopUI.Hide();
        }
        else
        {
          metaShopUI.Show();
        }
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      playerInTrigger = true;

      if (metaShopMessageText != null)
      {
        metaShopMessageText.text = "Press E to open the meta shop";
        metaShopMessageText.gameObject.SetActive(true);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      playerInTrigger = false;

      if (metaShopMessageText != null)
      {
        metaShopMessageText.gameObject.SetActive(false);
      }

      if (metaShopUI != null && metaShopUI.container.gameObject.activeSelf)
      {
        metaShopUI.Hide();
      }
    }
  }
}
