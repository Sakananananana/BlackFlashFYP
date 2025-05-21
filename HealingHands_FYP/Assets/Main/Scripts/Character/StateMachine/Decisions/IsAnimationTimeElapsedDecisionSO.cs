using UnityEngine;

[CreateAssetMenu(fileName = "IsAnimationTimeElapsedDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /IsAnimationTimeElapsedDecisionSO")]
public class IsAnimationTimeElapsedDecisionSO : StateDecisionSO
{
    protected override Decision CreateDecision() => new IsAnimationTimeElapsedDecision();
}

public class IsAnimationTimeElapsedDecision : Decision
{
    private Animator _anim;
    private float _animationLength;
    private float _startTime;


    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _startTime = Time.time;
        _anim = stateMachine.GetComponent<Animator>();
        _animationLength = _anim.GetCurrentAnimatorStateInfo(0).length;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement() => Time.time > _startTime + _animationLength;
}