using UnityEngine;

[CreateAssetMenu(fileName = "GettingHitActionSO", menuName = "Scriptable Objects /State Machine /Actions /GettingHitActionSO")]
public class GettingHitActionSO : StateActionSO
{
    public BoolEventChannelSO ShowDamageScreenOnDamageReceive;
    protected override StateAction CreateAction() => new GettingHitAction();
}

public class GettingHitAction : StateAction
{
    private Rigidbody2D _rb2D;
    private Damageable _damageable;
    private BoolEventChannelSO _showDamageScreenOnDamageReceive;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _rb2D = stateMachine.GetComponent<Rigidbody2D>();
        _damageable = stateMachine.GetComponent<Damageable>();
        _showDamageScreenOnDamageReceive = ((GettingHitActionSO)OriginSO).ShowDamageScreenOnDamageReceive;
        _showDamageScreenOnDamageReceive.RaiseEvent(true);
    }

    public override void OnUpdate() {  }

    public override void OnFixedUpdate()
    {
        _rb2D.linearVelocity = _damageable.HitDirection * (5f - Time.deltaTime);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _showDamageScreenOnDamageReceive.RaiseEvent(false);
    }

 
}
