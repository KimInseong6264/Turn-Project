using UnityEngine;

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
        StartSpawn();
        
        _battleManager.SetState(BattleState.TurnStart02);
    }

    public void Exit() {}

    public void Update() {}

    private void StartSpawn()
    {
        if (UnitSelector.Players.Count == 0)
        {
            Debug.LogError("전투 유닛이 없습니다.");
            Debug.LogError("플레이어 수:" + UnitSelector.Players.Count);
            Debug.LogError("에너미 수:" + UnitSelector.Enemies.Count);
            return;
        }
        
        // 유닛을 생성하고, 그 유닛을 배틀매니저 리스트에 담는다.
        foreach (var unit in UnitSelector.Players)
        {
            UnitPresenter presenter = GameObject.Instantiate(
                unit.UnitPrefab,
                _spawnPoints[0].position, 
                Quaternion.identity
                ).Presenter;
            presenter.SetObjectName(unit.Name);
            
            _battleManager.AddPlayers(presenter);
        }
        
        foreach (var unit in UnitSelector.Enemies)
        {
            UnitPresenter presenter = GameObject.Instantiate(
                unit.UnitPrefab, 
                _spawnPoints[1].position, 
                Quaternion.identity
                ).Presenter;
            presenter.SetObjectName(unit.Name);
            
            _battleManager.AddEnemies(presenter);
        }
    }
}