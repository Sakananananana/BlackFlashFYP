using System;
using UnityEngine;

public abstract class Decision: IStateComponent
{
    internal StateDecisionSO _originSO;
    protected StateDecisionSO OriginSO => _originSO;

    public virtual void OnStateEnter(StateMachine stateMachine) { }

    public virtual void OnStateExit() { }
    public abstract bool Statement();
}

public struct StateDecision
{
    internal readonly StateMachine _stateMachine;
    internal readonly Decision _decision;
    internal readonly bool _expectedResult;

    public StateDecision(StateMachine stateMachine, Decision decision, bool expectedResult)
    { 
        _stateMachine = stateMachine;
        _decision = decision;    
        _expectedResult = expectedResult;    
    }

    public bool IsConditionMet()
    {
        bool statement = _decision.Statement();
        bool isMet = statement == _expectedResult;
     
        return isMet;
    }

}
