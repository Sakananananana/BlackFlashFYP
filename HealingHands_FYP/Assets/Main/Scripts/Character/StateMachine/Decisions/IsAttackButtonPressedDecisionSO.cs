using UnityEngine;

[CreateAssetMenu(fileName = "IsAttackButtonPressed", menuName = "Scriptable Objects /State Machine /Decisions /IsAttackButtonPressed")]
public class IsAttackButtonPressedDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsAttackButtonPressed();
}

public class IsAttackButtonPressed : Decision
{
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override bool Statement() => _protagonist.AttackPerformed;
    public override void OnStateExit() { }
}
