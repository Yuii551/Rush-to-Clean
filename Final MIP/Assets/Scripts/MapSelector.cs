using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public Button mapButton;
    public string loadingSceneName;
    public string mapSceneName;

    [SerializeField] private MapSelector loadingScene;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        mapButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        StartCoroutine(LoadMapScene());
    }

    private IEnumerator LoadMapScene()
    {
        // Load the loading scene asynchronously
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(loadingSceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            // Calculate the progress of the loading scene (0 to 0.9)
            float loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

            // If the loading is nearly complete
            if (loadingProgress >= 0.9f)
            {
                // Wait for a short delay
                yield return new WaitForSeconds(0.5f);

                // Activate the loading scene to transition to the help scene
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Load the help scene asynchronously
        AsyncOperation helpOperation = SceneManager.LoadSceneAsync("HelpScene", LoadSceneMode.Additive);

        while (!helpOperation.isDone)
        {
            yield return null;
        }

        // Unload the loading scene
        SceneManager.UnloadSceneAsync(loadingSceneName);

        // Load the map scene asynchronously
        AsyncOperation mapOperation = SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Additive);

        while (!mapOperation.isDone)
        {
            yield return null;
        }

        // Unload the help scene
        SceneManager.UnloadSceneAsync("HelpScene");
    }
}

