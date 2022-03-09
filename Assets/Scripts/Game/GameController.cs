using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;


    [SerializeField] private GameObject WindowOfGameOver;
    [SerializeField] private bool isDeathPlayer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDeathPlayer = false;
    }

    public void WinPlayer()
    {
        Debug.Log("You saved all your friends");
    }

    public void DeathPlayer()
    {
        isDeathPlayer = true;
        Instantiate(WindowOfGameOver, WindowOfGameOver.transform.position, WindowOfGameOver.transform.rotation);
    }

    public bool IsDeathPlayer()
    {
        return isDeathPlayer;
    }

    public List<MeshCollider> GetPlane()
    {
        return this.gameObject.GetComponent<Spawn>().planes;
    }
}
