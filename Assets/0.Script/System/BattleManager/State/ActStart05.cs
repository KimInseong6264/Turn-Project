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
        foreach (var battleInfo in _battleManager.BattleSequence)
        {
            yield return CoroutineManager.GetWaitTime(1f);
            if(battleInfo.Value.Target != null)
                battleInfo.Value.OnBattleExcute();
            else
            {
                Debug.LogWarning(battleInfo.Key + "타겟 없음");
            }
        }
        
        _battleManager.SetState(BattleState.TurnEnd06);
    }
    
    
}