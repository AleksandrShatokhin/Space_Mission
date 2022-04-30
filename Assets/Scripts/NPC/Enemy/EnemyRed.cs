using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRed : EnemyManager
{
    [SerializeField] private GameObject fireball, spawnFire;
    [SerializeField] private LayerMask doorMask;
    [SerializeField] bool isDoor;

    void Start()
    {
        ConnectingTheMainComponents();

        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();

        StartCoroutine(FOVRoutine());

        float randomDelay = Random.Range(3, 5);
        StartCoroutine(Scream(randomDelay));
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

    // данный void вызывается по Event в анимации FireballAttack
    public void CreateFireball()
    {
        Instantiate(fireball, spawnFire.transform.position, Quaternion.identity);
    }

    private bool CheckDoorOnPath()
    {
        Collider[] colliderPlayer = Physics.OverlapSphere(transform.position, radiusView, playerMask);

        if (colliderPlayer.Length != 0)
        {
            Transform target = colliderPlayer[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleView / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distance, doorMask))
                {
                    isDoor = false;
                }
                else
                {
                    isDoor = true;
                }
            }
        }
        return isDoor;
    }

    void AttackFireBall()
    {
        float speedRotate = 7.0f;

        // в режиме атаки понадобилось поводачивать вражеского персонажа за игроком
        Vector3 relativePos = targetPlayer.position - transform.position;
        Quaternion rotationEnemy = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationEnemy, speedRotate * Time.deltaTime);

        if (distanceToPlayer <= 10 && distanceToPlayer > 2 && FieldOfView())
        {
            agent.isStopped = true;
            anim_enemy.SetBool("isWalk", false);
            anim_enemy.SetBool("isFireballAttack", true);
        }
        else
        {
            anim_enemy.SetBool("isFireballAttack", false);
        }
    }

    // реализация движения вражеского персонажа, когда он взаимодействует с игроком (видит игрока)
    public override void MoveWhenSeePlayer()
    {
        if (distanceToPlayer > 10)
        {
            agent.SetDestination(targetPlayer.transform.position);
            agent.isStopped = false;
            anim_enemy.SetBool("isWalk", true);
        }

        // исключаю стрельбу по игроку, когда разделяет дверь
        // огненые шары летят в игрока сквозь дверь
        if (!CheckDoorOnPath())
        {
            AttackFireBall();
        }
        else
        {
            agent.SetDestination(targetPlayer.transform.position);
            agent.isStopped = false;
            anim_enemy.SetBool("isWalk", true);
        }

        // красный вражеский персонаж тоже имеет возможность
        // бить рукой при короткой дистанции
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