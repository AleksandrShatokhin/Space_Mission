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

    // каждый enemy должен реализовывать конкретные действия
    // 1 - движение (патрулирование) по отведенной ему территории
    public abstract void MoveEnemyPatrolling();

    // 2 - двигаться на игрока (преследовать) в случае обнаружения
    public abstract void MoveWhenSeePlayer();

    // 3 - нападать на игрока (атаковать) при короткой дистанции
    public abstract void Attack();


    // проверить объект, на котором висит скрипт, чтоб точки для движения задавались разные
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

    // вражеский персонаж должен переходить в режим преследования, если в него выстрелить
    // для реализации этого пока сделаю данный код
    public bool IsPersecution(bool variable)
    {
        isPersecution = variable;
        return isPersecution;
    }
}
