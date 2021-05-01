using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    protected PlayerController player;
    
    private Rigidbody rigidBody;

    [SerializeField] private float maxHealth;
    private float currentHealth;
    
    [SerializeField] private float acceleration;
    [SerializeField] private float deAcceleration;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDuration;
    [SerializeField] protected float damage;

    private float attackTimer;

    protected Transform target;

    private bool canAttack = false;

    protected abstract void Attack();

    private void Awake()
    {
        currentHealth = maxHealth;

        rigidBody = GetComponent<Rigidbody>();

        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        HandleDetection();
        
        if (target)
        {
            if (canAttack)
            {
                HandleCombat();
            }
            else
            {
                HandleMovement();
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;

        dirToTarget.y = 0;
            
        Vector3 targetVelocity = dirToTarget * movementSpeed;
            
        if((target.position - transform.position).magnitude > attackRange)
        {
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, targetVelocity, acceleration * Time.deltaTime);
            canAttack = false;
        }
        else
        {
            if (rigidBody.velocity.magnitude > 0.2f)
            {
                rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, deAcceleration * Time.deltaTime);   
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
                canAttack = true;
                attackTimer = 0;
            }
        }
    }

    private void HandleCombat()
    {
        if (attackTimer >= attackDuration)
        {
            Attack();
            attackTimer = 0;
        }
        else
        {
            attackTimer += Time.deltaTime;
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

    public void ModifyHealth(float health)
    {
        currentHealth += health;

        Debug.Log(health + " health. Has " + currentHealth + " remaining.");
        
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
