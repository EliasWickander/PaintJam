using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum WeaponType
{
    KetchupGun,
    BeanShotgun,
    GrenadeLauncher
}

public class PlayerCombat : MonoBehaviour
{
    private PlayerController playerController;

    public WeaponType startGun;
    public Weapon CurrentWeapon { get; set; }
    public Weapon grenadeLauncher;
    public Weapon beanShotgun;
    public Weapon ketchupGun;
    
    public List<Weapon> weaponInventory = new List<Weapon>();

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI totalAmmoText;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        InputControls.PlayerActions playerActions = GameManager.Instance.InputControls.Player;

        playerActions.Fire.performed += context =>
        {
            if (CurrentWeapon.CurrentAmmo > 0)
            {
                CurrentWeapon.Shoot(playerController.camera.transform);
            }
        };
        
        playerActions.Reload.performed += context => CurrentWeapon.Reload();

        playerActions.SwapToKetchupGun.performed += context => SwapWeapon(WeaponType.KetchupGun); 
        playerActions.SwapToBeanShotgun.performed += context => SwapWeapon(WeaponType.BeanShotgun); 
        playerActions.SwapToGrenadeLauncher.performed += context => SwapWeapon(WeaponType.GrenadeLauncher); 
        
        weaponInventory.Add(GetWeaponFromType(startGun));
        SwapWeapon(startGun);
    }

    private void Update()
    {
        UpdateAmmoText();
    }
    
    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + CurrentWeapon.CurrentAmmo + "/" + CurrentWeapon.MaxAmmoPerChamber;
        totalAmmoText.text = CurrentWeapon.TotalAmmo.ToString();
    }

    public void SwapWeapon(WeaponType weapon)
    {
        Weapon newWeapon = GetWeaponFromType(weapon);

        if (!weaponInventory.Contains(newWeapon))
            return;
        
        if(CurrentWeapon != null)
            CurrentWeapon.gameObject.SetActive(false);

        newWeapon.gameObject.SetActive(true);

        CurrentWeapon = newWeapon;
    }

    public Weapon GetWeaponFromType(WeaponType type)
    {
        Weapon weapon = ketchupGun;
        
        switch (type)
        {
            case WeaponType.KetchupGun:
            {
                weapon = ketchupGun;
                break;
            }
            case WeaponType.BeanShotgun:
            {
                weapon = beanShotgun;
                break;
            }
            case WeaponType.GrenadeLauncher:
            {
                weapon = grenadeLauncher;
                break;
            }
        }

        return weapon;
    }
}
