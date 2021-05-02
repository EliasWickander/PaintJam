using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public WeaponType weaponToGive;
    
    protected override bool Activate()
    {
        List<Weapon> weaponInventory = player.playerCombat.weaponInventory;

        Weapon weapon = player.playerCombat.GetWeaponFromType(weaponToGive);
        if (weaponInventory.Contains(weapon))
            return false;
        
        weaponInventory.Add(weapon);
        player.playerCombat.SwapWeapon(weaponToGive);

        return true;
    }
}
