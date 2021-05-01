using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Milk : Enemy
{
    Animator this_anim;


    private void Start()
    {
        this_anim = GetComponent<Animator>();
    }
    protected override void Attack()
    {
        Debug.Log("milk attack!");
        this_anim.SetTrigger("attack");
        
    }
    public void Dmg_milk()
    {
        player.ModifyHealth(-damage);
    }
}
