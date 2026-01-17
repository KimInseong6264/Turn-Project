using System.Collections.Generic;
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
        
        Debug.Log("플레이어 입력을 기다립니다.");
    }

    public void Exit()
    {
        GameManager.Instance.UpdateUI(UIGroupName.ActSelectUI, false);
    }

    public void Update()
    {
        if(_battleManager.IsStartBattle)
            _battleManager.SetState(BattleState.ActStart05);
    }

    private void UpdateUI(UIGroupName uiGroupName, string createTag = "")
    {
        GameManager.Instance.UpdateUI(uiGroupName, true);

        foreach (var unitData in _battleManager.Players)
        {
            // var clickObj = GameManager.Instance.CreateButton<ClickObject>(uiGroupName, createTag);
            // clickObj.OnClick += () => SkillSelect();
        }
    }

    private void UpdateSkillUI(UnitPresenter unit, UIGroupName uiGroupName, string createTag = "")
    {
        foreach (var skill in unit.GetSkills())
        {
            // var clickObj = GameManager.Instance.CreateButton<ClickObject>(uiGroupName, createTag);
            
        }
    }
    
    private void SkillSelect()
    {
        
    }
    
    
}