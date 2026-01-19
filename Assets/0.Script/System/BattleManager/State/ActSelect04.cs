using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActSelect04 : IState
{
    private BattleManager _battleManager;
    
    public ActSelect04(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void Enter()
    {
        GameManager.Instance.UpdateUI(UIGroupName.ActSelectUI, true);
        GameManager.Instance.UpdateUI(UIGroupName.TargetSelectUI, true);

        SetEnuemySequence();
        
        Debug.Log("플레이어 입력을 기다립니다.");
    }

    public void Exit()
    {
        GameManager.Instance.UpdateUI(UIGroupName.ActSelectUI, false);
        GameManager.Instance.UpdateUI(UIGroupName.TargetSelectUI, false);
    }

    public void Update()
    {
        if (_battleManager.IsStartBattle)
            _battleManager.SetState(BattleState.ActStart05);
    }

    private void SetEnuemySequence()
    {
        var enemyAI = BattleManager.Instance.EnemyAISystema;
        foreach (var unit in _battleManager.BattleSequence.Values.ToList())
        {
            if(unit.Team == UnitTeam.Player)
                continue;

            _battleManager.SetSequence(unit.Attacker.Name, enemyAI.SetEnemyInfo(unit));
        }
    }
}