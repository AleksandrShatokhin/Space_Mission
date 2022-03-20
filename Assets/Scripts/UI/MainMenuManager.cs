using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private Button exit;

    void Start()
    {
        start.onClick.AddListener(ToStartGame);
        exit.onClick.AddListener(ToExit);
    }

    void ToStartGame()
    {
        LoadingScreen.SwitchScene("FirstCutscene");
    }

    void ToExit()
    {
        Application.Quit();
    }
}
