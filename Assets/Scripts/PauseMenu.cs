using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class PauseMenu : MonoBehaviour
{
    // Reference to the pause menu
    public GameObject pauseMenu;

    // Reference to the button that will resume gameplay
    public Button resumeButton;

    // This method will be called when the pause menu is opened
    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0;

        // Enable the pause menu
        pauseMenu.SetActive(true);
    }

    // This method will be called when the resume button is clicked
    public void ResumeGame()
    {
        // Unpause the game
        Time.timeScale = 1;

        // Disable the pause menu
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        // Set the button's onClick event to call the ResumeGame method
        resumeButton.onClick.AddListener(ResumeGame);
        
    }

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Time.timeScale==0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
        // Open the pause menu when the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}
