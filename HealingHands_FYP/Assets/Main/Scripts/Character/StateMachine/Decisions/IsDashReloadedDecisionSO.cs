using UnityEngine;

[CreateAssetMenu(fileName = "IsDashingReloaded", menuName = "Scriptable Objects /State Machine /Decisions /IsDashingReloaded")]
public class IsDashReloadedDecisionSO : StateDecisionSO<IsDashReloaded> {}

public class IsDashReloaded : Decision
{
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override bool Statement() => _protagonist.CanDash;
}
