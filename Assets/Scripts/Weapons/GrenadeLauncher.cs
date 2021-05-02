using System.Collections;
using System.Collections.Generic;
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
        GameObject launchedGrenade = Instantiate(grenade, origin.position + origin.forward, origin.rotation);

        Rigidbody rigidbody = launchedGrenade.GetComponent<Rigidbody>();
        
        rigidbody.AddForce(origin.up * upPower, ForceMode.Impulse);
        rigidbody.AddForce(origin.forward * launchPower, ForceMode.Impulse);
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
