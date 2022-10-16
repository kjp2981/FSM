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
            bool isTransition = false;
            for (int i = 0; i < pair.conditionList.Count; i++)
            {
                if (pair.conditionList[i].IfCondition(_currentState, pair.nextState) == true)
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
                bool isTransition = false;
                for (int i = 0; i < pair.conditionList.Count; i++)
                {
                    if (pair.conditionList[i].IfCondition(_currentState, pair.nextState) == true)
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