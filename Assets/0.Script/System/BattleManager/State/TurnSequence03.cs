using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 유닛 속도에 따라 행동순서 세팅
/// </summary>
public class TurnSequence03 : IState
{
    private BattleManager _battleManager;
    private List<UnitAndSpeed> _unitsAndSpeeds;
    
    public TurnSequence03(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _unitsAndSpeeds = new List<UnitAndSpeed>();
    }

    public void Enter()
    {
        _unitsAndSpeeds.Clear();
        
        SetSpeedDict();
        SetSequence();
        
        _battleManager.SetState(BattleState.ActSelect04);
    }

    public void Exit() {}

    public void Update() {}

    // 순서 정렬을 위해 speedDict에 집어넣는다.
    private void SetSpeedDict()
    {
        foreach (var player in _battleManager.Players)
        {
            _unitsAndSpeeds.Add(new UnitAndSpeed(player));
        }
        foreach (var enemy in _battleManager.Enemies)
        {
            _unitsAndSpeeds.Add(new UnitAndSpeed(enemy));
        }
        
        _unitsAndSpeeds.Sort((x,y) => y.Speed.CompareTo(x.Speed));
    }

    // 순서대로 시퀀스에 세팅
    private void SetSequence()
    {
        foreach (var structUnit in _unitsAndSpeeds)
        {
            _battleManager.SetSequence(structUnit.Unit);
            Debug.Log("시퀀스 세팅" + structUnit.Unit.Name + "/스피드" + structUnit.Unit.Speed);
        }
    }
    
    // 속도 계산용 구조체
    private struct UnitAndSpeed
    {
        public UnitPresenter Unit;
        public int Speed;

        public UnitAndSpeed(UnitPresenter unit)
        {
            Unit = unit;
            Speed = unit.Speed;
        }
    }
}