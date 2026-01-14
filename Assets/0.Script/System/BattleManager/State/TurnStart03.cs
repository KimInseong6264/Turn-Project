

public class TurnStart03 : IState
{
    private BattleManager _battleManager;
    
    public TurnStart03(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void Enter()
    {
        
        _battleManager.SetState(BattleState.ActSelect);
    }

    public void Exit() {}

    public void Update() {}
}