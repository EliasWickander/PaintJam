using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public InputControls InputControls { get; set; }
    
    private bool cursorLocked = false;

    [SerializeField] private TextMeshProUGUI scoreText, scoreText2;
    [SerializeField] private TextMeshProUGUI waveText;
    public int Score { get; set; }
    public int CurrentWave { get; set; }

    private void Awake()
    {
        Instance = this;
        
        InputControls = new InputControls();
        
        InputControls.Enable();
        
        ToggleCursor();
    }

    private void Start()
    {
        InputControls.SystemActions systemActions = InputControls.System;

        systemActions.ToggleCursor.performed += context => ToggleCursor();

    }

    private void Update()
    {
        scoreText.text = Score.ToString();
        scoreText2.text = Score.ToString();
        waveText.text = "Wave " + CurrentWave;
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
