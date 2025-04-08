using UnityEngine;

public class BaseState : ScriptableObject
{
    public virtual void OnStateEnter(StateMachine stateMachine) { }

    public virtual void OnUpdate () { }

    public virtual void OnFixedUpdate() { }

    public virtual void OnStateExit() { }
}
