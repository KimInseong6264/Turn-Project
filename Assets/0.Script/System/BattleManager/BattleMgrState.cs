using System.Collections.Generic;
using UnityEngine;


public partial class BattleManager
{
    private Dictionary<BattleState, IState> _states;
    private IState  _currentState;
    
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
    
    private void Update()
    {
        _currentState?.Update();
    }
}