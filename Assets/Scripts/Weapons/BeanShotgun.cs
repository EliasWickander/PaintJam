using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeanShotgun : Weapon
{
    [SerializeField] private float impactRadius = 1;
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
        Vector3 posOffset = Vector3.up * 0.5f;
        Vector3 dirOffset = Vector3.up * 0.1f;
        
        Vector3 socketPos = origin.position + posOffset;
        Vector3 dir = origin.forward + dirOffset;
        dir.Normalize();

        if (Physics.SphereCast(socketPos, impactRadius, dir, out RaycastHit hit, range))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy)
            {
                enemy.ModifyHealth(-damage);
            }
        }

        CurrentAmmo -= 1;
        attackRateTimer = attackRate;
        
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
