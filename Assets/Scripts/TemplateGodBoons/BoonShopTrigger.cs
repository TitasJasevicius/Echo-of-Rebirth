using UnityEngine;

public class BoonShopTrigger : MonoBehaviour
{
  [SerializeField] public TemplateGodUI templateGodUI;
  public void OnTriggerEnter2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      templateGodUI.Show();
    }

  }

  public void OnTriggerExit2D(Collider2D collider)
  {
    PlayerResources player = collider.GetComponent<PlayerResources>();
    if (player != null)
    {
      templateGodUI.Hide();
    }
  }
}
