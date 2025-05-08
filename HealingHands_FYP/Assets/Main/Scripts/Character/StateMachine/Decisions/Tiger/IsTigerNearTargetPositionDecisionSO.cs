using UnityEngine;

[CreateAssetMenu(fileName = "IsTigerNearTargetPositionDecisionSO", menuName = "Scriptable Objects /State Machine /Decisions /Tiger /IsTigerNearTargetPositionDecisionSO")]
public class IsTigerNearTargetPositionDecisionSO : StateDecisionSO
{
    public TransformAnchor TransformAnchor;
    protected override Decision CreateDecision() => new IsTigerNearTargetPositionDecision();
}

public class IsTigerNearTargetPositionDecision : Decision
{
    private TransformAnchor _protagonist;
    private StateMachine _stateMachine;
    private Vector3 _targetPosition;
    private float _distance;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = ((IsTigerNearTargetPositionDecisionSO)OriginSO).TransformAnchor;
        _targetPosition = _protagonist.Value.position;
        _stateMachine = stateMachine;
        
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override bool Statement()
    {
        _distance = (_targetPosition - _stateMachine.transform.position).magnitude;
        return _distance < 5;
    }
}
