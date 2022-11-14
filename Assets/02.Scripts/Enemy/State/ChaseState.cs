using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : AIState
{
    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponentInParent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        agent.isStopped = false;
    }

    public override void OnStateLeave()
    {

    }

    public override void TakeAAction()
    {
        agent.SetDestination(_aiBrain.Target.transform.position);
    }
}
