using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 1. 전투 씬 전환
/// 2. 유닛 스폰
/// </summary>
public class BattleStart01 : IState
{
    private BattleManager _battleManager;
    private Transform[] _spawnPoints;
    
    public BattleStart01(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _spawnPoints = battleManager.SpawnPoints;
    }

    public void Enter()
    {
        StartSpawn(UnitSelector.Players, SpawnPoint.PlayersSpawn);
        StartSpawn(UnitSelector.Enemies, SpawnPoint.EnemiesSpawn);
        
        _battleManager.SetState(BattleState.TurnStart02);
    }

    public void Exit() {}

    public void Update() {}
    

    // 생성된 유닛을 배틀매니저 리스트에 담는다.
    private void StartSpawn(List<UnitDataSO> creatUnitList, SpawnPoint spawnPoint)
    {
        if (UnitSelector.Players.Count == 0)
        {
            Debug.LogError("전투 유닛이 없습니다.");
            Debug.LogError("플레이어 수:" + UnitSelector.Players.Count);
            Debug.LogError("에너미 수:" + UnitSelector.Enemies.Count);
            return;
        }
        
        foreach (var unitData in creatUnitList)
        {
            UnitPresenter presenter = CreateUnit(unitData, spawnPoint);
            
            switch(presenter.Team)
            {
                case UnitTeam.Player:
                    _battleManager.AddPlayers(presenter);
                    break;
                case UnitTeam.Enemy:
                    _battleManager.AddEnemies(presenter);
                    break;
            }
        }
    }
    
    
    // 유닛 생성
    private UnitPresenter CreateUnit(UnitDataSO unitData, SpawnPoint spawnPoint)
    {
        UnitPresenter presenter = GameObject.Instantiate(
            unitData.UnitPrefab,
            _spawnPoints[(int)spawnPoint].position,
            Quaternion.identity
        ).Presenter;
        presenter.SetObjectName(unitData.Name);
        return presenter;
    }
    
    private enum SpawnPoint { PlayersSpawn, EnemiesSpawn }
}