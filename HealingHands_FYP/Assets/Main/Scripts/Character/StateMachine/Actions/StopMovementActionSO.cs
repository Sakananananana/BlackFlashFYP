using UnityEngine;

[CreateAssetMenu(fileName = "StopMovementAction", menuName = "Scriptable Objects /State Machine /Actions /StopMovementAction")]
public class StopMovementActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new StopMovementAction();
}

public class StopMovementAction : StateAction
{
    private Protagonist _protagonist;
    private Rigidbody2D _rb2D;


    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();
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
