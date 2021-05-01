using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Milk : Enemy
{
    [SerializeField]Animator this_anim;


    private void Start()
    {
       // this_anim = GetComponent<Animator>();
    }
    public override void Attack()
    {
        Debug.Log("milk attack!");
        this_anim.SetTrigger("Attack");
        
    }
    public void Dmg_milk()
    {
        player.ModifyHealth(-damage);
    }
}
