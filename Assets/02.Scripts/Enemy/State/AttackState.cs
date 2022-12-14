using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : AIState
{
    [SerializeField]
    private float attackDelay = 1f;
    [SerializeField]
    private float damage = 2f;

    private float attackTimer = 0f;

    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        agent.isStopped = true;
        attackTimer = attackDelay;
    }

    public override void OnStateLeave()
    {

    }

    public override void TakeAAction()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackDelay)
        {
            IHittable hit = _aiBrain.Target.GetComponent<IHittable>();
            hit?.Damage(damage, this.gameObject);
        }
    }
}
