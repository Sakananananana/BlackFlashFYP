using UnityEngine;

[CreateAssetMenu(fileName = "ResetGettingHitActionSO", menuName = "Scriptable Objects /State Machine /Actions /ResetGettingHitActionSO")]
public class ResetGettingHitActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ResetGettingHitAction();
}

public class ResetGettingHitAction : StateAction
{
    Damageable _damageable;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _damageable.GetHit = false;
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }
}
