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

    // ���������� �������� ���������� ���������, ����� �� ��������������� � ������� (����� ������)
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