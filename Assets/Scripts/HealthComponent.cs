using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;

    public void ChangeHealth(float hp)
    {
        health -= hp;

        if (health <= 0)
        {
            GetComponentInParent<IDeathable>().Kill();
        }
    }
}
