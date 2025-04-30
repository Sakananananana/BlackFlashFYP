using UnityEngine;

[CreateAssetMenu(fileName = "IsBoarAttackingDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Boar /IsBoarAttackingDecisionSO")]
public class IsBoarAttackingDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsBoarAttackingDecision();
}

public class IsBoarAttackingDecision : Decision
{
    private Boar _boar;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _boar = stateMachine.GetComponent<Boar>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _boar.IsAttacking;
}

