using UnityEngine;

public class BoonShopTrigger : MonoBehaviour
{
  [SerializeField] public TemplateGodUI templateGodUI;
  [SerializeField] public Transform containerToShow;

  private bool playerInTrigger = false; 
  private bool containerVisible = false; 
  private void Update()
  {
    
    if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
    {
      if (containerVisible)
      {
        
        templateGodUI.HideContainer(containerToShow);
        containerVisible = false; 
      }
      else
      {
        
        templateGodUI.ShowContainer(containerToShow);
        containerVisible = true;
      }
    }
  }

  public void OnTriggerEnter2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      playerInTrigger = true; 
    }
  }

  public void OnTriggerExit2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      playerInTrigger = false; 
      if (containerVisible)
      {
        templateGodUI.HideContainer(containerToShow); 
        containerVisible = false; 
      }
    }
  }
}
