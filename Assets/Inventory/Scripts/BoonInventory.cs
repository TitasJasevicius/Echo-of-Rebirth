using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;

public class BoonInventory
{

  private List<Boon> boons = new List<Boon>();

  public BoonInventory()
  {
    boons = new List<Boon>();

    AddBoon(new Boon { boonType = Boon.BoonType.SwingSpeedBoon });
    AddBoon(new Boon { boonType = Boon.BoonType.HealthBoon });
    AddBoon(new Boon { boonType = Boon.BoonType.ManaBoon });
    AddBoon(new Boon { boonType = Boon.BoonType.SwingSpeedBoon });
    AddBoon(new Boon { boonType = Boon.BoonType.HealthBoon });
    AddBoon(new Boon { boonType = Boon.BoonType.ManaBoon });
    Debug.Log("Inventory created");
    Debug.Log(boons.Count);
  }

  public void AddBoon(Boon boon)
  {
    boons.Add(boon);
  }
  public List<Boon> GetBoons()
  {
    return boons;
  }

}
