using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;

    // игровые окна
    [SerializeField] private GameObject mainui;
    [SerializeField] private GameObject windowOfGameOver, winWindow, pauseWindow;

    // статусы
    [SerializeField] private bool isDeathPlayer, isPauseMode, isWinPlayer;

    private AudioSource gameAudio;
    [SerializeField] private AudioClip audioPause;


    //-----------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDeathPlayer = false;
        isPauseMode = false;
        isWinPlayer = false;

        gameAudio = GetComponent<AudioSource>();
    }

    public void PauseMode()
    {
        if (!isPauseMode)
        {
            isPauseMode = true;
            TimeSpeedInGame();
            mainui.SetActive(false);
            PlayAudio(audioPause);
            Instantiate(pauseWindow, pauseWindow.transform.position, pauseWindow.transform.rotation);
        }
        else
        {
            GameObject pause = GameObject.Find("PauseWindow(Clone)");

            isPauseMode = false;
            TimeSpeedInGame();
            mainui.SetActive(true);
            PlayAudio(audioPause);
            Destroy(pause);
        }
    }

    public void WinPlayer()
    {
        TimeSpeedInGame();
        isWinPlayer = true;
        isPauseMode = true;
        mainui.SetActive(false);
        Instantiate(winWindow, winWindow.transform.position, winWindow.transform.rotation);
    }

    public void DeathPlayer()
    {
        TimeSpeedInGame();
        isDeathPlayer = true;
        isPauseMode = true;
        mainui.SetActive(false);
        Instantiate(windowOfGameOver, windowOfGameOver.transform.position, windowOfGameOver.transform.rotation);
    }

    // управление скоростью течения времени в игре (пауза)
    public void TimeSpeedInGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1.0f;
        }
        else
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0;
        }
    }

    public bool IsDeathPlayer()
    {
        return isDeathPlayer;
    }

    public bool IsPauseMode()
    {
        return isPauseMode;
    }

    public bool IsWinPlayer()
    {
        return isWinPlayer;
    }

    public List<MeshCollider> GetPlane()
    {
        return this.gameObject.GetComponent<Spawn>().planes;
    }

    public void PlayAudio(AudioClip audio)
    {
        gameAudio.PlayOneShot(audio, 0.5f);
    }
}
