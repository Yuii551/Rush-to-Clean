using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public UnityEngine.GameObject gameManager;
    public TMP_Text gameOverText;

    private bool isGameOver;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameManager.SetActive(false);
        isGameOver = false;
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameManager.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isGameOver = true;
        gameOverText.text = "GAME OVER";
    }

    public void GameWon()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameManager.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isGameOver = true;
        gameOverText.text = "YOU WIN";
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Update()
    {
        if (isGameOver)
        {
            // Don't perform any further gameplay updates when game is over
            return;
        }
    }
}
