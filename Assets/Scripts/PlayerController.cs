using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Camera camera;

    [SerializeField] private float acceleration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnRate;

    [SerializeField] private float clampX = 90;

    private Rigidbody rigidbody;

    private Vector3 movementDir;

    private float rotX;
    private float rotY;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        
    }

    private void Start()
    {
        rotX = 0;
        rotY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        movementDir = new Vector3(horizontal, 0, vertical).normalized;
        
        Debug.Log(rigidbody.velocity.magnitude);
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
        rotX += Input.GetAxis("Mouse X") * turnRate * Time.deltaTime;
        rotY += Input.GetAxis("Mouse Y") * turnRate * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, -Mathf.Abs(clampX), Mathf.Abs(clampX));      
        
        camera.transform.localRotation = Quaternion.Euler(-rotY, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotX, 0f);
    }
}
