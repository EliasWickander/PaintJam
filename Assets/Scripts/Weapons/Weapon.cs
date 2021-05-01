using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackRate;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected int totalAmmo;
    [SerializeField] protected int maxAmmoPerChamber;
    [SerializeField] protected int maxAmmoTotal;

    protected float attackRateTimer = 0;
    
    public int MaxAmmoPerChamber
    {
        get { return maxAmmoPerChamber; }
        set { maxAmmoPerChamber = value; }
    }
    public int MaxAmmoTotal
    {
        get { return maxAmmoTotal; }
        set { maxAmmoTotal = value; }
    }
    public int TotalAmmo
    {
        get { return totalAmmo; }
        set { totalAmmo = value; }
    }

    public int CurrentAmmo { get; set; }

    private void Awake()
    {
        Reload();
    }

    private void Update()
    {
        if (attackRateTimer > 0)
        {
            attackRateTimer -= Time.deltaTime;
        }
        else
        {
            attackRateTimer = 0;
        }
    }

    public abstract void Shoot(Transform origin);
    public abstract void Reload();

    public void AddAmmo(int ammo)
    {
        TotalAmmo += ammo;
    }
}
