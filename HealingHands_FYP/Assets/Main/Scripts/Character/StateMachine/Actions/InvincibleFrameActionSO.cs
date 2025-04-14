using UnityEngine;

[CreateAssetMenu(fileName = "InvincibleFrameActionSO", menuName = "Scriptable Objects /State Machine /Actions /InvincibleFrameActionSO")]
public class InvincibleFrameActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new InvincibleFrameAction();
}
public class InvincibleFrameAction : StateAction
{
    private CircleCollider2D _collider;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _collider = stateMachine.GetComponent<CircleCollider2D>();
        _collider.excludeLayers = LayerMask.GetMask("Enemy");
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _collider.excludeLayers = 0;
    }

    public override void OnFixedUpdate() { }

    public override void OnUpdate() { }
}
