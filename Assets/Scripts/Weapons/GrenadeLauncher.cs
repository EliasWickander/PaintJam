using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [SerializeField] private GameObject grenade;
    [SerializeField] private float launchPower;
    [SerializeField] private float upPower;
    
    public override void Shoot(Transform origin)
    {
        this.origin = origin;
        if (attackRateTimer > 0)
        {
            return;   
        }
        
        Debug.Log("pew pew");
        
        animator.SetTrigger("Fire");
    }

    public void LaunchGrenade()
    {
        Vector3 posOffset = Vector3.up * 0.5f;
        Vector3 dirOffset = Vector3.up * 0.1f;
        
        Vector3 socketPos = origin.position + posOffset;
        Vector3 dir = origin.forward + dirOffset;
        dir.Normalize();
        
        GameObject launchedGrenade = Instantiate(grenade, socketPos + origin.forward, origin.rotation);

        Rigidbody rigidbody = launchedGrenade.GetComponent<Rigidbody>();
        
        rigidbody.AddForce(origin.up * upPower, ForceMode.Impulse);
        rigidbody.AddForce(dir * launchPower, ForceMode.Impulse);
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
