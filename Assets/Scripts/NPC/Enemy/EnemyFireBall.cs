using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour
{
    private int damage;
    private GameObject target;
    [SerializeField] private GameObject blast;

    private float forceRate = 20.0f;
    private Rigidbody rb_Fireball;

    void Start()
    {
        target = GameObject.Find("Player");

        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotationTestObject = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationTestObject;

        rb_Fireball = GetComponent<Rigidbody>();
        rb_Fireball.AddForce(transform.forward * forceRate, ForceMode.Impulse);

        Destroy(this.gameObject, 3.0f);
    }

    private void OnDestroy()
    {
        Instantiate(blast, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);

        if (other.gameObject == target)
        {
            damage = Random.Range(5, 15);

            target.GetComponent<HealthComponent>().ChangeHealth(damage);
        }
    }
}
