using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyManager : ControllerNPC
{
    [SerializeField] protected Transform targetPlayer;
    protected float distanceToPlayer;

    protected NavMeshAgent agent;
    protected Animator anim_enemy;

    protected Vector3 pointForMove;

    [SerializeField] protected bool isPersecution;

    // ������ enemy ������ ������������� ���������� ��������
    // 1 - �������� (��������������) �� ���������� ��� ����������
    public abstract void MoveEnemyPatrolling();

    // 2 - ��������� �� ������ (������������) � ������ �����������
    public abstract void MoveWhenSeePlayer();

    // 3 - �������� �� ������ (���������) ��� �������� ���������
    public abstract void Attack();


    // ��������� ������, �� ������� ����� ������, ���� ����� ��� �������� ���������� ������
    protected void CheckRoomForEnemy()
    {
        StartCoroutine(DelayCreatePointForMove());
    }

    IEnumerator DelayCreatePointForMove()
    {
        // ������� ��������� �������, ��� ��� ������� ������ ������������ ��������� �������� � ����� � ����� ���������� ��� (���� ��� �������)
        yield return new WaitForSeconds(0.1f);

        int plane = GameObject.Find("Game").GetComponent<Spawn>().RandPlane();

        switch (plane)
        {
            case (int)Rooms.Room1:
                pointForMove = new Vector3(Random.Range(-8, 8), transform.position.y, Random.Range(-4, 34));
                break;

            case (int)Rooms.Hallway:
                pointForMove = new Vector3(Random.Range(-28, 28), transform.position.y, Random.Range((float)36.5, 43));
                break;

            case (int)Rooms.Warehouse:
                pointForMove = new Vector3(Random.Range((float)-50.5, (float)-31.5), transform.position.y, Random.Range((float)31.5, (float)48.5));
                break;
        }
    }

    // ��������� �������� ������ ���������� � ����� �������������, ���� � ���� ����������
    // ��� ���������� ����� ���� ������ ������ ���
    public bool IsPersecution(bool variable)
    {
        isPersecution = variable;
        return isPersecution;
    }
}
