using System.Collections;

public class ActStart05 : IState
{
    private BattleManager _battleManager;
    private IBattlePosition _battlePos;

    public ActStart05(BattleManager battleManager)
    {
        _battleManager = battleManager;
        _battlePos = battleManager.SpawnMgr;
    }

    public void Enter()
    {
        _battleManager.StartCoroutine(OnBattleExcute());
    }

    public void Exit() {}

    public void Update() {}

    
    private IEnumerator OnBattleExcute()
    {
        int count = 1;
        foreach (var battleInfo in _battleManager.BattleSequence.Values)
        {
            if (battleInfo.Attacker.Data.IsDead || battleInfo.Target.Data.IsDead)
                continue;

            SetPos(battleInfo);
            yield return battleInfo.OnBattleExcute();
        }
        yield return CoroutineManager.GetWaitTime(1.5f);

        _battleManager.SetState(BattleState.TurnEnd06);
    }

    
    private void SetPos(BattleInfo battleInfo)
    {
        var attackerTr = battleInfo.Attacker.MyObject.transform;
        var targetTr = battleInfo.Target.MyObject.transform;

        switch (battleInfo.Attacker.Data.Team)
        {
            case UnitTeam.Player:
                attackerTr.position = _battlePos.PlayerPos;
                targetTr.position = _battlePos.EnemyPos;
                break;
            case UnitTeam.Enemy:
                attackerTr.position = _battlePos.EnemyPos;
                targetTr.position = _battlePos.PlayerPos;
                break;
        }
    }
}