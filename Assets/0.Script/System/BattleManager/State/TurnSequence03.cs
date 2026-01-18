using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 유닛 속도에 따라 행동순서 세팅
/// </summary>
public class TurnSequence03 : IState
{
    private BattleManager _battleManager;
    private List<BattleInfo> _battleInfos;
    
    public TurnSequence03(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _battleInfos = new List<BattleInfo>();
    }

    public void Enter()
    {
        _battleInfos.Clear();
        
        SetSpeedSort();
        SetSequence();
        
        _battleManager.SetState(BattleState.ActSelect04);
        
    }

    public void Exit() {}

    public void Update() {}

    // 순서 정렬을 위해 speedDict에 집어넣는다.
    private void SetSpeedSort()
    {
        foreach (var player in _battleManager.Players)
        {
            _battleInfos.Add(new BattleInfo(player));
        }
        foreach (var enemy in _battleManager.Enemies)
        {
            _battleInfos.Add(new BattleInfo(enemy));
        }
        
        _battleInfos.Sort((x,y) => y.Speed.CompareTo(x.Speed));
    }

    // 순서대로 시퀀스에 세팅
    private void SetSequence()
    {
        foreach (var battleInfo in _battleInfos)
        {
            _battleManager.AddSequence(battleInfo);
            Debug.Log("시퀀스 세팅" + battleInfo.Attacker.Name + "/스피드" + battleInfo.Speed);
        }
    }
}