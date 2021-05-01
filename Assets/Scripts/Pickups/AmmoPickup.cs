using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup: Pickup
{
    [SerializeField] private int ammoToGive = 6;
    
    protected override void Activate()
    {
        Weapon playerWeapon = player.playerCombat.weapon;

        int ammoDiff = playerWeapon.MaxAmmoTotal - playerWeapon.TotalAmmo;
        
        if (ammoToGive > ammoDiff)
            playerWeapon.AddAmmo(ammoDiff);
        else
            playerWeapon.AddAmmo(ammoToGive);
    }
}
