using UnityEngine;

[CreateAssetMenu(fileName = "IsBoarCollidedWithTargetDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Boar /IsBoarCollidedWithTargetDecisionSO")]
public class IsBoarCollidedWithTargetDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsBoarCollidedWithTargetDecision();
}

public class IsBoarCollidedWithTargetDecision : Decision
{
    private Boar _boar;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _boar= stateMachine.GetComponent<Boar>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _boar.IsAttackHit;
}
