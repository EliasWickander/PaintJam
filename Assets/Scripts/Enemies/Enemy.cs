using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    protected PlayerController player;
    
    private Rigidbody rigidBody;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] protected float currentHealth;
    
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float deAcceleration = 10;
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float detectionRange = 20;
    [SerializeField] private float attackRange = 10;
    [SerializeField] private float attackDuration = 2;
    [SerializeField] protected float damage = 5;

    [SerializeField] protected int score = 5;

    

    private float attackTimer;

    protected Transform target;

    private bool canAttack = false;

    //abstarct voids
    public abstract void Attack();
    public abstract void walksound();
    public abstract void attacksound();
    public abstract void Diedsfx();
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
            
            HandleMovement();
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
        }
        else
        {
            if (target != null)
            {
                target = null;
            }
            else
            {
            }
        }
    }

    public void ModifyHealth(float health)
    {
        currentHealth += health;

        if (currentHealth <= 0)
        {
            Debug.Log("enemy died");
            Diedsfx();

            if (SpawnDirector.Instance)
                SpawnDirector.Instance.activeWave.UnitsAlive--;

            GameManager.Instance.Score += score;
            Destroy(gameObject);
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
