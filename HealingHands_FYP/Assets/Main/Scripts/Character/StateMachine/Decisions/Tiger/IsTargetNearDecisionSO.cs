using UnityEngine;

[CreateAssetMenu(fileName = "IsTargetNearDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Tiger /IsTargetNearDecisionSO")]
public class IsTargetNearDecisionSO : StateDecisionSO
{
    public float DistanceThresold;
    public TransformAnchor PlayerAnchor;
    
    protected override Decision CreateDecision() => new IsTargetNearDecision();
}

public class IsTargetNearDecision : Decision
{
    private TransformAnchor _protagonist;
    private StateMachine _stateMachine;
    private float _distanceThresold;
    private float _distance;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _stateMachine = stateMachine;
        _distanceThresold = ((IsTargetNearDecisionSO)OriginSO).DistanceThresold;   
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement()
    {
        _protagonist = ((IsTargetNearDecisionSO)OriginSO).PlayerAnchor;
        _distance = (_protagonist.Value.position - _stateMachine.transform.position).magnitude;
        return _distance < _distanceThresold;
    }
}
