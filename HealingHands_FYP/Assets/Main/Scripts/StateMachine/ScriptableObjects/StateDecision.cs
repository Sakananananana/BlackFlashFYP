using UnityEngine;

public abstract class StateDecision : ScriptableObject
{
    private bool _expectedResult;

    public virtual void OnStateEnter(StateMachine stateMachine, bool expectedResult) 
    {
        _expectedResult = expectedResult;
    }

    public virtual void OnStateExit() { }
    public abstract bool Statement();

    public bool IsConditionMet()
    { 
        if (Statement() == _expectedResult)
            return true;
        else
            return false;
    }
}
