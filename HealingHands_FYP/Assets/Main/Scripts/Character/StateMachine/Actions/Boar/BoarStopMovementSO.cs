using UnityEngine;

[CreateAssetMenu(fileName = "BoarStopMovementSO", menuName = "Scriptable Objects /State Machine /Actions /Boar /BoarStopMovementSO")]
public class BoarStopMovementSO : StateActionSO
{
    protected override StateAction CreateAction() => new BoarStopMovement();
}

public class BoarStopMovement : StateAction
{
    private Rigidbody2D _rb2D;

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = Vector2.zero;
    }

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void OnStateExit()
    {
        _rb2D.constraints = RigidbodyConstraints2D.None;
        _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        base.OnStateExit();
    }

    public override void OnUpdate()
    {
    }

}
