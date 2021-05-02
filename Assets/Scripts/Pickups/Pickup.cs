using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected PlayerController player;
    [SerializeField] private float radius = 2;

    AudioSource AS;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        AS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Collider[] colliderHits = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));

        if (colliderHits.Length > 0)
        {
            if(Activate())
            {
                AS.Play();
                Destroy(gameObject);
            }
                
        }
    }

    protected abstract bool Activate();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
