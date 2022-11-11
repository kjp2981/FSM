using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RommingState : AIState
{
    [SerializeField]
    private Transform[] rommingPoint;

    private Transform nextRommingPos;
    private int nextRommingIdx = 0;

    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = _aiBrain.GetComponentInParent<NavMeshAgent>();

        nextRommingIdx = 0;
    }

    public override void OnStateEnter()
    {
        nextRommingPos = GetNextRommingPos();

        agent.SetDestination(nextRommingPos.position);
    }

    public override void TakeAAction()
    {
        
    }

    public Transform GetNextRommingPos()
    {
        if (rommingPoint.Length <= 0)
            return null;

        nextRommingIdx = (nextRommingIdx + 1) % rommingPoint.Length;

        Transform rommingPos = rommingPoint[nextRommingIdx];

        return rommingPos;
    }
}
