using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICondition : MonoBehaviour
{
    protected AIBrain _aiBrain;

    protected virtual void Awake()
    {
        _aiBrain = GetComponentInParent<AIBrain>();
    }

    public abstract bool IfCondition(AIState currentState, AIState nextState);
}

