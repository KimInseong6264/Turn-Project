using System.Collections.Generic;
using UnityEngine;

public class TurnEnd06 : IState
{
    private BattleManager _battleManager;
    private List<UnitPresenter> _players;
    private List<UnitPresenter> _enemies;
    
    public TurnEnd06(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _players = battleManager.Players;
        _enemies = battleManager.Enemies;
    }

    public void Enter()
    {
        _battleManager.Init();

        UpdateState();
    }

    public void Exit() {}

    public void Update() {}


    private void UpdateState()
    {
        if (_players.Count == 0 || _enemies.Count == 0)
            _battleManager.SetState(BattleState.BattleEnd07);
        
        _battleManager.SetState(BattleState.TurnStart02);
    }
}
