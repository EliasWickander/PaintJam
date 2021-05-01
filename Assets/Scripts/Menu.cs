using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameObject optionsMenu;
    private GameObject mainMenu;
    public bool isMainMenu;
    public bool inGame;
    public bool optionsOn;
    private GameObject player;
    private float startingSense = 50f;
    private float startingSound = 0.5f;
    private GameObject cam;
    public float volumeSaved;
    public GameObject menuCanvas;

    public void Awake()
    {
        optionsMenu = GameObject.FindGameObjectWithTag("Options");
        DontDestroyOnLoad(this);

        if ( mainMenu = GameObject.FindGameObjectWithTag("MainMenu"))
        {
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
            isMainMenu = true;
            inGame = false;
        }

        else
        {
            isMainMenu = false;
        }
        
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

       if (mainMenu)
       {
            menuCanvas = GameObject.Find("Canvas");
            DontDestroyOnLoad(menuCanvas);
       }
        
    }



    void Start()
    {
         optionsMenu.SetActive(false);

        // if (isMainMenu)  mainMenu.SetActive(false);
        startingSense = 50f;
        startingSound = 0.5f;
        Debug.Log("StartingGaem");

    }

    public void OptionsToggle()
    {
        if (inGame)
        {
            //Close the menu
            if (optionsOn)
            {
                Cursor.lockState = CursorLockMode.Locked;
                optionsMenu.SetActive(false);
                Time.timeScale = 1;
            }
            //Open the menu
            else
            {
                Cursor.lockState = CursorLockMode.None;
                optionsMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    
        if (!optionsOn)
        {
            optionsMenu.SetActive(true);
            optionsOn = true;

            if (!inGame) mainMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(false);
            optionsOn = false;
            if (!inGame) mainMenu.SetActive(true);
        }
       

    }

    public void Regrets()
    {
        SetSense(startingSense);
        SetSound(startingSound);
    }

    public void SetSense(float sense)
    {
       
        if (inGame)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerController pcScript = player.GetComponent<PlayerController>();
            
            pcScript.turnRate = sense + 50;
            Debug.Log(sense);
            
        }
        else
        {
            startingSense = sense;
        }

    }

    public void SetSound(float volume)
    {
       cam = GameObject.FindGameObjectWithTag("MainCamera");
        
        if (inGame)
        {
            volumeSaved = volume;
            Debug.Log(volume);
        }
        else
        {
            startingSound = volume;
        }

    }
    
    public void loadGame(int sceneToLoad)
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        inGame = true;
        SceneManager.LoadScene(sceneToLoad);

      /*  if (GameObject.FindGameObjectWithTag("Player")) Debug.Log("It worked woho!");
        else Debug.Log("It did not work"); */
       
    }


}
