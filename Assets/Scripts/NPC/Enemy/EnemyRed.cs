using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRed : EnemyManager
{
    [SerializeField] private GameObject fireball, spawnFire;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim_enemy = this.GetComponent<Animator>();

        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();

        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        // ��� ��������� ������, ����� ����������� ������ ��������� ������ targetPlayer
        if (targetPlayer == null)
        {
            return;
        }

        distanceToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

        if (!isPersecution)
        {
            // ���� ����� ��������� �� � ���� ������
            if (!FieldOfView())
            {
                base.MoveEnemyPatrolling();
            }

            // ������� �������, ���� ��� ����� ��������� �� �������� ��������� ��� �������� �� �����
            // ������� ��������� �������� ��� �������� (������) � �������������� �� ������
            if (distanceToPlayer <= 2 && !FieldOfView())
            {
                IsPersecution(true);
            }

            // ���� ������ ������� ��������� ��������
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

    // ������ void ���������� �� Event � �������� FireballAttack
    public void CreateFireball()
    {
        Instantiate(fireball, spawnFire.transform.position, Quaternion.identity);
    }

    void AttackFireBall()
    {
        float speedRotate = 7.0f;

        // � ������ ����� ������������ ������������ ���������� ��������� �� �������
        Vector3 relativePos = targetPlayer.position - transform.position;
        Quaternion rotationEnemy = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationEnemy, speedRotate * Time.deltaTime);

        if (distanceToPlayer <= 10 && distanceToPlayer > 2 && FieldOfView())
        {
            anim_enemy.SetBool("isWalk", false);
            anim_enemy.SetBool("isFireballAttack", true);
        }
        else
        {
            anim_enemy.SetBool("isFireballAttack", false);
        }
    }

    // ���������� �������� ���������� ���������, ����� �� ��������������� � ������� (����� ������)
    public override void MoveWhenSeePlayer()
    {
        if (distanceToPlayer > 10)
        {
            agent.SetDestination(targetPlayer.transform.position);
            agent.stoppingDistance = 10;
            anim_enemy.SetBool("isWalk", true);
        }

        AttackFireBall();

        // ������� ��������� �������� ���� ����� �����������
        // ���� ����� ��� �������� ���������
        base.AttackHand();
    }

    // �������� ���������� �������� ����� �� ��������� �������� ������ ������
    IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfView();
        }
    }
}