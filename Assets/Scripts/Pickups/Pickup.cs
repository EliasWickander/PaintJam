using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected PlayerController player;
    [SerializeField] private float radius = 2;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        Collider[] colliderHits = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));

        if (colliderHits.Length > 0)
        {
            Activate();
            Destroy(gameObject);
        }
    }

    protected abstract void Activate();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
