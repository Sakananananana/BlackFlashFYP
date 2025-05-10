using UnityEngine;

[CreateAssetMenu(fileName = "TigerMultipleBiteActionSO", menuName = "Scriptable Objects /State Machine /Actions /Tiger /TigerMultipleBiteActionSO")]
public class TigerMultipleBiteActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new TigerMultipleBiteAction();
}

public class TigerMultipleBiteAction : StateAction
{
    private Tiger _tiger;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _tiger = stateMachine.GetComponent<Tiger>();
        _tiger.IsMultipleBiteHandler();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate() { }

    public override void OnFixedUpdate() { }
}
