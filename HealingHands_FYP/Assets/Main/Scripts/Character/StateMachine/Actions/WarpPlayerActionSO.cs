using UnityEngine;

[CreateAssetMenu(fileName = "WarpPlayerActionSO", menuName = "Scriptable Objects /State Machine /Actions /WarpPlayerActionSO")]
public class WarpPlayerActionSO : StateActionSO
{
    public VoidEventChannelSO OnWarpPerformed;
    public LoadEventChannelSO WarpToHome;
    public GameSceneSO Home;
    public DungeonSO DungeonSO;

    protected override StateAction CreateAction() => new WarpPlayerAction();
}

public class WarpPlayerAction : StateAction
{
    private Protagonist _protagonist;
    private LoadEventChannelSO _warpToHome;
    private GameSceneSO _home;
    private DungeonSO _dungeonSO;
    private VoidEventChannelSO _onWarpPerformed;

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _protagonist = stateMachine.GetComponent<Protagonist>();

        _onWarpPerformed = ((WarpPlayerActionSO)OriginSO).OnWarpPerformed;
        _warpToHome = ((WarpPlayerActionSO)OriginSO).WarpToHome;
        _dungeonSO = ((WarpPlayerActionSO)OriginSO).DungeonSO;
        _home = ((WarpPlayerActionSO)OriginSO).Home;

        _protagonist.WarpPerformed = false;      
    }

    public override void OnFixedUpdate() { }


    public override void OnUpdate() { }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _dungeonSO.ResetDungeonProgress();
        _onWarpPerformed.RaiseEvent();
        _warpToHome.RaiseEvent(_home);
    }
}
