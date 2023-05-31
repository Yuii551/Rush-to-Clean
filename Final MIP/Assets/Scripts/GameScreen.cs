using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScreen : MonoBehaviour
{
    public Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(BackButtonClicked);
    }

    public void BackButtonClicked()
    {
        backButton.interactable = false; // Disable the button
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.interactable = false; // Disable the button
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
