using UnityEngine;

[CreateAssetMenu(fileName = "DashingActionSO", menuName = "Scriptable Objects /State Machine /Actions /DashingActionSO")]
public class DashingActionSO : StateAction
{
    private Vector2 _dashDir;
    private Rigidbody2D _rb2D;
    private PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _protagonist = stateMachine.GetComponent<PlayerControls>();

        _dashDir = _protagonist.LastMoveDir;
        _protagonist.DashingCooldown();
    }

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = _dashDir * _protagonist.DashForce;
    }

    public override void OnStateExit() { }

    public override void OnUpdate() { }
}
