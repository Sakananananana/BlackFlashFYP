using UnityEngine;

[CreateAssetMenu(fileName = "IsCharacterMovementRestricted", menuName = "Scriptable Objects /State Machine /Decisions /IsCharacterMovementRestricted")]
public class IsCharacterMovementRestrictedDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsCharacterMovementRestricted();
}

public class IsCharacterMovementRestricted : Decision
{
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override bool Statement() => _protagonist.CanMove;
}
