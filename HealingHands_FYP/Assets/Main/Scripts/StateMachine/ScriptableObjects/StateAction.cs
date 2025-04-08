using UnityEngine;
using UnityEngine.AI;

public abstract class StateAction : ScriptableObject
{
    public virtual void OnStateEnter(StateMachine stateMachine) { }
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public virtual void OnStateExit() { }
}

