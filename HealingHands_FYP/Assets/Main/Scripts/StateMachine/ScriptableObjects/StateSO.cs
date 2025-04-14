using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects /State Machine /State")]
public class StateSO : ScriptableObject
{
    [SerializeField] private StateActionSO[] _actions = null;
    [SerializeField] private StateTransition[] _transitions = null;

    /// <summary>
    /// Will create a new state or return an existing one inside <paramref name="createdInstances"/>.
    /// </summary>
    internal State GetState(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
    {
        if (createdInstances.TryGetValue(this, out var obj))
            return (State)obj;

        var state = new State();
        createdInstances.Add(this, state);

        state._originSO = this;
        state._stateMachine = stateMachine;
        state._transitions = GetTransitions(_transitions);
        state._actions = GetActions(_actions, stateMachine, createdInstances);

        return state;
    }

    private StateAction[] GetActions(StateActionSO[] scriptableActions,
        StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
    {
        int count = scriptableActions.Length;
        var actions = new StateAction[count];
        for (int i = 0; i < count; i++)
            actions[i] = scriptableActions[i].GetAction(stateMachine, createdInstances);

        return actions;
    }

    private StateTransition[] GetTransitions(StateTransition[] transitionsList) 
    {
        StateTransition[] newTransitions = new StateTransition[transitionsList.Length];

        for (int i = 0; i < transitionsList.Length; i++)
        {
            // Create a new instance of the transition
            newTransitions[i] = new StateTransition();

            // Copy all data from the original transition
            newTransitions[i]._targetState = transitionsList[i]._targetState;
            newTransitions[i].Decisions = new List<ConditionUsage>();

            // Deep copy each ConditionUsage
            foreach (var condition in transitionsList[i].Decisions)
            {
                newTransitions[i].Decisions.Add(new ConditionUsage
                {
                    Decision = condition.Decision,
                    ExpectedResult = condition.ExpectedResult,
                    Operator = condition.Operator
                });
            }
        }

        return newTransitions;
    }
}

