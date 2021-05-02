using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupGun : Weapon
{
    public override void Shoot(Transform origin)
    {
        this.origin = origin;
        if (attackRateTimer > 0)
        {
            return;   
        }

        animator.SetTrigger("Fire");
    }

    public void ApplyShot()
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
        attackRateTimer = attackRate;
        
        Debug.DrawRay(origin.position, origin.forward * range, Color.blue, 5);

        if (CurrentAmmo == 0)
            Reload();
    }
    
    public override void Reload()
    {
        int ammoDiff = Mathf.Abs(maxAmmoPerChamber - CurrentAmmo);

        if (ammoDiff == 0 || TotalAmmo == 0)
        {
            return;   
        }

        animator.SetTrigger("reload");
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
