using System.Collections.Generic;
using UnityEngine;

public abstract class StateDecisionSO: ScriptableObject
{
    internal StateDecision GetStateDecision(StateMachine stateMachine, bool expectedResult, Dictionary<ScriptableObject, object> createdInstances)
    {
        if (!createdInstances.TryGetValue(this, out var obj))
        {
            var decision = CreateDecision();
            decision._originSO = this;
            createdInstances.Add(this, decision);
            decision.OnStateEnter(stateMachine);

            obj = decision;
        }

        return new StateDecision(stateMachine, (Decision)obj, expectedResult);
    }

    protected abstract Decision CreateDecision();
}

public abstract class StateDecisionSO<T> : StateDecisionSO where T : Decision, new()
{
    protected override Decision CreateDecision() => new T();
}
