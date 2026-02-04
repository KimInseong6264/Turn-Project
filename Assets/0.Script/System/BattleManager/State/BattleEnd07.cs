using System.Collections;
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

        _battleManager.StartCoroutine(GameEnd());
    }

    public void Exit() {}

    public void Update() {}

    private IEnumerator GameEnd()
    {
        yield return CoroutineManager.GetWaitTime(1.5f);
        
        GameManager.Instance.UpdateUI(UIGroupName.GameEnd, true);
        _battleManager.OnGameEnded(_battleManager.Winners);
        yield return CoroutineManager.GetWaitTime(1f);
        
        GameManager.Instance.OnLoadScene(3);
    }
}