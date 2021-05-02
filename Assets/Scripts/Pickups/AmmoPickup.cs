using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup: Pickup
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int ammoToGive = 6;

    
    
    protected override bool Activate()
    {

        Weapon weapon = player.playerCombat.GetWeaponFromType(weaponType);
        
        Weapon playerWeapon = player.playerCombat.CurrentWeapon;

        if (!player.playerCombat.weaponInventory.Contains(weapon))
            return false;
        
        int ammoDiff = playerWeapon.MaxAmmoTotal - playerWeapon.TotalAmmo;

        if (ammoDiff == 0)
            return false;
        
        if (ammoToGive > ammoDiff)
            playerWeapon.AddAmmo(ammoDiff);
        else
            playerWeapon.AddAmmo(ammoToGive);

        return true;
    }
}
