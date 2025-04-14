using UnityEngine;

[CreateAssetMenu(fileName = "IsTimeElapsed", menuName = "Scriptable Objects /State Machine /Decisions /IsTimeElapsed")]
public class IsTimeElapsedDecisionSO : StateDecisionSO<IsTimeElapsed>
{
    public float _duration;
    protected override Decision CreateDecision() => new IsTimeElapsed();
    
}

public class IsTimeElapsed : Decision
{
    private new IsTimeElapsedDecisionSO _originSO => (IsTimeElapsedDecisionSO)base._originSO;
    private float _startTime;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        _startTime = Time.time;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => Time.time >= _startTime + _originSO._duration;
}
