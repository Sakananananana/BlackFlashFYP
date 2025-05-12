using UnityEngine;

[CreateAssetMenu(fileName = "IsCharacterWarpPerfomedDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /IsCharacterWarpPerfomedDecisionSO")]
public class IsCharacterWarpPerfomedDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsCharacterWarpPerformedDecision();
}

public class IsCharacterWarpPerformedDecision : Decision
{
    private Protagonist _protagonist;
    
    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _protagonist.WarpPerformed;
}
