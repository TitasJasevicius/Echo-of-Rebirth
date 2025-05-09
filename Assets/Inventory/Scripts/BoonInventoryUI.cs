using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoonInventoryUI : MonoBehaviour
{
  /*public BoonInventory boonInventory;
  public Transform boonSlotContainer;
  public Transform boonSlotTemplate;
 
  public void Awake()
  {
    boonSlotContainer = transform.Find("BoonSlotContainer");
    boonSlotTemplate = boonSlotContainer.Find("BoonSlotTemplate");
  }
  
  public void SetBoonInventory(BoonInventory inventory)
  {
    boonInventory = inventory;
    RefreshInventoryBoons();
  }
  public void RefreshInventoryBoons()
  {
    foreach (Transform child in boonSlotContainer)
    {
      if (child != boonSlotTemplate)
      {
        Destroy(child.gameObject);
      }
    }

    int x = 0;
    int y = 0;
    float boonSlotCellSize = 30f;
    foreach (var boon in boonInventory.GetBoons())
    {
      
      RectTransform boonSlotRectTransform = Instantiate(boonSlotTemplate, boonSlotContainer).GetComponent<RectTransform>();
      boonSlotRectTransform.gameObject.SetActive(true);
      boonSlotRectTransform.anchoredPosition = new Vector2(x * boonSlotCellSize, -y * boonSlotCellSize);
      Image image = boonSlotRectTransform.Find("Image").GetComponent<Image>();
      image.sprite = boon.GetSprite();
      x++;
      if(x > 4)
      {
        x = 0;
        y++;
      }

    }
  } */


}
