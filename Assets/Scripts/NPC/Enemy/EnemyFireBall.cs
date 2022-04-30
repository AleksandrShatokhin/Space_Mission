using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour
{
    private int damage;
    private GameObject target;

    private float forceRate = 20.0f;
    private Rigidbody rb_Fireball;

    [SerializeField] private AudioClip audioFireBall;

    void Start()
    {
        target = GameObject.Find("TargetForFireball");

        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotationEnemyFireball = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotationEnemyFireball;

        rb_Fireball = GetComponent<Rigidbody>();
        rb_Fireball.AddForce(transform.forward * forceRate, ForceMode.Impulse);

        GameController.GetInstance().PlayAudio(audioFireBall);

        Destroy(this.gameObject, 3.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);

        if (other.gameObject.tag == "Player")
        {
            damage = Random.Range(5, 15);

            other.gameObject.GetComponent<HealthComponent>().ChangeHealth(damage);
            other.gameObject.GetComponent<PlayerController>().PlayAudioPain();
        }
    }
}
