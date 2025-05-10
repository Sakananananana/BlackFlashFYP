using UnityEngine;

[CreateAssetMenu(fileName = "TigerLungeActionSO", menuName = "Scriptable Objects /State Machine /Actions /Tiger /TigerLungeActionSO")]
public class TigerLungeActionSO : StateActionSO
{
    public TransformAnchor TransformAnchor;

    protected override StateAction CreateAction() => new TigerLungeAction();
}

public class TigerLungeAction : StateAction
{
    private TransformAnchor _protagonist;
    private Vector2 _direction;
    private Rigidbody2D _rb2D;
    private Tiger _tiger;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = ((TigerLungeActionSO)OriginSO).TransformAnchor;
        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _tiger = stateMachine.GetComponent<Tiger>();

        _tiger.LungingHandler();
        _direction = (_protagonist.Value.position - stateMachine.transform.position).normalized;
    }

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = _direction * 12f;
    }

    public override void OnUpdate() { }

    public override void OnStateExit() { base.OnStateExit(); }
}