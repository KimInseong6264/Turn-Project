using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActStart05 : IState
{
    private BattleManager _battleManager;
    private Stack<UnitPresenter> _unitSequence;
    
    public ActStart05(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _unitSequence = battleManager.UnitSequence;
    }

    public void Enter()
    {
        _battleManager.StartCoroutine(OnSkillInSequence());
    }

    public void Exit() {}

    public void Update() {}

    private IEnumerator OnSkillInSequence()
    {
        while (_unitSequence.Count > 0)
        {
            yield return CoroutineManager.GetWaitTime(1f);
            _unitSequence.Pop().StartSkillExecute();
        }
        
        Debug.Log("스택은 텅 비어야 해" + _unitSequence.Count);
        _battleManager.SetState(BattleState.TurnEnd06);
    }
}