using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class FriendlyManager : ControllerNPC
{
    [SerializeField] protected Transform targetPlayer;

    protected float distanceToPlayer;

    protected NavMeshAgent agent;
    protected Animator anim_friedly;

    public abstract void FollowThePlayer();
}
