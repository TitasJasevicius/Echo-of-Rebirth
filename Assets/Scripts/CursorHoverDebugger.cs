using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CursorHoverDebugger : MonoBehaviour
{
  void Update()
  {
    // Check if the cursor is hovering over a UI element
    if (EventSystem.current.IsPointerOverGameObject())
    {
      // Create a pointer event data object to store mouse position
      PointerEventData pointerData = new PointerEventData(EventSystem.current)
      {
        position = Input.mousePosition
      };

      // List to hold the raycast results
      List<RaycastResult> raycastResults = new List<RaycastResult>();
      EventSystem.current.RaycastAll(pointerData, raycastResults);

      // Log UI elements the cursor is hovering over
      foreach (RaycastResult result in raycastResults)
      {
        Debug.Log("Hovering over UI: " + result.gameObject.name);
      }
    }
    else
    {
      Debug.Log("Not hovering over any UI element.");
    }

    // Check for objects in the 3D/2D world using a Physics Raycast
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
      Debug.Log("Hovering over 3D object: " + hit.collider.gameObject.name);
    }
    else
    {
      Debug.Log("Not hovering over any 3D object.");
    }

    // Optional: Check for 2D objects using Physics2D Raycast
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector2.zero);
    if (hit2D.collider != null)
    {
      Debug.Log("Hovering over 2D object: " + hit2D.collider.gameObject.name);
    }
  }
}
