using UnityEngine;

[CreateAssetMenu(fileName = "IsMovementInputValidDecisionSO", menuName = "Scriptable Objects/IsMovementInputValidDecisionSO")]
public class IsMovementInputValidDecisionSO : StateDecisionSO<IsMovementInputValid>
{
    
}

public class IsMovementInputValid : Decision
{
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        _protagonist = stateMachine.GetComponent<Protagonist>();
    }

    public override bool Statement()
    {
        return _protagonist.MoveDir.sqrMagnitude > 0.1f;
    }
}
