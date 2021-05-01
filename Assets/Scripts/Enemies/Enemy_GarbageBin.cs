using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GarbageBin : Enemy
{
    protected override void Attack()
    {
        Debug.Log("garbage attack!");
        player.ModifyHealth(-damage);
    }
}
