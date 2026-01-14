using System;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : SingletonBase<BattleManager>
{
    private Dictionary<BattleState, IState> _states;
    private IState  _currentState;
    
    public List<UnitPresenter> Players { get; private set; }
    public List<UnitPresenter> Enemies { get; private set; }
    
    public Stack<UnitPresenter> UnitSequence { get; private set; }
    
    //
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
    //
    
    protected override void Awake()
    {
        base.Awake();
        Players =  new List<UnitPresenter>();
        Enemies = new List<UnitPresenter>();
        UnitSequence = new Stack<UnitPresenter>();
    }

    private void Start()
    {
        SetState();
    }

    // 상태 피턴 세팅
    private void SetState()
    {
        _states = new Dictionary<BattleState, IState>();
        _states.Add(BattleState.BattleStart, new BattleStart01(this));
        _states.Add(BattleState.TurnStart , new TurnStart02(this));
        _states.Add(BattleState.TurnSequence, new TurnSequence03(this));
        _states.Add(BattleState.ActSelect , new ActSelect04(this));
        _states.Add(BattleState.ActStart , new ActStart05(this));
        _states.Add(BattleState.TurnEnd , new TurnEnd06(this));
        _states.Add(BattleState.BattleEnd , new BattleEnd07(this));
        SetState(BattleState.BattleStart);
    }
    
    public void SetPlayers(UnitPresenter presenter) => Players.Add(presenter);
    public void SetEnemies(UnitPresenter presenter) => Enemies.Add(presenter);
    public void SetSequence(UnitPresenter presenter) => UnitSequence.Push(presenter);

    // 현재 상태 갱신
    public void SetState(BattleState state)
    {
        _currentState?.Exit();
        _currentState = _states[state];
        Debug.Log("<color=green>현재상태" + _currentState + "</color>");
        
        _currentState.Enter();

    }

    public void TestKey()
    {
        SetState(BattleState.TurnSequence);

    }
}

public enum BattleState
{
    BattleStart, TurnSequence,TurnStart, ActSelect, ActStart, TurnEnd, BattleEnd
}