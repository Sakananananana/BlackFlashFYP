using UnityEngine;

[CreateAssetMenu(fileName = "IsToadAttacking", menuName = "Scriptable Objects /State Machine /Decisions /IsToadAttacking")]
public class IsToadAttackingDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsToadAttacking();
}

public class IsToadAttacking : Decision
{
    private Toad _toad;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _toad = stateMachine.GetComponent<Toad>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => _toad.IsAttacking;
}
