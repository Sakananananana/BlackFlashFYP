using UnityEngine;

[CreateAssetMenu(fileName = "ResetMovementRestrictionActionSO", menuName = "Scriptable Objects /State Machine /Actions /ResetMovementRestrictionActionSO")]
public class ResetMovementRestrictionActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ResetMovementRestrictionAction();
}

public class ResetMovementRestrictionAction : StateAction
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

        _protagonist.CanMove = true;
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }
}
