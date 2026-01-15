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
        _battleManager.StartCoroutine(OnSkillInSequence());
    }

    public void Exit() {}

    public void Update() {}

    private IEnumerator OnSkillInSequence()
    {
        while (_battleManager.UnitSequence.Count > 0)
        {
            yield return CoroutineManager.GetWaitTime(1f);
            _battleManager.GetSequence().StartSkillExecute();
        }
        
        _battleManager.SetState(BattleState.TurnEnd06);
    }
}