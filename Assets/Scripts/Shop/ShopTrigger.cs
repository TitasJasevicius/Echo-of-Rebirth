using UnityEngine;
using TMPro;

public class ShopTrigger : MonoBehaviour
{
  [SerializeField] public TextMeshProUGUI shopMessageText; 
  [SerializeField] public ShopUI shopUI; 

  private bool playerInTrigger = false;

  private void Start()
  {
    if (shopMessageText != null)
    {
      shopMessageText.gameObject.SetActive(false); 
    }
  }

  private void Update()
  {
    if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
    {
      if (shopUI != null)
      {
        if (shopUI.container.gameObject.activeSelf)
        {
          shopUI.Hide(); 
        }
        else
        {
          shopUI.Show(); 
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

      if (shopMessageText != null)
      {
        shopMessageText.text = "Press E to open the shop";
        shopMessageText.gameObject.SetActive(true); 
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      playerInTrigger = false;

      if (shopMessageText != null)
      {
        shopMessageText.gameObject.SetActive(false); 
      }

      if (shopUI != null && shopUI.container.gameObject.activeSelf)
      {
        shopUI.Hide(); 
      }
    }
  }
}
