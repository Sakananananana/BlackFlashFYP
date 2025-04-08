using UnityEngine;

[CreateAssetMenu(fileName = "IsAttackingPressed", menuName = "Scriptable Objects /State Machine /Decisions /IsAttackingPressed")]
public class IsAttackingPressed : StateDecision
{
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);

        _protagonist = stateMachine.GetComponent<PlayerControls>();
    }

    public override bool Statement() => _protagonist.AttackPerformed;
    public override void OnStateExit() { }
}
