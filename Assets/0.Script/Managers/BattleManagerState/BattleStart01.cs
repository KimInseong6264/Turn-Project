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
        
        foreach (var unit in UnitSelector.Instance.Players)
        {
            GameObject obj = UnitDataManager.Instance.GetUnitData(unit.Name).UnitPrefab;
            GameObject.Instantiate(obj, _spawnPoints[0].position, Quaternion.identity);
        }
        foreach (var unit in UnitSelector.Instance.Enemies)
        {
            GameObject obj = UnitDataManager.Instance.GetUnitData(unit.Name).UnitPrefab;
            GameObject.Instantiate(obj, _spawnPoints[1].position, Quaternion.identity);
        }
    }
}