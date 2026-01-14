using UnityEngine;

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
        
        _battleManager.SetState(BattleState.TurnSequence);
    }

    public void Exit() {}

    public void Update() {}

    private void StartSpawn()
    {
        if (UnitSelector.Instance.Players == null || UnitSelector.Instance.Enemies == null)
        {
            Debug.LogError("전투 유닛이 없습니다.");
            return;
        }
        
        // 유닛을 생성하고, 그 유닛을 배틀매니저 리스트에 담는다.
        foreach (var unit in UnitSelector.Instance.Players)
        {
            Debug.Log(unit.Name);
            UnitPresenter presenter = GameObject.Instantiate(
                UnitDataManager.Instance.GetUnitData(unit.Name).UnitPrefab,
                _spawnPoints[0].position, 
                Quaternion.identity
                ).Presenter;
            presenter.View.gameObject.name = unit.Name;
            
            _battleManager.SetPlayers(presenter);
        }
        
        foreach (var unit in UnitSelector.Instance.Enemies)
        {
            UnitPresenter presenter = GameObject.Instantiate(
                UnitDataManager.Instance.GetUnitData(unit.Name).UnitPrefab, 
                _spawnPoints[1].position, 
                Quaternion.identity
                ).Presenter;
            presenter.View.gameObject.name = unit.Name;
            
            _battleManager.SetEnemies(presenter);
        }
    }
}