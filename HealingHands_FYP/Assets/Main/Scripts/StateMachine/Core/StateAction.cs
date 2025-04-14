using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction: IStateComponent
{
    internal StateActionSO _originSO;
    protected StateActionSO OriginSO => _originSO;

    public virtual void OnStateEnter(StateMachine stateMachine) { }
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public virtual void OnStateExit() { }

}


