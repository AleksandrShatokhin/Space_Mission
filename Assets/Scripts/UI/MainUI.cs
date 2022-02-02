using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI bulletsInScreen;

    public GameObject player;
    public Slider healthBar;

    void Awake()
    {
        bulletsInScreen.text = null;

        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        bulletsInScreen.text = " " + player.GetComponent<PlayerController>().GetCurrentBullet() + " / " + player.GetComponent<PlayerController>().GetMaxBullet();
        healthBar.value = player.GetComponent<HealthComponent>().GetHealth();
    }
}
