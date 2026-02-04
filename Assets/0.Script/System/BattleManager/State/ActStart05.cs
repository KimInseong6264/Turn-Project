using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActStart05 : IState
{
    private BattleManager _battleManager;
    
    public ActStart05(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void Enter()
    {
        _battleManager.StartCoroutine(OnBattleExcute());
    }

    public void Exit() {}

    public void Update() {}

    private IEnumerator OnBattleExcute()
    {
        foreach (var battleInfo in _battleManager.BattleSequence.Values)
        {
            if(battleInfo.Attacker.Data.IsDead || battleInfo.Target.Data.IsDead)
                continue;
                
            yield return battleInfo.OnBattleExcute();
        }
        yield return CoroutineManager.GetWaitTime(1.5f);

        _battleManager.SetState(BattleState.TurnEnd06);
    }
    
    
}