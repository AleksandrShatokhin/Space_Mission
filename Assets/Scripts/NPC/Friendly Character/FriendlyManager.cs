using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class FriendlyManager : ControllerNPC
{
    [SerializeField] protected Transform targetPlayer;
    [SerializeField] protected Transform targetSpaceShip;

    protected float distanceToPlayer;
    protected bool switchTarget;

    protected NavMeshAgent agent;
    protected Animator anim_friedly;

    public virtual void FollowThePlayer()
    {
        distanceToPlayer = Vector3.Distance(targetPlayer.position, transform.position);

        if (FieldOfView())
        {
            agent.SetDestination(targetPlayer.position);
            anim_friedly.SetBool("isWalk", true);
            agent.stoppingDistance = 3;

            if (distanceToPlayer <= 3)
            {
                anim_friedly.SetBool("isWalk", false);
            }
        }
        else
        {
            anim_friedly.SetBool("isWalk", false);
            agent.stoppingDistance = 0;
        }
    }

    public bool SwitchTarget(bool variable)
    {
        switchTarget = variable;
        return switchTarget;
    }

    protected void FollowTheSpaceShip()
    {
        agent.SetDestination(targetSpaceShip.position);
        anim_friedly.SetBool("isWalk", true);
        agent.stoppingDistance = 0;
    }
}
