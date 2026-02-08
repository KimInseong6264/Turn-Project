using System.Collections;
using UnityEngine;

public class ActSelect04 : IState
{
    private BattleManager _battleManager;
    private EnemyAI _enemyAI;
    
    public ActSelect04(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _enemyAI = battleManager.EnemyAISystem;
    }

    public void Enter()
    {
        GameManager.Instance.UpdateUI(UIGroupName.ActSelectUI, true);
        GameManager.Instance.UpdateUI(UIGroupName.TargetSelectUI, true);

        _battleManager.StartCoroutine(SetEnemySequence());
        
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

    private IEnumerator SetEnemySequence()
    {
        yield return null;
        
        foreach (var unit in _battleManager.BattleSequence.Values)
        {
            if(unit.AttackerTeam == UnitTeam.Player || unit.Attacker.Data.IsDead)
                continue;

            _battleManager.SetSequence(unit.Attacker.Data.Name, _enemyAI.SetEnemyInfo(unit));
        }
    }
}