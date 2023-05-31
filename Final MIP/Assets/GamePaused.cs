using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePaused : MonoBehaviour
{
    public GameObject pauseUI;
    private bool isPaused;
    private bool isGameOver;

    private void Start()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        isGameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver) // Only pause the game if it's not already game over
            {
                if (!isPaused)
                    PauseGame();
                else
                    ResumeGame();
            }
        }

        if (isGameOver)
        {
            // Don't perform any further gameplay updates when game is over
            return;
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;


        // Ensure the cursor is visible and unlocked
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SetGameOver(bool value)
    {
        isGameOver = value;
    }
}
