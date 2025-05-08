using UnityEngine;

[CreateAssetMenu(fileName = "IsTigerHealthLowDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Tiger /IsTigerHealthLowDecisionSO")]
public class IsTigerHealthLowDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsTigerHealthLowDecision();
}

public class IsTigerHealthLowDecision : Decision
{
    private Damageable _damageable;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement()
    {
        return _damageable._currentHealthSO.CurrentHealth <= (_damageable._currentHealthSO.MaxHealth / 2);
    }
 
}
