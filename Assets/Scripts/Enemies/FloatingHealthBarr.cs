using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar
{
    [SerializeField] private Slider slider;



    public void UpdateHealthBar(int health, int maxHealth)
    {
        if (slider == null)
        {
            Debug.LogError("Slider is not assigned.");
            return;
        }
        slider.value = (float)health / maxHealth;
    }

    void Update()
    {

    }
}
