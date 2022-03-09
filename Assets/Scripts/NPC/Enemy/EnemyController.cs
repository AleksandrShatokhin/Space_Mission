using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyManager
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
                MoveEnemyPatrolling();
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

    // реализация атаки нашего игрока
    public override void Attack()
    {
        // если игрок на дистанции атаки
        if (distanceToPlayer <= 2 && !GameController.GetInstance().IsDeathPlayer() && FieldOfView())
        {
            anim_enemy.SetBool("isWalk", false);
            anim_enemy.SetBool("isAttack", true);
        }
        else
        {
            anim_enemy.SetBool("isAttack", false);
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

        Attack();
    }

    // реализация движения вражеского персонажа, когда он не взаимодействует с игроком
    public override void MoveEnemyPatrolling()
    {
        if (transform.position != pointForMove)
        {
            agent.SetDestination(pointForMove);
            agent.stoppingDistance = 0;
            anim_enemy.SetBool("isWalk", true);
        }
        else
        {
            anim_enemy.SetBool("isWalk", false);
            agent.stoppingDistance = 1;
            StartCoroutine(CreateNewRandomPoint());
        }
    }

    // создадим задерку в создании новой точки для движения врежеского персонажа
    // для того, чтоб вражеский персонаж немного задерживался в точке
    IEnumerator CreateNewRandomPoint()
    {
        yield return new WaitForSeconds(1.5f);
        
        CheckPlaneContact();
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
