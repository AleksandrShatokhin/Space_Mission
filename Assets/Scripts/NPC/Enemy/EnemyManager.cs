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
    protected void CheckNameEnemy()
    {
        string name = this.gameObject.transform.name;

        switch (name)
        {
            case "Biomech_Mutant_Room1":
                pointForMove = new Vector3(Random.Range(-8, 8), transform.position.y, Random.Range(-4, 34));
                break;

            case "Biomech_Mutant_Hallway":
                pointForMove = new Vector3(Random.Range(-28, 28), transform.position.y, Random.Range((float)36.5, 43));
                break;

            case "Biomech_Mutant_Skin_1_bloody":
                pointForMove = new Vector3(Random.Range(-28, 28), transform.position.y, Random.Range((float)36.5, 43));
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
