using UnityEngine;

public class State
{
    internal StateSO _originSO;
    internal StateMachine _stateMachine;
    internal StateTransition[] _transitions;
    internal StateAction[] _actions;

    internal State() { }

    public State(StateSO originSO, StateMachine stateMachine, StateTransition[] transitions, StateAction[] actions)
    {
        _originSO = originSO;
        _stateMachine = stateMachine;
        _transitions = transitions;
        _actions = actions;
    }

    public void OnStateEnter()
    {
        void OnStateEnter(IStateComponent[] comps)
        {
            for (int i = 0; i < comps.Length; i++)
                comps[i].OnStateEnter(_stateMachine);
        }
        OnStateEnter(_transitions);
        OnStateEnter(_actions);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < _actions.Length; i++)
            _actions[i].OnUpdate();

        for (int i = 0; i < _transitions.Length; i++)
            _transitions[i].OnUpdate();
    }

    public void OnFixedUpdate()
    {
        for (int i = 0; i < _actions.Length; i++)
            _actions[i].OnFixedUpdate();
    }

    public void OnStateExit()
    {
        void OnStateExit(IStateComponent[] comps)
        {
            for (int i = 0; i < comps.Length; i++)
                comps[i].OnStateExit();
        }
        OnStateExit(_transitions);
        OnStateExit(_actions);
    }
}
