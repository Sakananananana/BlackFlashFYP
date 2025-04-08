using UnityEngine;

[CreateAssetMenu(fileName = "GettingHit_Timer", menuName = "Scriptable Objects /State Machine /Decisions /GettingHit_Timer")]
public class GettingHit_Timer : StateDecision
{
    private float _startTime;
    public float _duration;

    public override void OnStateEnter(StateMachine stateMachine, bool expectedResult)
    {
        base.OnStateEnter(stateMachine, expectedResult);

        _startTime = Time.time;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => Time.time >= _startTime + _duration;

}
