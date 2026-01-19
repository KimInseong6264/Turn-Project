using UnityEngine;

public class BattleEnd07 : IState
{
    private BattleManager _battleManager;
    
    public BattleEnd07(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }
    
    public void Enter()
    {
        Debug.Log(_battleManager.Winners + "<color=red>가 이겼습니다.</color>");
    }

    public void Exit() {}

    public void Update() {}
}