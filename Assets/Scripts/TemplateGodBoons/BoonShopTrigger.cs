using UnityEngine;

public class BoonShopTrigger : MonoBehaviour
{
  [SerializeField] public TemplateGodUI templateGodUI;
  [SerializeField] public Transform containerToShow;
  public void OnTriggerEnter2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      templateGodUI.ShowContainer(containerToShow);

    }

  }

  public void OnTriggerExit2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      templateGodUI.HideContainer(containerToShow);
    }
  }
}
