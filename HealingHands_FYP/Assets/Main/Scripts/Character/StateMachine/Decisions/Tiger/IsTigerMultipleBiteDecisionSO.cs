using UnityEngine;

[CreateAssetMenu(fileName = "IsTigerMultipleBiteDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Tiger /IsTigerMultipleBiteDecisionSO")]
public class IsTigerMultipleBiteDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsTigerMultipleBiteDecision();
}

public class IsTigerMultipleBiteDecision : Decision
{
    private Tiger _tiger;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _tiger = stateMachine.GetComponent<Tiger>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _tiger.IsSingleBite;
}
