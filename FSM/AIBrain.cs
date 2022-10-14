using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    //private List<AIState> _stateList;

    [SerializeField]
    private GameObject _target;
    public GameObject Target => _target;

    private AIState _beforeState;
    [SerializeField]
    private AIState _currentState;

    public List<ConditionPair> GlobalTransition;

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public AIState GetCurrentState()
    {
        return _currentState;
    }

    public void ChangeState(AIState state)
    {
        _beforeState = _currentState;
        _beforeState.OnStateLeave();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    private void Awake()
    {
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        if (_target != null)
        {
            _currentState.TakeAAction();
        }
        else
        {
            return;
        }
    }

    private void LateUpdate()
    {
        ConditionPair nextCondition = null;
        foreach(ConditionPair pair in GlobalTransition)
        {
            bool isTransition = true;
            for (int i = 0; i < pair.conditionList.Count; i++)
            {
                if (pair.conditionList[i].IfCondition(_currentState, pair.nextState))
                {
                    isTransition = true;
                }
                else isTransition = false;
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
                bool isTransition = true;
                for (int i = 0; i < pair.conditionList.Count; i++)
                {
                    if (pair.conditionList[i].IfCondition(_currentState, pair.nextState))
                    {
                        isTransition = true;
                    }
                    else isTransition = false;
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
