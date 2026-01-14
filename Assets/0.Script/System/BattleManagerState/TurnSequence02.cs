using System.Collections.Generic;
using UnityEngine;

public class TurnSequence02 : IState
{
    private BattleManager _battleManager;
    private SortedDictionary<int, List<UnitPresenter>> _unitsSpeedDict;
    
    public TurnSequence02(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _unitsSpeedDict = new SortedDictionary<int, List<UnitPresenter>>();
    }

    public void Enter()
    {
        Debug.Log("턴 시작에 접어들엇다.");
        SetSpeedDict();

        SetSequence();
        
        _battleManager.SetState(BattleState.TurnStart);
    }

    public void Exit() {}

    public void Update() {}

    // 순서 정렬을 위해 speedDict에 집어넣는다.
    private void SetSpeedDict()
    {
        foreach (var enemy in _battleManager.Enemies)
        {
            int speed = enemy.Model.Speed;
            if (_unitsSpeedDict.TryGetValue(speed, out List<UnitPresenter> units))
                units.Add(enemy);
            else
                _unitsSpeedDict.Add(speed, new List<UnitPresenter> {enemy});
        }
        foreach (var player in _battleManager.Players)
        {
            int speed = player.Model.Speed;
            if (_unitsSpeedDict.TryGetValue(speed, out List<UnitPresenter> units))
                units.Add(player);
            else
                _unitsSpeedDict.Add(speed, new List<UnitPresenter> {player});
        }
    }

    // 순서대로 시퀀스에 세팅
    private void SetSequence()
    {
        foreach (var units in _unitsSpeedDict)
        {
            foreach (var unit in units.Value)
            {
                _battleManager.SetSequence(unit);
                Debug.Log("시퀀스 세팅" + unit.Model.Name + "/스피드" +  unit.Model.Speed);
            }
        }
    }
}