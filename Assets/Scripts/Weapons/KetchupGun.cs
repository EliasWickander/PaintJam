using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupGun : Weapon
{
    public override void Shoot(Transform origin)
    {

        if (Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, range))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy)
            {
                enemy.ModifyHealth(-damage);
            }
        }

        CurrentAmmo -= 1;
        
        Debug.DrawRay(origin.position, origin.forward * range, Color.blue, 5);
    }

    public override void Reload()
    {
        int ammoDiff = Mathf.Abs(maxAmmoPerChamber - CurrentAmmo);

        if (TotalAmmo >= ammoDiff)
        {
            CurrentAmmo += ammoDiff;
            TotalAmmo -= ammoDiff;
        }
        else
        {
            CurrentAmmo += TotalAmmo;
            TotalAmmo = 0;
        }
        
    }
}
