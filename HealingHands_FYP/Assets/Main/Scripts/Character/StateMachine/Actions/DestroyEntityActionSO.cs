using UnityEngine;

[CreateAssetMenu(fileName = "DestroyEntity", menuName = "Scriptable Objects /State Machine /Actions /DestroyEntity")]
public class DestroyEntityActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new DestroyEntityAction();
}

public class DestroyEntityAction : StateAction
{
    private GameObject _gameObject;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _gameObject = stateMachine.gameObject;
        GameObject.Destroy(_gameObject);
    }
    

    public override void OnStateExit() { }
    public override void OnUpdate() { }
    public override void OnFixedUpdate() { }
}
