using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int health;

    public void ChangeHealth(int hp)
    {
        health -= hp;

        if (health <= 0)
        {
            GetComponentInParent<IDeathable>().Kill();
        }
    }
}
