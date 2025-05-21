using UnityEngine;

[CreateAssetMenu(fileName = "BoarAttackActionSO", menuName = "Scriptable Objects /State Machine /Actions /Boar /BoarAttackActionSO")]
public class BoarAttackActionSO : StateActionSO
{
    public TransformAnchor PlayerAnchor;
    public float MovementSpeed;

    protected override StateAction CreateAction() => new BoarAttackAction();
}

public class BoarAttackAction : StateAction
{
    private Rigidbody2D _rb2D;
    private Vector2 _direction;
    private TransformAnchor _protagonist;
    private float _movementSpeed;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _protagonist = ((BoarAttackActionSO)OriginSO).PlayerAnchor;
        _movementSpeed= ((BoarAttackActionSO)OriginSO).MovementSpeed;
        _direction = (_protagonist.Value.position - stateMachine.transform.position).normalized;
    }

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = _direction * _movementSpeed;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate()
    {
    }
}
