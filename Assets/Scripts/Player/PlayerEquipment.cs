using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
  public PlayerResources playerResources;
  public Weapon currentWeapon;

  public void EquipWeapon(Weapon newWeapon)
  {
    if (currentWeapon != null)
    {
      currentWeapon.DeactivateWeapon();
    }

    currentWeapon = newWeapon;
    currentWeapon.ActivateWeapon();
  }

  
}
