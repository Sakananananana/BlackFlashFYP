using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGettingHitActionSO", menuName = "Scriptable Objects /State Machine /Actions /EnemyGettingHitActionSO")]
public class EnemyGettingHitActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new EnemyGettingHitAction();
}

public class EnemyGettingHitAction : StateAction
{
    private Damageable _damageable;

    public override void OnFixedUpdate() { }
    public override void OnUpdate() { }

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        _damageable = stateMachine.GetComponent<Damageable>();
        _damageable._spriteRenderer.color = Color.red;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _damageable._spriteRenderer.color = Color.white;
    }
}
