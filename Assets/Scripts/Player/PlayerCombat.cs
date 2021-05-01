using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerController playerController;
    
    public Weapon weapon;

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
            if (weapon.CurrentAmmo > 0)
            {
                weapon.Shoot(playerController.camera.transform);
            }
        };
        
        playerActions.Reload.performed += context => weapon.Reload();
    }

    private void Update()
    {
        UpdateAmmoText();
    }
    
    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + weapon.CurrentAmmo + "/" + weapon.MaxAmmoPerChamber;
        totalAmmoText.text = weapon.TotalAmmo.ToString();
    }
}
