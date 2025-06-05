using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public Image healthImage;           // Assign this in the Inspector (the UI Image object)
  public Sprite[] healthSprites;      // Assign your 20 sprites in order, from full to empty

  private int maxHealth;

  // Call this to set up the initial health bar
  public void SetMaxHealth(int health)
  {
    maxHealth = health;
    UpdateHealthBar(health);
  }

  // Call this whenever health changes
  public void SetHealth(int health)
  {
    UpdateHealthBar(health);
  }

  private void UpdateHealthBar(int health)
  {
    int spriteIndex = Mathf.Clamp(
        Mathf.RoundToInt((float)(maxHealth - health) / maxHealth * (healthSprites.Length - 1)),
        0, healthSprites.Length - 1);
    healthImage.sprite = healthSprites[spriteIndex];
  }
}
