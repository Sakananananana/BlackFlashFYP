using PlayerInputSystem;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DashingCooldownActionSO", menuName = "Scriptable Objects /State Machine /Decisions /DashingCooldownActionSO")]
public class DashingCooldownActionSO : StateActionSO<DashingCooldownAction> 
{
    public float Duration;
}

public class DashingCooldownAction : StateAction
{
    private new DashingCooldownActionSO _originSO => (DashingCooldownActionSO)base._originSO;
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        _protagonist = stateMachine.GetComponent<Protagonist>();
        _protagonist.CoroutineTimer(_protagonist.DashCooldownTimer(_originSO.Duration));
    }

    public override void OnFixedUpdate() { }

    public override void OnUpdate() { }

    public override void OnStateExit() { }
}