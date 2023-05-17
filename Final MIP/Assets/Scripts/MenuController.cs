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

    private int _qualityLevel;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        tutorialButton.onClick.AddListener(TutorialButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene("MapSelection");
    }

    private void TutorialButtonClicked()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    private void QuitButtonClicked()
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
