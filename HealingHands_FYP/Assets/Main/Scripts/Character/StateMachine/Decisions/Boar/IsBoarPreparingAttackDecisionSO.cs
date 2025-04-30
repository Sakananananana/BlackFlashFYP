using UnityEngine;

[CreateAssetMenu(fileName = "IsBoarPreparingAttackDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Boar /IsBoarPreparingAttackDecisionSO")]
public class IsBoarPreparingAttackDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsBoarPreparingAttackDecision();
}

public class IsBoarPreparingAttackDecision : Decision
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

    public override bool Statement() => _boar.IsPreparingAttack;
}
