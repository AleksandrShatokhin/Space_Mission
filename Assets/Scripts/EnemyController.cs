using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDeathable
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float speedEnemy;
    private int damage = 5;

    [SerializeField] private float distanceToPlayer;

    private NavMeshAgent agent;
    private Animator anim_enemy;
    
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim_enemy = this.GetComponent<Animator>();
    }

    void Update()
    {
        // для избежания ошибки после уничтожения игрока появлятся потеря targetPlayer
        if (targetPlayer == null)
        {
            return;
        }

        distanceToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

        if (distanceToPlayer < 10 && distanceToPlayer > 1)
        {
            //transform.LookAt(targetPlayer, Vector3.up);

            //transform.Translate(Vector3.forward * speedEnemy * Time.deltaTime);

            //Vector3 direction = targetPlayer.transform.position - transform.position;
            //Quaternion rotation = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 15 * Time.deltaTime);

            agent.SetDestination(targetPlayer.transform.position);

            anim_enemy.SetBool("isWalk", true);
        }
        else
        {
            anim_enemy.SetBool("isWalk", false);
        }

        if (distanceToPlayer <= 2 && !GameController.GetInstance().IsDeathPlayer())
        {
            //targetPlayer.GetComponent<HealthComponent>().ChangeHealth(damage);
            anim_enemy.SetBool("isAttack", true);
        }
        else
        {
            anim_enemy.SetBool("isAttack", false);
        }
    }

    // Реализация интерфейса
    void IDeathable.Kill()
    {
        Debug.Log("Enemy killed");

        Destroy(this.gameObject);
    }
    

}
