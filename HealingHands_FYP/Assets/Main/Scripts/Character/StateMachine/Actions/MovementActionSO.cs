using UnityEngine;

[CreateAssetMenu(fileName = "MovementActionSO", menuName = "Scriptable Objects /State Machine /Actions /MovementActionSO")]
public class MovementActionSO : StateAction
{
    Rigidbody2D _rb2D;
    PlayerControls _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _protagonist= stateMachine.GetComponent<PlayerControls>();
    }

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = _protagonist.MoveDir * _protagonist.MoveSpeed;
        _protagonist.MovementVector = _rb2D.linearVelocity;
    }
 
    public override void OnUpdate() 
    {
        
    }
    public override void OnStateExit() { }
}
