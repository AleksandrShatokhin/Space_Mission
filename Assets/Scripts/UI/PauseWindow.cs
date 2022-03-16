using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private Button clickRestart, clickExit;

    void Start()
    {
        clickRestart.onClick.AddListener(ToRestart);
        clickExit.onClick.AddListener(ToExitInMenu);
    }

    void ToRestart()
    {
        LoadingScreen.SwitchScene("Level1");
    }

    void ToExitInMenu()
    {
        LoadingScreen.SwitchScene("MainMenu");
    }
}
