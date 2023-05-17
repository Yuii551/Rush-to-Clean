using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public Button mapButton;
    public string gameSceneName;
    public int mapNumber;

    private void Awake()
    {
        mapButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(gameSceneName);
        GameSceneController.selectedMap = mapNumber; // You may need to adjust this line based on your specific implementation
    }
}
