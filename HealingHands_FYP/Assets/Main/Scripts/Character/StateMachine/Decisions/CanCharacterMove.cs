using UnityEngine;

[CreateAssetMenu(fileName = "CanCharacterMove", menuName = "Scriptable Objects /State Machine /Decisions /CanCharacterMove")]
public class CanCharacterMove : StateDecision
{
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);

        _protagonist = stateMachine.GetComponent<PlayerControls>();
    }

    public override bool Statement()
    {
        return _protagonist.CanMove;
    }
}
