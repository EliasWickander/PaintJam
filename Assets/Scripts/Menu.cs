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
       
        
    }



    void Start()
    {
        // optionsMenu.SetActive(false);

        // if (isMainMenu)  mainMenu.SetActive(false);
        startingSense = 50f;
        startingSound = 0.5f;
        Debug.Log("StartingGaem");

    }

    private void OptionsToggle()
    {
        if (inGame)
        {
            if (optionsOn)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;

            }
        }
    
        if (!optionsOn)
        {
            optionsMenu.SetActive(true);
            optionsOn = true;
        }
        else
        {
            optionsMenu.SetActive(false);
            optionsOn = false;
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
        inGame = true;
        SceneManager.LoadScene(sceneToLoad);

      /*  if (GameObject.FindGameObjectWithTag("Player")) Debug.Log("It worked woho!");
        else Debug.Log("It did not work"); */
       
    }


}
