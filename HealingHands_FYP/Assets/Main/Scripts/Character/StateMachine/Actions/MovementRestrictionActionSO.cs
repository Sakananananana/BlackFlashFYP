using UnityEngine;

[CreateAssetMenu(fileName = "MovementRestrictionActionSO", menuName = "Scriptable Objects /State Machine /Actions /MovementRestrictionActionSO")]
public class MovementRestrictionActionSO : StateAction
{
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<PlayerControls>();
        _protagonist.CanMove = false;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }
}
