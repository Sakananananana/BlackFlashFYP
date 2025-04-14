using UnityEngine;

[CreateAssetMenu(fileName = "MovementActionSO", menuName = "Scriptable Objects /State Machine /Actions /MovementActionSO")]
public class MovementActionSO: StateActionSO
{
    protected override StateAction CreateAction() => new MovementAction();
}
public class MovementAction : StateAction
{
    Rigidbody2D _rb2D;
    Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _protagonist= stateMachine.GetComponent<Protagonist>();
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
