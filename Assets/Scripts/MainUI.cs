using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI bulletsInScreen;
    public GameObject player;

    void Start()
    {
        bulletsInScreen.text = null;

        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        bulletsInScreen.text = " " + player.GetComponent<PlayerController>().GetCurrentBullet() + " / " + player.GetComponent<PlayerController>().GetMaxBullet();
    }
}
