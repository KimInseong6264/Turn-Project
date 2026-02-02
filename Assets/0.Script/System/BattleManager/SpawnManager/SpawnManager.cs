using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    private List<ISpawn> _spawners;
    private List<IUnitSetPos> _unitSetPosList;
    private List<UnitPresenter> _players;
    private List<UnitPresenter> _enemies;
    [SerializeField] private Transform[] _spawnPoints;

    public List<UnitPresenter> Players => _players;
    public List<UnitPresenter> Enemies => _enemies;
    
    private void Awake()
    {
        _spawners =  new List<ISpawn>();
        _unitSetPosList = new List<IUnitSetPos>();
        
        foreach (var spawnPoint in _spawnPoints)
        {
            var spawner = spawnPoint.GetComponent<ISpawn>();
            spawner.OnSpawningCompleted += SetUnits;
            _spawners.Add(spawner);
            
            var unitSetPos = spawnPoint.GetComponent<IUnitSetPos>();
            _unitSetPosList.Add(unitSetPos);
        }
    }

    // 참조용 플레이어, 에너미 리스트에 담는다.
    private void SetUnits(List<UnitPresenter> units)
    {
        if(units == null || units.Count == 0)
            return;
        
        switch (units[0].Data.Team)
        {
            case UnitTeam.Player:
                _players = units;
                break;
            
            case UnitTeam.Enemy:
                _enemies = units;
                break;
        }
    }
    
    public void OnSpawn(List<UnitDataSO> playerDataList, List<UnitDataSO> enemyDataList)
    {
        _spawners[0].OnSpawn(playerDataList);
        _spawners[1].OnSpawn(enemyDataList);
    }

    public void SetUnitsPos(List<UnitPresenter> units)
    {
        switch (units[0].Data.Team)
        {
            case UnitTeam.Player:
                _unitSetPosList[0].SetUnitsPosition(units);
                break;
            
            case UnitTeam.Enemy:
                _unitSetPosList[1].SetUnitsPosition(units);
                break;
        }
    }
}