using UnityEngine;

public class ToggleBoonInventory : MonoBehaviour
{
  public GameObject boonInventoryUI;
  public KeyCode toggleKey = KeyCode.B;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void Update()
  {
    if (Input.GetKeyDown(toggleKey))
    {
      boonInventoryUI.SetActive(!boonInventoryUI.activeSelf);
    }
  }
}
