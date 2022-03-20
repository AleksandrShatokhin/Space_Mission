using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlue : EnemyManager
{
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim_enemy = this.GetComponent<Animator>();

        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();

        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        // для избежания ошибки, после уничтожения игрока появлятся потеря targetPlayer
        if (targetPlayer == null)
        {
            return;
        }

        distanceToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

        if (!isPersecution)
        {
            // если игрок находится не в поле зрения
            if (!FieldOfView())
            {
                base.MoveEnemyPatrolling();
            }

            // добавим условие, если наш игрок находится на короткой дистанции или подходит со спины
            // условно вражеский персонаж его замечает (слышит) и поворачивается на игрока
            if (distanceToPlayer <= 2 && !FieldOfView())
            {
                IsPersecution(true);
            }

            // если игрока заметил вражеский персонаж
            if (FieldOfView())
            {
                IsPersecution(true);
            }
        }
        else
        {
            MoveWhenSeePlayer();
        }
    }

    // реализация движения вражеского персонажа, когда он взаимодействует с игроком (видит игрока)
    public override void MoveWhenSeePlayer()
    {
        if (distanceToPlayer > 2)
        {
            agent.SetDestination(targetPlayer.transform.position);
            agent.stoppingDistance = 1;
            anim_enemy.SetBool("isWalk", true);
        }

        base.AttackHand();
    }

    // создадим постоянную проверку может ли вражеский персонаж видеть игрока
    IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfView();
        }
    }
}