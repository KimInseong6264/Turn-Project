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
    }

    public void Exit() {}

    public void Update() {}
    
    // 씬에 있는 유닛들의 위치를 갱신해준다.
    private void SetPosition()
    {
        if (_players == null || _enemies == null)
        {
            Debug.LogError("배틀 할 유닛이 없습니다.");
            return;
        }

        int count = 0;
        foreach (var player in _players)
        {
            player.SetPosition(new Vector3(-5 - count, 1, 3));
            count += 2;
        }

        count = 0;
        foreach (var enemy in _enemies)
        {
            enemy.SetPosition(new Vector3(5 + count, 1, 3));
            count += 2;
        }
        
        _battleManager.SetState(BattleState.TurnSequence03);
    }
}