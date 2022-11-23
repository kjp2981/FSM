using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionAndNot
{
    public AICondition condition;
    public bool not;
}

[System.Serializable]
public class ConditionPair
{
    public List<ConditionAndNot> conditionAndNotList;
    public AIState nextState;
    public int priority;
}

public abstract class AIState : MonoBehaviour
{
    protected AIBrain _aiBrain;

    public List<ConditionPair> _transitionList;

    protected virtual void Awake()
    {
        _aiBrain = GetComponentInParent<AIBrain>();
    }

    public virtual void OnStateEnter() { }

    public virtual void OnStateLeave() { }

    public abstract void TakeAAction();
}
