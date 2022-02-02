using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftHand : MonoBehaviour
{
    private GameObject targetPlayer;
    private int damage;

    private void Start()
    {
        targetPlayer = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetPlayer)
        {
            damage = Random.Range(5, 15);

            targetPlayer.GetComponent<HealthComponent>().ChangeHealth(damage);
        }
    }
}
