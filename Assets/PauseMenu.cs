using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems; //this is to fix the pause screen issue

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the Pause Menu Panel
    public Button MenuButton;       // Reference to the Menu Button
    private bool isPaused = false;  // Tracks if the game is paused

    void Start()
    {
        // Assign the function to the Menu Button's onClick event
        MenuButton.onClick.AddListener(TogglePauseMenu);
        pauseMenuUI.SetActive(false);  // Ensure the pause menu is hidden at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;          // Freeze the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f;          // Resume the game
        isPaused = false;

        EventSystem.current.SetSelectedGameObject(null);
    }

    // Optional: Add other buttons like "Quit" or "Settings"
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}