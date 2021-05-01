using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GarbageBin : Enemy
{
    [SerializeField] Animator this_anim;
    public override void Attack()
    {
        Debug.Log("garbage attack!");
        this_anim.SetTrigger("Attack");

    }
    


    private void Start()
    {
        // this_anim = GetComponent<Animator>();
    }
    
    public void Dmg_Gabage()
    {
        player.ModifyHealth(-damage);
    }
}
