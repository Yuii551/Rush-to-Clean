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

    [SerializeField] private GameObject loadingScene;
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
        // Show the loading scene
        if (loadingScene == null)
        {
            Debug.LogError("Loading scene GameObject is not assigned. Make sure to assign it in the inspector.");
            yield break;
        }

        loadingScene.SetActive(true);

        // Load the loading scene asynchronously
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(loadingSceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            // Calculate the progress of the loading scene (0 to 0.9)
            float loadingProgress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

            // Update the slider value accordingly
            if (slider == null)
            {
                Debug.LogError("Slider component is not assigned. Make sure to assign it in the inspector.");
                yield break;
            }

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

        // Load the map scene asynchronously
        AsyncOperation mapOperation = SceneManager.LoadSceneAsync(mapSceneName);

        while (!mapOperation.isDone)
        {
            // Calculate the progress of the map scene loading (0 to 1)
            float mapProgress = Mathf.Clamp01(mapOperation.progress / 0.9f);

            // Update the slider value accordingly
            if (slider == null)
            {
                Debug.LogError("Slider component is not assigned. Make sure to assign it in the inspector.");
                yield break;
            }

            slider.value = mapProgress;

            yield return null;
        }
    }
}

