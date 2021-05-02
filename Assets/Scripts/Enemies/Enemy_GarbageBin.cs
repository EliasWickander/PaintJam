using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GarbageBin : Enemy
{
    [SerializeField] Animator this_anim;
    AudioSource AU;
    public AudioClip walk, attack, die;
    public override void Attack()
    {
        Debug.Log("garbage attack!");
        this_anim.SetTrigger("Attack");

    }
    


    private void Start()
    {
        // this_anim = GetComponent<Animator>();
        AU = GetComponent<AudioSource>();
    }
    
    public void Dmg_Gabage()
    {
        player.ModifyHealth(-damage);
    }
    public override void walksound()
    {
        AU.clip = walk;
        AU.Play();
    }
    public override void attacksound()
    {
        AU.clip = attack;
        AU.Play();
    }

    public override void Diedsfx()
    {
        AU.clip = die;
        AU.Play();
    }
    
}
