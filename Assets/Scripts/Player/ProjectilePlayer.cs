using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    private Rigidbody rb_Projectile;
    private float forceRate = 40.0f;
    private int damage = 1;

    void Start()
    {
        rb_Projectile = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        rb_Projectile.AddForce(transform.forward * forceRate, ForceMode.Impulse);

        Destroy(gameObject, 3.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);

        if (other.gameObject.layer == (int)Layers.Enemy)
        {
            // наносим урон
            other.gameObject.GetComponent<HealthComponent>().ChangeHealth(damage);

            // при необходимости обернуть попаданием выстрела на себя вражесского персонажа
            // проверяем по какому Enemy попадаем
            if (other.gameObject.name == "EnemyBlue")
            {
                other.gameObject.GetComponent<EnemyBlue>().IsPersecution(true);
            }

            if (other.gameObject.name == "EnemyRed")
            {
                other.gameObject.GetComponent<EnemyRed>().IsPersecution(true);
            }
        }
    }
}
