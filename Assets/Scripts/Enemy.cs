using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    private Rigidbody rigidBody;
    
    [SerializeField] private float maxHealth;
    private float currentHealth;
    
    [SerializeField] private float acceleration;
    [SerializeField] private float deAcceleration;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;

    protected Transform target;

    protected abstract void Attack();

    private void Awake()
    {
        currentHealth = maxHealth;

        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleDetection();
        
        if (target)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            dirToTarget.y = 0;
            
            Vector3 targetVelocity = dirToTarget * movementSpeed;
            
            if((target.position - transform.position).magnitude > attackRange)
            {
                rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, targetVelocity, acceleration * Time.deltaTime);   
            }
            else
            {
                rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, deAcceleration * Time.deltaTime);
            }
        }
    }

    private void HandleDetection()
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Took " + damage + " damage. Has " + currentHealth + " remaining.");
        if (currentHealth <= 0)
        {
            Debug.Log("died");
            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
