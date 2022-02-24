using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNPC : MonoBehaviour, IDeathable
{
    [SerializeField] protected float radiusView;
    [SerializeField] [Range(0, 360)] protected float angleView;

    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected LayerMask obstacleMask;

    [SerializeField] protected bool canSeePlayer;

    protected bool FieldOfView()
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

        return canSeePlayer;
    }

    // Реализация интерфейса по HealthComponent
    void IDeathable.Kill()
    {
        Destroy(this.gameObject);
    }
}
