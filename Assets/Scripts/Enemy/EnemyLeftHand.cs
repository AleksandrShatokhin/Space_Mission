using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftHand : MonoBehaviour
{
    private GameObject targetPlayer;
    private int damage = 1;

    private void Start()
    {
        targetPlayer = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetPlayer)
        {
            targetPlayer.GetComponent<HealthComponent>().ChangeHealth(damage);
        }
    }
}
