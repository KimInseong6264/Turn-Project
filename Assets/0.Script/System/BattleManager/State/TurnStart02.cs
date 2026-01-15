using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 상태 갱신(버프, 디버프 갱신)
/// 2. 유닛의 위치 갱신
/// </summary>
public class TurnStart02 : IState
{
    private BattleManager _battleManager;
    private List<UnitPresenter> _players;
    private List<UnitPresenter> _enemies;
    
    public TurnStart02(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _players = battleManager.Players;
        _enemies = battleManager.Enemies;
    }

    public void Enter()
    {
        SetPosition();
        
        _battleManager.SetState(BattleState.TurnSequence02);
    }

    public void Exit() {}

    public void Update() {}
    
    
    private void SetPosition()
    {
        foreach (var player in _players)
        {
            player.SetPosition(new Vector3(-3, 0, 0));
        }
        
        foreach (var enemy in _enemies)
        {
            enemy.SetPosition(new Vector3(3, 0, 0));
        }
    }
}