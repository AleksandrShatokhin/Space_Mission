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

    // ���������� ������������ ��� ��������� �������� ���������� �� ������������,
    // ���� ������� ���� ����������� ��������� � ������ � ��������� �������
    protected Collision col;

    //---------------------------------------------------------------------------------------------------------

    // ������ enemy ������ ������������� ���������� ��������
    // 1 - �������� (��������������) �� ���������� ��� ����������
    public abstract void MoveEnemyPatrolling();

    // 2 - ��������� �� ������ (������������) � ������ �����������
    public abstract void MoveWhenSeePlayer();

    // 3 - �������� �� ������ (���������) ��� �������� ���������
    public abstract void Attack();
    

    // ��������� �������� ������ ���������� � ����� �������������, ���� � ���� ����������
    // ��� ���������� ����� ���� ������ ������ ���
    public bool IsPersecution(bool variable)
    {
        isPersecution = variable;
        return isPersecution;
    }

    // ��������� ������, �� ������� ����� ������, ���� ����� ��� �������� ���������� ������
    protected void CheckPlaneContact()
    {
        List<MeshCollider> currentPlane = GameController.GetInstance().GetPlane();
        float posY = 0.01962253f; // ������� �� Y ��� �������� ��������� ����������

        // ��������� �������� �������� ��������� ������� �������
        // ������ �������� � �������� ���� �������
        if (col.gameObject.transform.name == currentPlane[0].name)
        {
            pointForMove = new Vector3(Random.Range(-8, 8), posY, Random.Range(-4, 34));
        }

        // ��������� �������� �������� ��������� ������ �������
        // ������ �������� � �������� ���� �������
        if (col.gameObject.transform.name == currentPlane[1].name)
        {
            pointForMove = new Vector3(Random.Range(-28, 28), posY, Random.Range((float)36.5, 43));
        }

        // ��������� �������� �������� ��������� ������
        // ������ �������� � �������� ���� �������
        if (col.gameObject.transform.name == currentPlane[2].name)
        {
            pointForMove = new Vector3(Random.Range((float)-50.5, (float)-31.5), posY, Random.Range((float)31.5, (float)48.5));
        }

        // ��������� �������� �������� ��������� ������� (�������) ��������
        // ������ �������� � �������� ���� �������
        if (col.gameObject.transform.name == currentPlane[3].name)
        {
            pointForMove = new Vector3(Random.Range((float)31.2, (float)38.5), posY, Random.Range((float)36.2, (float)73.5));
        }

        // ��������� �������� �������� ��������� �������� �������� ��������
        // ������ �������� � �������� ���� �������
        if (col.gameObject.transform.name == currentPlane[4].name)
        {
            pointForMove = new Vector3(Random.Range((float)-28.5, (float)58.5), posY, Random.Range((float)76.2, (float)83.5));
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        // �������� �������� ����������� ����������� ��������� ���, ������� ���.��������
        if (pointForMove == new Vector3(0, 0, 0))
        {
            col = other;
            CheckPlaneContact();
        }
    }
}