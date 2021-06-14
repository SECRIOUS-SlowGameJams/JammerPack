using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 
 */


public class GameStateManager : MonoBehaviour
{

    public KeyCode pauseGameKey;
    public KeyCode toggleUIKey;
    public GameObject mainMenu;
    public GameObject pauseScreen;


    /// Public Properties
    static event Action UIOpenedEvent;
    static event Action UIClosedEvent;

    public static event Action GamePausedEvent;
    public static event Action GameUnPausedEvent;


    public static event Action GameOverEvent;
    /// Serialized Fields for Editor
#pragma warning disable 0649

#pragma warning restore 0649


    ///  private Fields
    public bool canPauseGame = true;
    public bool UIPausesTheGame = true;
    public bool canOpenUI = true;
    bool ispaused = false;
    bool uiIsOpen;

    ///  Unity CallBacks Methods
    void Awake()
    {


    }

    void OnEnable()
    {
        if (UIPausesTheGame)
        {
            UIOpenedEvent += PauseGame;
            UIClosedEvent += UnPauseGame;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        //allows the player to pause the game;
        if (canPauseGame)
            TogglePauseGameFeature();

        if (canOpenUI)
        {
            if (Input.GetKeyDown(toggleUIKey))
            {
                ToggleUI(!uiIsOpen);
            }
        }
    }


    void OnDisable()
    {
        if (UIPausesTheGame)
        {
            UIOpenedEvent -= PauseGame;
            UIClosedEvent -= UnPauseGame;
        }
    }


    ///  Public Methods

    //is called from UI Toggler
    public void ToggleUI(bool tof)
    {
        if (tof)
        {
            uiIsOpen = true;
            mainMenu.SetActive(true);
            UIOpenedEvent?.Invoke();
        }
        else
        {
            uiIsOpen = false;
            mainMenu.SetActive(false);
            UIClosedEvent?.Invoke();
        }
    }

    public static void GameOver()
    {
        GameOverEvent?.Invoke();
    }

    ///  Private Methods
    ///  
    void TogglePauseGameFeature()
    {
        {
            if (Input.GetKeyDown(pauseGameKey))
            {
                if (!ispaused)
                {
                    PauseGame();
                }
                else
                {
                    UnPauseGame();
                }
            }
        }

    }


    void PauseGame()
    {
        if (pauseScreen != null)
            pauseScreen.SetActive(true);
        GamePausedEvent?.Invoke();
        ispaused = true;
        Time.timeScale = 0;
    }

    void UnPauseGame()
    {
        if (pauseScreen != null)
            pauseScreen.SetActive(false);
        GameUnPausedEvent?.Invoke();
        ispaused = false;
        Time.timeScale = 1;
    }
}
