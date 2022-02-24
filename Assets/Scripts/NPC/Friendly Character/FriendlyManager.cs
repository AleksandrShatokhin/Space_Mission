using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class FriendlyManager : ControllerNPC
{
    [SerializeField] protected Transform targetPlayer;

    protected NavMeshAgent agent;

    public abstract void FollowThePlayer();
}
