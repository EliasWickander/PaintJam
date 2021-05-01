using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float detectionRange;
    
    private float currentHealth;

    protected Transform target;

    protected abstract void Attack();

    private void Update()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRange, LayerMask.GetMask("Player"));
        
        if (detectedColliders.Length > 0)
        {
            target = detectedColliders[0].transform;
            Debug.Log("Detected!");
        }
        else
        {
            if (target != null)
            {
                Debug.Log("Lost target!");
                target = null;
            }
            else
            {
                Debug.Log("No target");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
