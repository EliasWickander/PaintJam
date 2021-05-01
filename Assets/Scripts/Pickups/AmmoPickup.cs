using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup: Pickup
{
    [SerializeField] private int ammoToGive = 6;
    
    protected override void Activate()
    {
        player.playerCombat.weapon.AddAmmo(ammoToGive);
    }
}
