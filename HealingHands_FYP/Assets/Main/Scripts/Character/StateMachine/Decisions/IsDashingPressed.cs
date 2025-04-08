using UnityEngine;

[CreateAssetMenu(fileName = "IsDashingPressed", menuName = "Scriptable Objects /State Machine /Decisions /IsDashingPressed")]
public class IsDashingPressed : StateDecision
{
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);

        _protagonist = stateMachine.GetComponent<PlayerControls>();
    }

    public override bool Statement()
    {
        return _protagonist.DashPerformed;
    }
}
