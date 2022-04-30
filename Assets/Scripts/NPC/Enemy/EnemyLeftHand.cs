using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftHand : MonoBehaviour
{
    private GameObject targetPlayer;
    private int damage;
    [SerializeField] private AudioClip audioHandHit;

    // переменная для контролирования урона от коллайдера руки игроку
    public static bool isHandHitThePlayer;

    private void Start()
    {
        targetPlayer = GameObject.Find("Player");
        isHandHitThePlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetPlayer && !isHandHitThePlayer)
        {
            damage = Random.Range(10, 20);

            GameController.GetInstance().PlayAudio(audioHandHit);
            targetPlayer.GetComponent<HealthComponent>().ChangeHealth(damage);
            isHandHitThePlayer = true;
        }
    }
}
