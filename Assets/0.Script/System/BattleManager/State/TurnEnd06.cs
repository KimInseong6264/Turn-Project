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

    // 어느 한 쪽이 모두 죽으면 BattleEnd로 상태 전환
    private void UpdateState()
    {
        bool IsPlayerLife = IsUnitLife(_players);
        bool IsEnemieLife = IsUnitLife(_enemies);

        if (!IsPlayerLife || !IsEnemieLife)
        {
            if(IsEnemieLife)
                _battleManager.SetWinners(UnitTeam.Enemy);
        
            if(IsPlayerLife)
                _battleManager.SetWinners(UnitTeam.Player);
            
            _battleManager.SetState(BattleState.BattleEnd07);
            return;
        }
        
        _battleManager.SetState(BattleState.TurnStart02);
    }

    private bool IsUnitLife(List<UnitPresenter> units)
    {
        foreach (var unit in units)
        {
            if(!unit.IsDead)
                return true;
        }
        
        return false;
    }
}
