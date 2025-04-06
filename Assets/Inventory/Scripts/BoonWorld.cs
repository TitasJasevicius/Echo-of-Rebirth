using UnityEngine;
using System;
using System.Collections.Generic;
public class BoonWorld : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static BoonWorld SpawnBoonWorld(Vector3 position, Boon boon)
    {
      Transform transform = Instantiate(BoonAssets.Instance.pfBoonWorld, position, Quaternion.identity);

      BoonWorld boonWorld = transform.GetComponent<BoonWorld>();
      boonWorld.SetBoon(boon);

      return boonWorld;
    }
    public Boon boon;
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetBoon(Boon boon)
    {
        this.boon = boon;
        spriteRenderer.sprite = boon.GetSprite();
    }
 }
