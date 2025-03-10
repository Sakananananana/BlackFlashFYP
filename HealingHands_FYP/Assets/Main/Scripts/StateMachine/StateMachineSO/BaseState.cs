using UnityEngine;

public class BaseState : ScriptableObject
{
    public virtual void StateEnter() { }

    public virtual void StateUpdate () { }

    public virtual void StateFixedUpdate() { }

    public virtual void StateExit() { }
}
