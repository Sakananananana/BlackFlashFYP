using UnityEngine;

[CreateAssetMenu(fileName = "IsBoarDizzyDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Boar /IsBoarDizzyDecisionSO")]
public class IsBoarDizzyDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsBoarDizzyDecision();
}

public class IsBoarDizzyDecision : Decision
{
    Boar _boar;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _boar = stateMachine.GetComponent<Boar>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _boar.IsDizzy;
}
