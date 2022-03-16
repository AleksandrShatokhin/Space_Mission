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
    }

    private void Update()
    {
        bool isPause = GameController.GetInstance().IsPauseMode();

        // для избежания ошибки, после уничтожения игрока появлятся потеря targetPlayer
        if (targetPlayer == null)
        {
            return;
        }

        if (isPause)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            FollowThePlayer();
        }
    }

    public override void FollowThePlayer()
    {
        distanceToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

        if (FieldOfView())
        {
            agent.SetDestination(targetPlayer.transform.position);
            anim_friedly.SetBool("isWalk", true);
            anim_friedly.SetBool("isCrouch", false);
            agent.stoppingDistance = 3;

            if (distanceToPlayer <= 3)
            {
                anim_friedly.SetBool("isWalk", false);
            }
        }
        else
        {
            anim_friedly.SetBool("isCrouch", true);
            anim_friedly.SetBool("isWalk", false);
            agent.stoppingDistance = 0;
        }
    }
}
