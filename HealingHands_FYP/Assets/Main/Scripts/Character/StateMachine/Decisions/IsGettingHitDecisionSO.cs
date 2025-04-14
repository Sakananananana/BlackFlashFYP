using UnityEngine;

[CreateAssetMenu(fileName = "IsGettingHit", menuName = "Scriptable Objects /State Machine /Decisions /IsGettingHit")]
public class IsGettingHitDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsGettingHit();
}

public class IsGettingHit : Decision
{
    private Damageable _damageable;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override bool Statement()
    { 
        bool result = false;

        if (_damageable != null) 
        {
            result = _damageable.GetHit;
        }

        return result;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
