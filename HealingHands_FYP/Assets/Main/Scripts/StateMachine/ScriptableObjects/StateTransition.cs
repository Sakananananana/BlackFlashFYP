using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class StateTransition
{
    public State ToState;
    public List<ConditionUsage> Decisions = new List<ConditionUsage>();
   
    private List<List<ConditionUsage>> _resultGroups;
    private StateMachine _stateMachine;

    public void OnStateEnter(StateMachine stateMachine) 
    {
        _stateMachine = stateMachine;

        for (int i = 0; i < Decisions.Count; i++)
        {
            Decisions[i].Decision.OnStateEnter(stateMachine, Decisions[i].ExpectedResult == Result.True);
        }

        _resultGroups = Decisions.Aggregate(new List<List<ConditionUsage>>(), (acc, cu) =>
        {
            if (acc.Count == 0 || acc.Last().Last().Operator != Operator.And)
                acc.Add(new List<ConditionUsage> { cu });
            else
                acc.Last().Add(cu);
            return acc;
        });
    }

    public void OnUpdate()
    {
        //any means between 2 groups if one of the groups hit the expected result will change the state
        //the all in any means all the members in the result group MUST return its exp.Result
        if (_resultGroups.Any(group => group.All(cu => cu.Decision.IsConditionMet() == true)))
        {
            _stateMachine.ChangeState(ToState);
        }
    }

    public void OnStateExit() { }

}

[Serializable]
public class ConditionUsage
{
    public StateDecision Decision;
    public Result ExpectedResult;
    public Operator Operator;
}
public enum Result { True, False }
public enum Operator { And, Or }
