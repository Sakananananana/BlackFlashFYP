using UnityEngine;

[CreateAssetMenu(fileName = "StopMovementAction", menuName = "Scriptable Objects /State Machine /Actions /StopMovementAction")]
public class StopMovementActionSO : StateAction
{
    private PlayerControls _protagonist;
    private Animator _animator;
    private Rigidbody2D _rb2D;


    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<PlayerControls>();
        _animator = stateMachine.GetComponent<Animator>();
        _rb2D = stateMachine.GetComponent<Rigidbody2D>();  
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = Vector2.zero;
        _protagonist.MovementVector = Vector2.zero;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

  
}
