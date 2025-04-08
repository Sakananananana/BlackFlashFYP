using UnityEngine;

[CreateAssetMenu(fileName = "IsGettingHit", menuName = "Scriptable Objects /State Machine /Decisions /IsGettingHit")]
public class IsGettingHit : StateDecision
{
    private Damageable _damageable;
    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);

        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override bool Statement() => _damageable.GetHit;

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    
}
