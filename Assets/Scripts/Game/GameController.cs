using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;

    // ������� ����
    [SerializeField] private GameObject mainui;
    [SerializeField] private GameObject windowOfGameOver, winWindow, pauseWindow;

    // �������
    [SerializeField] private bool isDeathPlayer, isPauseMode, isWinPlayer;

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
    }

    public void PauseMode()
    {
        if (!isPauseMode)
        {
            isPauseMode = true;
            TimeSpeedInGame();
            mainui.SetActive(false);
            Instantiate(pauseWindow, pauseWindow.transform.position, pauseWindow.transform.rotation);
        }
        else
        {
            GameObject pause = GameObject.Find("PauseWindow(Clone)");

            isPauseMode = false;
            TimeSpeedInGame();
            mainui.SetActive(true);
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

    // ���������� ��������� ������� ������� � ���� (�����)
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
}
