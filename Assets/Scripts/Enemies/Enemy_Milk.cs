using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Milk : Enemy
{
    [SerializeField]Animator this_anim;
    AudioSource AU;
    public AudioClip walk, attack, die;
    private void Start()
    {
        // this_anim = GetComponent<Animator>();
        AU = GetComponent<AudioSource>();
    }
    public override void Attack()
    {
        this_anim.SetTrigger("Attack");
        
    }
    public void Dmg_milk()
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
