using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class StateTransition: IStateComponent
{
    public StateSO _targetState;
    public List<ConditionUsage> Decisions = new List<ConditionUsage>();
   
    private StateMachine _stateMachine;
    private List<List<ConditionUsage>> _resultGroups;

    public void OnStateEnter(StateMachine stateMachine) 
    {
        _stateMachine = stateMachine;

        for (int i = 0; i < Decisions.Count; i++)
        {
            var decision = Decisions[i].GetDecision(_stateMachine);
            decision._decision.OnStateEnter(_stateMachine);
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
        if (_resultGroups.Any(group => group.All(cu => cu.StateDecision.IsConditionMet())))
        {
            _stateMachine.ChangeState(_targetState.GetState(_stateMachine, _stateMachine._createdInstances));
        }
    }

    public void OnStateExit() 
    {
        for (int i = 0; i < Decisions.Count; i++)
        {
            Decisions[i].StateDecision._decision.OnStateExit();
        }
    }
}

[Serializable]
public class ConditionUsage
{
    public StateDecisionSO Decision;
    public Result ExpectedResult;
    public Operator Operator;

    public StateDecision StateDecision;
    public StateDecision GetDecision(StateMachine stateMachine)
    {
        if (Decision != null)
            StateDecision = Decision.GetStateDecision(stateMachine, ExpectedResult == Result.True, stateMachine._createdInstances);

        return StateDecision;
    }
}

public enum Result { True, False }
public enum Operator { And, Or }
