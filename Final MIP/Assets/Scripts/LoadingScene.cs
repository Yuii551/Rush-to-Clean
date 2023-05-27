using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider slider;
    [SerializeField] private float delayDuration = 2f; // Adjust the delay duration here

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        // Show the loading scene
        loadingScene.SetActive(true);

        // Wait for the specified delay duration
        yield return new WaitForSeconds(delayDuration);

        // Start loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // While the scene is loading
        while (!operation.isDone)
        {
            // Calculate the progress of the scene loading (0 to 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Update the slider value accordingly
            slider.value = progress;

            yield return null;
        }
    }


}
