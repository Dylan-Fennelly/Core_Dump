using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;


    public Button resumeButton;
    public Button restartButton;


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        
        pauseMenu.SetActive(false);
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void Start()
    { 
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(ResetGame);
        
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
