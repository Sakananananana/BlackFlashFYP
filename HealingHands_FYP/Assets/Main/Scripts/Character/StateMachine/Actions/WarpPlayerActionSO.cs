using UnityEngine;

[CreateAssetMenu(fileName = "WarpPlayerActionSO", menuName = "Scriptable Objects /State Machine /Actions /WarpPlayerActionSO")]
public class WarpPlayerActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new WarpPlayerAction();
}

public class WarpPlayerAction : StateAction
{
    private Protagonist _protagonist;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();


        _protagonist.WarpPerformed = false;
    }

    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }



    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnUpdate()
    {
    }
}
