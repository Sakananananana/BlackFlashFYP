using UnityEngine;

[CreateAssetMenu(fileName = "RespawnPlayerActionSO", menuName = "Scriptable Objects /State Machine /Actions /RespawnPlayerActionSO")]
public class RespawnPlayerActionSO : StateActionSO
{
    public LoadEventChannelSO RespawnAtHome;
    public GameSceneSO Home;
    public DungeonSO DungeonSO;
    protected override StateAction CreateAction() => new RespawnPlayerAction();
}

public class RespawnPlayerAction : StateAction
{
    private LoadEventChannelSO _respawnAtHome;
    private GameSceneSO _home;
    private DungeonSO _dungeonSO;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _respawnAtHome = ((RespawnPlayerActionSO)OriginSO).RespawnAtHome;
        _home = ((RespawnPlayerActionSO)OriginSO).Home;
        _dungeonSO = ((RespawnPlayerActionSO)OriginSO).DungeonSO;

        _respawnAtHome.RaiseEvent(_home);
        _dungeonSO.ResetDungeonProgress();
    }


    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnFixedUpdate() { }

    public override void OnUpdate() { }
}
