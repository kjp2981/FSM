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
    protected float stateDurationTime = 0f;
    public float StateDurationTime => stateDurationTime;

    public List<ConditionPair> _transitionList;

    public virtual void OnStateEnter()
    {
        stateDurationTime = 0f;
    }

    public virtual void OnStateLeave()
    {
        stateDurationTime = 0f;
    }

    public abstract void TakeAAction();
}
