using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyController : FriendlyManager
{
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FollowThePlayer();
    }

    public override void FollowThePlayer()
    {
        if (FieldOfView())
        {
            agent.SetDestination(targetPlayer.transform.position);
            agent.stoppingDistance = 3;
        }
        else
        {
            agent.stoppingDistance = 0;
        }
    }
}
