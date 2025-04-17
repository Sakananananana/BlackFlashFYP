using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "BoarPreparingAttackActionSO", menuName = "Scriptable Objects /State Machine /Actions /BoarPreparingAttackActionSO")]
public class BoarPreparingAttackActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new BoarPreparingAttackAction();
}

public class BoarPreparingAttackAction : StateAction
{
    Boar _boar;

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate()
    {
        _boar.SetAnimationFloat();
    }
}
