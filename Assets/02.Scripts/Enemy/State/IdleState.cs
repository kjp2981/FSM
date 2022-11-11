using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : AIState
{
    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = _aiBrain.GetComponentInParent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        agent.isStopped = true;
    }

    public override void TakeAAction()
    {
        
    }
}
