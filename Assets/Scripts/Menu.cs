using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameObject optionsMenu;
    private GameObject mainMenu;
    public bool isMainMenu;
    public bool inGame;
    public bool optionsOn;


    public void Awake()
    {
        optionsMenu = GameObject.FindGameObjectWithTag("Options");
        DontDestroyOnLoad(this);

        if ( mainMenu = GameObject.FindGameObjectWithTag("MainMenu"))
        {
            mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
            isMainMenu = true;
        }

        else
        {
            isMainMenu = false;
        }

       
    }



    void Start()
    {
       // optionsMenu.SetActive(false);

     // if (isMainMenu)  mainMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        

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



    public void SetVolume( float volume)
    {
        // set volume to volume value
        // also set the volume slider
        Debug.Log(volume);
    }

    public void loadGame(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
