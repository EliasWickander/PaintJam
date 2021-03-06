using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Slider sensSlider;

    public void Awake()
    {
        optionsMenu = GameObject.FindGameObjectWithTag("Options");
        DontDestroyOnLoad(this);

        if (GameObject.FindGameObjectWithTag("MainMenu"))
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
        startingSense = sensSlider.value;
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
                Cursor.visible = false;
                optionsMenu.SetActive(false);
                Time.timeScale = 1;
            }
            //Open the menu
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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

            pcScript.turnRate = sense;

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

    public void LoadGame(int sceneToLoad)
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        GameObject menuBG = GameObject.Find("background");
        menuBG.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        inGame = true;
        SceneManager.LoadScene(sceneToLoad);

        /*  if (GameObject.FindGameObjectWithTag("Player")) Debug.Log("It worked woho!");
          else Debug.Log("It did not work"); */

    }


}