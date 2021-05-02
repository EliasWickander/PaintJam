using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private float healthToGive;
    
    protected override bool Activate()
    {
        float healthDiff = player.maxHealth - player.CurrentHealth;

        if (healthDiff == 0)
            return false;
        
        if(healthToGive > healthDiff)
            player.ModifyHealth(healthDiff);
        else
            player.ModifyHealth(healthToGive);

        return true;
    }
}
