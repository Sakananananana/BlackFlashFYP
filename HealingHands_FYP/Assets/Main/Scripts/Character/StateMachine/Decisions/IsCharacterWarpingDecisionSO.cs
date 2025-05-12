using UnityEngine;

[CreateAssetMenu(fileName = "IsCharacterWarpingDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /IsCharacterWarpingDecisionSO")]
public class IsCharacterWarpingDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsCharacterWarpingDecision();
}

public class IsCharacterWarpingDecision : Decision
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

    public override bool Statement() => _protagonist.IsWarping;
}
