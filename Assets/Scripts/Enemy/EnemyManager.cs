using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyManager : MonoBehaviour, IDeathable
{
    [SerializeField] protected Transform targetPlayer;
    protected float distanceToPlayer;

    protected NavMeshAgent agent;
    protected Animator anim_enemy;

    protected Vector3 pointForMove;

    [SerializeField] protected float radiusView;
    [SerializeField] [Range(0, 360)] protected float angleView;

    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected LayerMask obstacleMask;

    [SerializeField] protected bool canSeePlayer, isPersecution;

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

    // ���� ������ ��������� ���������� ������ ������
    protected bool FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusView, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleView / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;

        return canSeePlayer;
    }

    // ��������� �������� ������ ���������� � ����� �������������, ���� � ���� ����������
    // ��� ���������� ����� ���� ������ ������ ���
    public bool IsPersecution(bool variable)
    {
        isPersecution = variable;
        return isPersecution;
    }

    // ���������� ���������� �� HealthComponent
    void IDeathable.Kill()
    {
        Debug.Log("Enemy killed");

        Destroy(this.gameObject);
    }
}
