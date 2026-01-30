using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 1. 전투 씬 전환
/// 2. 유닛 스폰
/// </summary>
public class BattleStart01 : IState
{
    private BattleManager _battleManager;
    
    public BattleStart01(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void Enter()
    {
        StartSpawn(UnitSelector.Players, UnitSelector.Enemies);
        
        _battleManager.SetState(BattleState.TurnStart02);
    }

    public void Exit()
    {
        GameManager.Instance.UpdateUI(UIGroupName.UnitSelectUI, false);
    }

    public void Update() {}
    

    // 생성된 유닛을 배틀매니저 리스트에 담는다.
    private void StartSpawn(List<UnitDataSO> playerDataList, List<UnitDataSO> enemyDataList)
    {
        var spawner = _battleManager.SpawnMgr;
        spawner.OnSpawn(playerDataList, enemyDataList);
    }
}