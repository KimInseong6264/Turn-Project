using System;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : SingletonBase<BattleManager>
{
    private Dictionary<BattleState, IState> _states;
    private IState  _currentState;
    
    public List<UnitPresenter> Players { get; private set; }
    public List<UnitPresenter> Enemies { get; private set; }
    public Queue<UnitPresenter> UnitSequence { get; private set; }
    public Dictionary<UnitPresenter, UnitPresenter> Targets { get; private set; }
    public bool IsStartBattle { get; private set; }
    
    //
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
    //
    
    protected override void Awake()
    {
        base.Awake();
        Players =  new List<UnitPresenter>();
        Enemies = new List<UnitPresenter>();
        UnitSequence = new Queue<UnitPresenter>();
        Targets = new Dictionary<UnitPresenter, UnitPresenter>();
        
        
        //==============================================
        gameObject.SetActive(false);
        //==============================================
        
        
    }

    private void Start()
    {
        SetState();
    }

    private void Update()
    {
        _currentState?.Update();
    }

    // 상태 피턴 세팅
    private void SetState()
    {
        _states = new Dictionary<BattleState, IState>();
        _states.Add(BattleState.BattleStart01, new BattleStart01(this));
        _states.Add(BattleState.TurnStart02 , new TurnStart02(this));
        _states.Add(BattleState.TurnSequence03, new TurnSequence03(this));
        _states.Add(BattleState.ActSelect04 , new ActSelect04(this));
        _states.Add(BattleState.ActStart05 , new ActStart05(this));
        _states.Add(BattleState.TurnEnd06 , new TurnEnd06(this));
        _states.Add(BattleState.BattleEnd07 , new BattleEnd07(this));
        SetState(BattleState.BattleStart01);
    }
    // 현재 상태 갱신
    public void SetState(BattleState state)
    {
        _currentState?.Exit();
        _currentState = _states[state];
        Debug.Log("<color=green>현재상태" + _currentState + "</color>");
        
        _currentState.Enter();
    }
    
    
    public void SetStartBattle(bool startBattle) => IsStartBattle = startBattle;
    public void SetPlayers(UnitPresenter presenter) => Players.Add(presenter);
    public void SetEnemies(UnitPresenter presenter) => Enemies.Add(presenter);
    
    public void SetSequence(UnitPresenter presenter) => UnitSequence.Enqueue(presenter);
    public UnitPresenter GetSequence() => UnitSequence.Dequeue();
}


// 스트럭트
public struct BattleInfo
{
    public int Speed { get; }
    public UnitPresenter Attacker { get; }
    public UnitPresenter Target { get; }

    public BattleInfo(UnitPresenter attacker, int speed, UnitPresenter target =null)
    {
        Speed = speed;
        Attacker = attacker;
        Target = target;
    }
}


public enum BattleState
{
    BattleStart01, TurnStart02, TurnSequence03, ActSelect04, ActStart05, TurnEnd06, BattleEnd07
}