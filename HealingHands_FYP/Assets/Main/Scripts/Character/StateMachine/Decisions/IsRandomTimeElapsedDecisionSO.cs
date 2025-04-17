using UnityEngine;

[CreateAssetMenu(fileName = "IsRandomTimeElapsed", menuName = "Scriptable Objects /State Machine /Decisions /IsRandomTimeElapsed")]
public class IsRandomTimeElapsedDecisionSO : StateDecisionSO<IsRandomTimeElapsed>
{
    public float _minDuration;
    public float _maxDuration;

    protected override Decision CreateDecision() => new IsRandomTimeElapsed();

}

public class IsRandomTimeElapsed : Decision
{
    private new IsRandomTimeElapsedDecisionSO _originSO => (IsRandomTimeElapsedDecisionSO)base._originSO;
    private float _startTime;
    private float _duration;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        _startTime = Time.time;
        _duration = Random.Range(_originSO._minDuration, _originSO._maxDuration);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => Time.time >= _startTime + _duration;
}