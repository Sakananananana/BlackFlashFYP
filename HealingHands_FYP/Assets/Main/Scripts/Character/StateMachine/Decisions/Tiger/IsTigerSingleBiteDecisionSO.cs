using UnityEngine;

[CreateAssetMenu(fileName = "IsTigerSingleBiteDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Tiger /IsTigerSingleBiteDecisionSO")]
public class IsTigerSingleBiteDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsTigerSingleBiteDecision();
}

public class IsTigerSingleBiteDecision : Decision
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
