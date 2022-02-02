using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int health;

    public void ChangeHealth(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GetComponentInParent<IDeathable>().Kill();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
