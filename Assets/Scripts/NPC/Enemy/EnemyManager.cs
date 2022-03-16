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

    // переменная понадобилась для упрощения хранения информации по столкновению,
    // чтоб удобней было реализовать обращения к методу у дочернего скрипта
    protected Collision col;

    //---------------------------------------------------------------------------------------------------------

    // каждый enemy должен реализовывать конкретные действия
    // 1 - движение (патрулирование) по отведенной ему территории
    public abstract void MoveEnemyPatrolling();

    // 2 - двигаться на игрока (преследовать) в случае обнаружения
    public abstract void MoveWhenSeePlayer();

    // 3 - нападать на игрока (атаковать) при короткой дистанции
    public abstract void Attack();
    

    // вражеский персонаж должен переходить в режим преследования, если в него выстрелить
    // для реализации этого пока сделаю данный код
    public bool IsPersecution(bool variable)
    {
        isPersecution = variable;
        return isPersecution;
    }

    // проверить объект, на котором висит скрипт, чтоб точки для движения задавались разные
    protected void CheckPlaneContact()
    {
        List<MeshCollider> currentPlane = GameController.GetInstance().GetPlane();
        float posY = 0.01962253f; // позиция по Y для движения вражеский персонажей

        // вражеский персонаж касается плоскости перовой комнаты
        // задаем движение в пределах этой комнаты
        if (col.gameObject.transform.name == currentPlane[0].name)
        {
            pointForMove = new Vector3(Random.Range(-8, 8), posY, Random.Range(-4, 34));
        }

        // вражеский персонаж касается плоскости второй комнаты
        // задаем движение в пределах этой комнаты
        if (col.gameObject.transform.name == currentPlane[1].name)
        {
            pointForMove = new Vector3(Random.Range(-28, 28), posY, Random.Range((float)36.5, 43));
        }

        // вражеский персонаж касается плоскости склада
        // задаем движение в пределах этой комнаты
        if (col.gameObject.transform.name == currentPlane[2].name)
        {
            pointForMove = new Vector3(Random.Range((float)-50.5, (float)-31.5), posY, Random.Range((float)31.5, (float)48.5));
        }

        // вражеский персонаж касается плоскости второго (правого) коридора
        // задаем движение в пределах этой комнаты
        if (col.gameObject.transform.name == currentPlane[3].name)
        {
            pointForMove = new Vector3(Random.Range((float)31.2, (float)38.5), posY, Random.Range((float)36.2, (float)73.5));
        }

        // вражеский персонаж касается плоскости дальнего длинного коридора
        // задаем движение в пределах этой комнаты
        if (col.gameObject.transform.name == currentPlane[4].name)
        {
            pointForMove = new Vector3(Random.Range((float)-28.5, (float)58.5), posY, Random.Range((float)76.2, (float)83.5));
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        // проверка контакта срабатывала срабатывала несколько раз, добавил доп.проверку
        if (pointForMove == new Vector3(0, 0, 0))
        {
            col = other;
            CheckPlaneContact();
        }
    }
}