using System.Collections.Generic;
using UnityEngine;


public class BattleManager : SingletonBase<BattleManager>
{
    //
    public List<UnitModel> Players;
    public List<UnitModel> Enemies;
    //
    
    private List<IState> _states;
    private IState  _currentState;

    //
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
    //
    
    protected override void Awake()
    {
        base.Awake();
        
        SetState();
    }

    // 상태 피턴 세팅
    private void SetState()
    {
        _states = new List<IState>
        {
            new BattleStart01(this),
            new TurnSequence02(this),
            new TurnStart03(this),
            new ActSelect04(this),
            new ActStart05(this),
            new TurnEnd06(this),
            new BattleEnd07(this)
        };
        SetState(_states[0]);
    }

    // 현재 상태 갱신
    public void SetState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}