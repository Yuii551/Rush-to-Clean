using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSceneController : MonoBehaviour
{
    public static int selectedMap;
    void Start()
    {
        Debug.Log("Selected Map: " + selectedMap);
    }
}
