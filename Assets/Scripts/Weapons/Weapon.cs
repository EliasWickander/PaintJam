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

    public abstract void Shoot(Transform origin);
    public abstract void Reload();

    public void AddAmmo(int ammo)
    {
        TotalAmmo += ammo;
    }
}
