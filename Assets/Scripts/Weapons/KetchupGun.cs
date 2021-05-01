using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupGun : Weapon
{
    public override void Shoot(Transform origin)
    {
        Debug.Log("ketchup pew pew");

        if (Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, range))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
        
        Debug.DrawRay(origin.position, origin.forward * range, Color.blue, 5);
    }
}
