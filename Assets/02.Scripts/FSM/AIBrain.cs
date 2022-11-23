using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    public GameObject Target => _target;

    private AIState _beforeState;
    [SerializeField]
    private AIState _currentState;

    private float stateDuractionTime = 0f;
    public float StateDuractionTime => stateDuractionTime;

    public List<ConditionPair> GlobalTransition;

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public AIState GetCurrentState()
    {
        return _currentState;
    }

    public AIState GetBeforeState()
    {
        return _beforeState;
    }

    public void ChangeState(AIState state)
    {
        if (_currentState == state)
            return;

        stateDuractionTime = 0f;
        _beforeState = _currentState;
        _beforeState.OnStateLeave();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    private void Start()
    {
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        if (_target != null)
        {
            _currentState.TakeAAction();
            stateDuractionTime += Time.deltaTime;
        }
        else
        {
            return;
        }
    }

    private void LateUpdate()
    {
        ConditionPair nextCondition = null;
        foreach (ConditionPair pair in GlobalTransition)
        {
            if (pair.conditionAndNotList.Count == 0) continue;

            bool isTransition = false;
            for (int i = 0; i < pair.conditionAndNotList.Count; i++)
            {
                if (pair.conditionAndNotList[i].condition == null) continue;
                
                if (pair.conditionAndNotList[i].condition.IfCondition(_currentState, pair.nextState) ^ pair.conditionAndNotList[i].not == true)
                {
                    isTransition = true;
                }
                else
                {
                    isTransition = false;
                    break;
                }
            }

            if (isTransition == true)
            {
                if (nextCondition == null)
                {
                    nextCondition = pair;
                }
                else
                {
                    if (pair.priority > nextCondition.priority)
                    {
                        nextCondition = pair;
                    }
                }
            }
        }

        if (nextCondition != null)
        {
            ChangeState(nextCondition.nextState);
        }
        else
        {
            foreach (ConditionPair pair in _currentState._transitionList)
            {
                if (pair.conditionAndNotList.Count == 0) continue;

                bool isTransition = false;
                for (int i = 0; i < pair.conditionAndNotList.Count; i++)
                {
                    if (pair.conditionAndNotList[i].condition == null) continue;
                
                    if (pair.conditionAndNotList[i].condition.IfCondition(_currentState, pair.nextState) ^ pair.conditionAndNotList[i].not == true)
                    {
                        isTransition = true;
                    }
                    else
                    {
                        isTransition = false;
                        break;
                    }
                }

                if (isTransition == true)
                {
                    if (nextCondition == null)
                    {
                        nextCondition = pair;
                    }
                    else
                    {
                        if (pair.priority > nextCondition.priority)
                        {
                            nextCondition = pair;
                        }
                    }
                }
            }

            if (nextCondition != null)
            {
                ChangeState(nextCondition.nextState);
            }
        }
    }
}
