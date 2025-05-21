using UnityEngine;

[CreateAssetMenu(fileName = "CrowAttackActionSO", menuName = "Scriptable Objects /State Machine /Actions /Crow /CrowAttackActionSO")]
public class CrowAttackActionSO : StateActionSO
{
    public TransformAnchor PlayerAnchor;
    public float MovementSpeed;

    protected override StateAction CreateAction() => new CrowAttackAction();
}

public class CrowAttackAction : StateAction
{
    private Rigidbody2D _rb2D;
    private Vector2 _direction;
    private float _movementSpeed;
    private StateMachine _stateMachine;
    private TransformAnchor _protagonist;
    

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _protagonist = ((CrowAttackActionSO)OriginSO).PlayerAnchor;
        _movementSpeed = ((CrowAttackActionSO)OriginSO).MovementSpeed;
        _stateMachine = stateMachine;
    }

    public override void OnUpdate() { } 

    public override void OnFixedUpdate()
    {
        _direction = (_protagonist.Value.position - _stateMachine.transform.position).normalized;
        _rb2D.linearVelocity = _direction * _movementSpeed;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
