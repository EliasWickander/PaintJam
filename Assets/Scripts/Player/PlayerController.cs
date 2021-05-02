using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    public float maxHealth;
    public float CurrentHealth { get; set; }

    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float turnRate;
    [SerializeField] private float turnSmoothMod = 1;
    private GameObject optionsMenu;

    [Header("Camera")]
    [SerializeField] private float clampX = 90;

    private Rigidbody rigidbody;

    private Vector3 movementDir;

    private float rotX;
    private float rotY;

    [Header("UI elements")]
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Animation/Sound")]
    AudioSource AS;
    [SerializeField] AudioClip Hurt, Die;
    [SerializeField] Animator Playericon;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();

        playerCombat = GetComponent<PlayerCombat>();
        CurrentHealth = maxHealth;
        AS = GetComponent<AudioSource>();

        optionsMenu =  GameObject.Find("MenuManager");

    }

    private void Start()
    {

        rotX = 0;
        rotY = 0;

        if (GameObject.Find("MenuManager")) optionsMenu.GetComponent<Menu>().Regrets();





        InputControls.SystemActions systemActionsVar = GameManager.Instance.InputControls.System;

        if (GameObject.Find("MenuManager")) systemActionsVar.ToggleOptions.performed += context => optionsMenu.GetComponent<Menu>().OptionsToggle();





    }
    
    // Update is called once per frame
    void Update()
    {
        Vector2 moveAxis = GameManager.Instance.InputControls.Player.Move.ReadValue<Vector2>();
        
        movementDir = new Vector3(moveAxis.x, 0, moveAxis.y).normalized;

        movementDir = transform.TransformDirection(movementDir);

        healthText.text = "Health \n" + CurrentHealth;

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

        camera.transform.localRotation = Quaternion.Slerp(camera.transform.localRotation, cameraTargetRot, 1);

        Quaternion playerTargetRot = Quaternion.Euler(0f, rotX, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerTargetRot, 1);
    }

    public void ModifyHealth(float health)
    {
        if(health<=0)
        {
            AS.clip = Hurt;
            AS.Play();
        }
        CurrentHealth += health;
        if (CurrentHealth <= maxHealth/2)
        {
            Playericon.SetBool("low_hp", true);
            
        }
        if (CurrentHealth >= maxHealth/2)
        {

            Playericon.SetBool("low_hp", false);
        }
        if (CurrentHealth <= 0)
        {
            Debug.Log("Player died");
            AS.clip = Die;
            AS.Play();
        }
        else if (CurrentHealth >= maxHealth)
        {
            CurrentHealth = maxHealth;
        }
        
    }
}
