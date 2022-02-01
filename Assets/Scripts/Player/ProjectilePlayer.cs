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
        if (other.gameObject.layer == 6 || other.gameObject.layer == 7) // 6 - Ground, 7 - Enemy
        {
            Destroy(this.gameObject);

            if (other.gameObject.layer == 7)
            {
                other.gameObject.GetComponent<HealthComponent>().ChangeHealth(damage);
                other.gameObject.GetComponent<EnemyController>().IsPersecution(true);
            }
        }
    }
}
