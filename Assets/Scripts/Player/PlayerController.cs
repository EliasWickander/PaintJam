using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Camera camera;

    [HideInInspector]
    public PlayerCombat playerCombat;
    
    [SerializeField] private float maxHealth;
    private float currentHealth;
    
    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnRate;
    [SerializeField] private float turnSmoothMod = 1;

    [Header("Camera")]
    [SerializeField] private float clampX = 90;

    private Rigidbody rigidbody;

    private Vector3 movementDir;

    private float rotX;
    private float rotY;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();

        playerCombat = GetComponent<PlayerCombat>();
        currentHealth = maxHealth;

    }

    private void Start()
    {

        rotX = 0;
        rotY = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 moveAxis = GameManager.Instance.InputControls.Player.Move.ReadValue<Vector2>();
        
        movementDir = new Vector3(moveAxis.x, 0, moveAxis.y).normalized;

        movementDir = transform.TransformDirection(movementDir);

    }

    private void LateUpdate()
    {
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 targetVel = movementDir * moveSpeed;
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, targetVel, Time.deltaTime * acceleration);
    }

    private void HandleRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        mouseDelta.Normalize();
        
        rotX += mouseDelta.x * turnRate * Time.deltaTime;
        rotY += mouseDelta.y * turnRate * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -Mathf.Abs(clampX), Mathf.Abs(clampX));

        Quaternion cameraTargetRot = Quaternion.Euler(-rotY, 0f, 0f);
        camera.transform.localRotation = Quaternion.Slerp(camera.transform.localRotation, cameraTargetRot, (1 / turnSmoothMod) * Time.deltaTime);

        Quaternion playerTargetRot = Quaternion.Euler(0f, rotX, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerTargetRot, (1 / turnSmoothMod) * Time.deltaTime);
    }

    public void ModifyHealth(float health)
    {
        currentHealth += health;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died");
        }
        else if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        Debug.Log(health + " health. Has " + currentHealth + " remaining.");
    }
}
