using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICondition : MonoBehaviour
{
    public abstract bool IfCondition(AIState currentState, AIState nextState);
}
