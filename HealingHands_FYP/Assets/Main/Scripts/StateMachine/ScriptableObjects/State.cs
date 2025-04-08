using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "State", menuName = "Scriptable Objects /State Machine /State")]
public class State : BaseState
{
    public List<StateAction> Actions = new List<StateAction>();
    public List<StateTransition> Transitions = new List<StateTransition>();

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        for (int i = 0; i < Transitions.Count; i++)
        {
            Transitions[i].OnStateEnter(stateMachine);
        }

        for (int i = 0; i < Actions.Count; i++)
        {
            Actions[i].OnStateEnter(stateMachine);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        for (int i = 0; i < Transitions.Count; i++)
        {
            Transitions[i].OnUpdate();
        }

        for (int i = 0; i < Actions.Count; i++)
        {
            Actions[i].OnUpdate();
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        for (int i = 0; i < Actions.Count; i++)
        {
            Actions[i].OnFixedUpdate();
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        for (int i = 0; i < Transitions.Count; i++)
        {
            Transitions[i].OnStateExit();
        }

        for (int i = 0; i < Actions.Count; i++)
        {
            Actions[i].OnStateExit();
        }
    }
}

