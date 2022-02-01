using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDeathable
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float distanceToPlayer;

    private NavMeshAgent agent;
    private Animator anim_enemy;

    private Vector3 pointForMove;

    [SerializeField] private float radiusView;
    [SerializeField] [Range(0, 360)] private float angleView;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [SerializeField] private bool canSeePlayer, isPersecution;
    
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim_enemy = this.GetComponent<Animator>();

        pointForMove = new Vector3(Random.Range(-8, 8), transform.position.y, Random.Range(-4, 34));

        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        Debug.Log(pointForMove);

        // ��� ��������� ������, ����� ����������� ������ ��������� ������ targetPlayer
        if (targetPlayer == null)
        {
            return;
        }

        distanceToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

        if (!isPersecution)
        {
            // ���� ����� ��������� �� � ���� ������
            if (!canSeePlayer)
            {
                MoveEnemyPatrolling();
            }

            // ������� �������, ���� ��� ����� ��������� �� �������� ��������� ��� �������� �� �����
            // ������� ��������� �������� ��� �������� (������) � �������������� �� ������
            if (distanceToPlayer <= 2 && !canSeePlayer)
            {
                //Vector3 directionToPlayer = targetPlayer.transform.position - transform.position;
                //Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer, Vector3.up);
                //Quaternion rotationEnemy = Quaternion.Lerp(transform.rotation, rotationToPlayer, 3 * Time.deltaTime);
                //transform.rotation = rotationEnemy;
                IsPersecution(true);
            }

            // ���� ������ ������� ��������� ��������
            if (canSeePlayer)
            {
                IsPersecution(true);
            }
        }
        else
        {
            MoveWhenSeePlayer();
        }
    }

    // ����� �������� ���������� ���������, ����� �� ��������������� � �������
    void MoveWhenSeePlayer()
    {
        if (distanceToPlayer > 2)
        {
            agent.SetDestination(targetPlayer.transform.position);
            agent.stoppingDistance = 1;
        }

        // ���� ����� �� ��������� �����
        if (distanceToPlayer <= 2 && !GameController.GetInstance().IsDeathPlayer() && canSeePlayer)
        {
            anim_enemy.SetBool("isWalk", false);
            anim_enemy.SetBool("isAttack", true);
        }
        else
        {
            anim_enemy.SetBool("isAttack", false);
        }
    }

    // ����� �������� ���������� ���������, ����� �� �� ��������������� � �������
    void MoveEnemyPatrolling()
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
        pointForMove = new Vector3(Random.Range(-8, 8), transform.position.y, Random.Range(-4, 34));
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

    // ����� ������� ������
    void FieldOfView()
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
    }

    public bool IsPersecution(bool variable)
    {
        isPersecution = variable;
        return isPersecution;
    }

    // ���������� ����������
    void IDeathable.Kill()
    {
        Debug.Log("Enemy killed");

        Destroy(this.gameObject);
    }
}
