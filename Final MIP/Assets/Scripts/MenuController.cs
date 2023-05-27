using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button quitButton;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    public int mainControllerSen = 4;

    [Header("Graphics Settings")]
    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Loading")]
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider slider;

    private int _qualityLevel;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("masterQuality", 2));
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("MapSelection");
    }

    public void TutorialButtonClicked()
    {
        StartCoroutine(LoadGame("TutorialScene"));
    }

    private IEnumerator LoadGame(string sceneName)
    {
        // Show the loading scene
        loadingScene.SetActive(true);

        // Load the loading scene asynchronously
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("LoadingScene");
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            // Calculate the progress of the loading scene (0 to 0.9)
            float loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

            // Update the slider value accordingly
            slider.value = loadingProgress;

            // If the loading is nearly complete
            if (loadingProgress >= 0.9f)
            {
                // Wait for a short delay
                yield return new WaitForSeconds(0.5f);

                // Activate the loading scene to transition to the desired scene
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
    }


}
