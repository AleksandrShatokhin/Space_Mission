using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;

    [SerializeField] private GameObject mainui;
    [SerializeField] private GameObject windowOfGameOver, winWindow, pauseWindow;
    [SerializeField] private bool isDeathPlayer, isPauseMode;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDeathPlayer = false;
        isPauseMode = false;
    }

    public void PauseMode()
    {
        if (!isPauseMode)
        {
            isPauseMode = true;
            mainui.SetActive(false);
            Instantiate(pauseWindow, pauseWindow.transform.position, pauseWindow.transform.rotation);
        }
        else
        {
            GameObject pause = GameObject.Find("PauseWindow(Clone)");

            isPauseMode = false;
            mainui.SetActive(true);
            Destroy(pause);
        }

    }

    public void WinPlayer()
    {
        isPauseMode = true;
        Instantiate(winWindow, winWindow.transform.position, winWindow.transform.rotation);
    }

    public void DeathPlayer()
    {
        isDeathPlayer = true;
        Instantiate(windowOfGameOver, windowOfGameOver.transform.position, windowOfGameOver.transform.rotation);
    }

    public bool IsDeathPlayer()
    {
        return isDeathPlayer;
    }

    public bool IsPauseMode()
    {
        return isPauseMode;
    }

    public List<MeshCollider> GetPlane()
    {
        return this.gameObject.GetComponent<Spawn>().planes;
    }
}
