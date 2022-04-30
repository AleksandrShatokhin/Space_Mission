using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyController : FriendlyManager
{
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim_friedly = this.GetComponent<Animator>();

        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();
        targetSpaceShip = GameObject.Find("PlayerShip").GetComponent<Transform>();

        switchTarget = false;
    }

    private void Update()
    {
        // для избежания ошибки, после уничтожения игрока появлятся потеря targetPlayer
        if (targetPlayer == null)
        {
            return;
        }

        if (!switchTarget)
        {
            base.FollowThePlayer();
        }
        else
        {
            base.FollowTheSpaceShip();
        }

    }
}
