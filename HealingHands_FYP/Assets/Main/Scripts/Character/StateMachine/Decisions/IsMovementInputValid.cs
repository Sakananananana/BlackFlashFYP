using UnityEngine;

[CreateAssetMenu(fileName = "IsMovementInputValid", menuName = "Scriptable Objects /State Machine /Decisions /IsMovementInputValid")]
public class IsMovementInputValid : StateDecision
{
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);
        _protagonist = stateMachine.GetComponent<PlayerControls>();
    }

    public override bool Statement()
    {
        Vector2 movementInput = _protagonist.MoveDir;
        return movementInput.sqrMagnitude > 0.1f;
    }
}
