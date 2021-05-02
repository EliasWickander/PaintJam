using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected float attackRate = 0.2f;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float range = 20;
    [SerializeField] protected int totalAmmo = 12;
    [SerializeField] protected int maxAmmoPerChamber = 6;
    [SerializeField] protected int maxAmmoTotal = 99;

    protected Transform origin;

    protected float attackRateTimer = 0;
    
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
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
        animator = GetComponent<Animator>();
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
