using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenObsolateCondition : AICondition
{
    [SerializeField]
    private LayerMask _obsolateLayer;
    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        Vector3 direction = _aiBrain.Target.transform.position - _aiBrain.transform.parent.transform.position;
        float distance = Vector3.Distance(_aiBrain.Target.transform.position, _aiBrain.transform.parent.transform.position);

        return Physics.Raycast(transform.position, direction, distance, _obsolateLayer);
    }
}
