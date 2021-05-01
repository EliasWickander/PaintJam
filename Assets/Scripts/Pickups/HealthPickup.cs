using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private float healthToGive;
    
    protected override void Activate()
    {
        player.ModifyHealth(healthToGive);
    }
}
