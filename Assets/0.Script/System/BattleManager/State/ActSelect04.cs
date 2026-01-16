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
    
}