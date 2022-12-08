using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenObsolateCondition : AICondition
{
    [SerializeField]
    private LayerMask _obsolateLayer;

    private RaycastHit ray;
    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        Vector3 direction = _aiBrain.Target.transform.position - _aiBrain.transform.parent.transform.position;
        float distance = Vector3.Distance(_aiBrain.Target.transform.position, _aiBrain.transform.parent.transform.position);
        Physics.Raycast(transform.position, direction, out ray, distance, _obsolateLayer);

        return ray.collider != null;
    }
}
