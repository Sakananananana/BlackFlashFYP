using UnityEngine;

[CreateAssetMenu(fileName = "IsDashingReloaded", menuName = "Scriptable Objects /State Machine /Decisions /IsDashingReloaded")]
public class IsDashingReloaded : StateDecision
{
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);

        _protagonist = stateMachine.GetComponent<PlayerControls>();
    }

    public override bool Statement()
    {
        return _protagonist.CanDash;
    }
}
