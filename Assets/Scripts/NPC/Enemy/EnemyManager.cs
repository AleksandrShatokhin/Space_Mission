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
    protected AudioSource audioSourceEnemy;

    [SerializeField] protected AudioClip scream;

    protected Vector3 pointForMove;

    [SerializeField] protected bool isPersecution;

    // ���������� ������������ ��� ��������� �������� ���������� �� ������������,
    // ���� ������� ���� ����������� ��������� � ������ � ��������� �������
    protected Collision col;

    //---------------------------------------------------------------------------------------------------------

    protected void ConnectingTheMainComponents()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim_enemy = this.GetComponent<Animator>();
        audioSourceEnemy = this.GetComponent<AudioSource>();
    }

    protected IEnumerator Scream(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            audioSourceEnemy.PlayOneShot(scream, 0.5f);
            anim_enemy.SetTrigger("isScream");
        }
    }

    // ������ enemy ������ ������������� ���������� ��������
    // 1 - �������� (��������������) �� ���������� ��� ����������
    public virtual void MoveEnemyPatrolling()
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

    // 2 - ��������� �� ������ (������������) � ������ �����������
    public abstract void MoveWhenSeePlayer();

    // 3 - �������� �� ������ (���������) ��� �������� ���������
    public virtual void AttackHand()
    {
        // ���� ����� �� ��������� �����
        if (distanceToPlayer <= 2 && FieldOfView())
        {
            anim_enemy.SetBool("isWalk", false);
            anim_enemy.SetBool("isFireballAttack", false);
            anim_enemy.SetBool("isAttack", true);
        }
        else
        {
            anim_enemy.SetBool("isAttack", false);
        }
    }
    

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
        float posY = 0.08333334f; // ������� �� Y ��� �������� ��������� ����������

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

    // �������� ������� � �������� ����� ����� ��� �������� ���������� ���������
    // ��� ����, ���� ��������� �������� ������� ������������ � �����
    IEnumerator CreateNewRandomPoint()
    {
        yield return new WaitForSeconds(1.5f);

        CheckPlaneContact();
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

    // Event �� ������ �������� ����� �����
    // �������� ����������� �������� ���� �����
    public void IsHandHitThePlayerToFalse()
    {
        EnemyLeftHand.isHandHitThePlayer = false;
    }

    public void DeathEnemy()
    {
        anim_enemy.SetTrigger("isDeath");
        Destroy(gameObject, 1.0f);
    }
}