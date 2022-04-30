using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    private AudioSource audioSourceMainMenu;

    [SerializeField] private Button start;
    [SerializeField] private Button exit;

    void Start()
    {
        audioSourceMainMenu = GetComponent<AudioSource>();

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
