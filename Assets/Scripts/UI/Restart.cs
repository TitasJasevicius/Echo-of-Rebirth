using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
  public GameObject menuPanel; 

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (menuPanel.activeSelf)
        HideMenu();
      else
        ShowMenu();
    }
  }

  public void ShowMenu()
  {
    SetActiveRecursively(menuPanel, true);
  }

  public void HideMenu()
  {
    SetActiveRecursively(menuPanel, false);
  }

  
  private void SetActiveRecursively(GameObject obj, bool active)
  {
    obj.SetActive(active);
    foreach (Transform child in obj.transform)
    {
      SetActiveRecursively(child.gameObject, active);
    }
  }

  public void RestartGame()
  {
   
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
