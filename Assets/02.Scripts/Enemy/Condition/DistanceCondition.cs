using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCondition : AICondition
{
    [SerializeField]
    private float _distance;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return Vector3.Distance(_aiBrain.Target.transform.position, transform.position) <= _distance;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _distance);
    }
#endif
}
