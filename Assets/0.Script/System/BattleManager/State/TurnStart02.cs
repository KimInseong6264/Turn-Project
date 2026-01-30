using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 상태 갱신(버프, 디버프 갱신)
/// 2. 유닛의 위치 갱신
/// </summary>
public class TurnStart02 : IState
{
    private BattleManager _battleManager;
    
    public TurnStart02(BattleManager battleManager)
    {
        _battleManager = battleManager;
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
        _battleManager.SpawnMgr.SetUnitsPos(_battleManager.Players);
        _battleManager.SpawnMgr.SetUnitsPos(_battleManager.Enemies);
        
        _battleManager.SetState(BattleState.TurnSequence03);
    }
}