using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionPair
{
    public List<AICondition> conditionList;
    public AIState nextState;
    public int priority;
}

public abstract class AIState : MonoBehaviour
{
    protected AIBrain _aiBrain = null;

    public List<ConditionPair> _transitionList;

    protected virtual void Start()
    {
        _aiBrain = GetComponentInParent<AIBrain>();
    }

    public virtual void OnStateEnter() { }

    public virtual void OnStateLeave() { }

    public abstract void TakeAAction();
}
