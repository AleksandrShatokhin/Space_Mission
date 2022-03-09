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
                MoveEnemyPatrolling();
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

    // ���������� ����� ������ ������
    public override void Attack()
    {
        // ���� ����� �� ��������� �����
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

    // ���������� �������� ���������� ���������, ����� �� ��������������� � ������� (����� ������)
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

    // ���������� �������� ���������� ���������, ����� �� �� ��������������� � �������
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

    // �������� ������� � �������� ����� ����� ��� �������� ���������� ���������
    // ��� ����, ���� ��������� �������� ������� ������������ � �����
    IEnumerator CreateNewRandomPoint()
    {
        yield return new WaitForSeconds(1.5f);
        
        CheckPlaneContact();
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
