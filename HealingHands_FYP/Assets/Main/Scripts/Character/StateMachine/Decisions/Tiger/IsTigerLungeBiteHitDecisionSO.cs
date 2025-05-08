using UnityEngine;

[CreateAssetMenu(fileName = "IsTigerLungeBiteHitDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Tiger /IsTigerLungeBiteHitDecisionSO")]
public class IsTigerLungeBiteHitDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsTigerLungeBiteHitDecision();
}

public class IsTigerLungeBiteHitDecision : Decision
{
    Tiger _tiger;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _tiger = stateMachine.GetComponent<Tiger>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _tiger.IsLungeBiteHit;
}
