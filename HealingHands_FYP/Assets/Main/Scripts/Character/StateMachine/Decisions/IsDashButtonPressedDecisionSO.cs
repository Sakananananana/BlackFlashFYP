using UnityEngine;

[CreateAssetMenu(fileName = "IsDashButtonPressed", menuName = "Scriptable Objects /State Machine /Decisions /IsDashButtonPressed")]
public class IsDashButtonPressedDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsDashButtonPressed();
}

public class IsDashButtonPressed : Decision
{
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override bool Statement() => _protagonist.DashPerformed;
}
