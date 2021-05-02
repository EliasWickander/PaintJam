using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public InputControls InputControls { get; set; }
    
    private bool cursorLocked = false;

    private void Awake()
    {
        Instance = this;
        
        InputControls = new InputControls();
        
        InputControls.Enable();
    }

    private void Start()
    {
        InputControls.SystemActions systemActions = InputControls.System;

        systemActions.ToggleCursor.performed += context => ToggleCursor();
        
        ToggleCursor();
    }

    private void ToggleCursor()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        cursorLocked = !cursorLocked;

    }
}
