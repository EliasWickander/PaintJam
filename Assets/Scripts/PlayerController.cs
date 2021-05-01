using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera camera;
    private PlayerCombat playerCombat;
    
    [SerializeField] private float acceleration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnRate;

    private Rigidbody rigidbody;

    private Vector3 movementDir;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        movementDir = new Vector3(horizontal, 0, vertical);
        
        movementDir = transform.TransformDirection(movementDir);
        
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
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");
        
        camera.transform.Rotate(new Vector3(-yAxis, 0, 0));
        transform.Rotate(new Vector3(0, xAxis, 0) * turnRate);
    }
}
