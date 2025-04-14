using UnityEngine;

[CreateAssetMenu(fileName = "IsEntityDead", menuName = "Scriptable Objects /State Machine /Decisions /IsEntityDead")]
public class IsEntityDeadDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsEntityDead();
}

public class IsEntityDead : Decision
{
    private Damageable _damageable;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _damageable.IsDead;
}
