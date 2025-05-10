using UnityEngine;

[CreateAssetMenu(fileName = "TigerSingleBiteActionSO", menuName = "Scriptable Objects /State Machine /Actions /Tiger /TigerSingleBiteActionSO")]
public class TigerSingleBiteActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new TigerSingleBiteAction();
}

public class TigerSingleBiteAction : StateAction
{
    private Tiger _tiger;
    
    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _tiger = stateMachine.GetComponent<Tiger>();
        _tiger.IsSingleBiteHandler();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }
}