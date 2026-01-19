using System;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : SingletonBase<BattleManager>
{
    private Dictionary<BattleState, IState> _states;
    private IState  _currentState;
    
    public List<UnitPresenter> Players { get; private set; }
    public List<UnitPresenter> Enemies { get; private set; }
    public Dictionary<string, BattleInfo> BattleSequence { get; private set; }
    public bool IsStartBattle { get; private set; }
    
    //
    private BattleInfo _currentBattleInfo;
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
    //
    
    protected override void Awake()
    {
        base.Awake();
        Players =  new List<UnitPresenter>();
        Enemies = new List<UnitPresenter>();
        BattleSequence = new Dictionary<string, BattleInfo>();
        
        
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

    # region 상태 패턴
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
        
        //=============================================================
        Debug.Log("<color=green>현재상태" + _currentState + "</color>");
        //=============================================================
        
        _currentState.Enter();
    }
    #endregion
    
    
    public void SetStartBattle(bool startBattle) => IsStartBattle = startBattle;
    public void AddPlayers(UnitPresenter presenter) => Players.Add(presenter);
    public void AddEnemies(UnitPresenter presenter) => Enemies.Add(presenter);
    
    // BattleSequence 관련
    public void AddSequence(BattleInfo battleInfo) => BattleSequence.Add(battleInfo.Attacker.Name ,battleInfo);
    public void SetSequenceSkill(SkillBase skill)
    {
        var battleInfo = BattleSequence[skill.OwnerName];
        BattleSequence[skill.OwnerName] = new BattleInfo(battleInfo, skill);
        Debug.Log("스킬 세팅" + BattleSequence[skill.OwnerName].SelectedSkill);
        
        //
        _currentBattleInfo = BattleSequence[skill.OwnerName];
        Debug.Log("현재 BattleInfo스킬 <color=red>" + _currentBattleInfo.SelectedSkill + "</color>");
        //
    }
    public void SetSequenceTarget(UnitPresenter target)
    {
        BattleSequence[_currentBattleInfo.Attacker.Name]
            = new BattleInfo(_currentBattleInfo, _currentBattleInfo.SelectedSkill, target);
        Debug.Log("타겟 세팅" + BattleSequence[_currentBattleInfo.Attacker.Name].Target);
    }
    
    public void Init()
    {
        BattleSequence.Clear();
        SetStartBattle(false); 
    }
}


// 배틀정보 저장 구조체
public struct BattleInfo
{
    public int Speed { get; }
    public UnitTeam Team { get; }
    public SkillBase SelectedSkill { get; }
    public UnitPresenter Attacker { get; }
    public UnitPresenter Target { get; }

    public BattleInfo(UnitPresenter attacker)
    {
        Speed = attacker.Speed;
        Team =  attacker.Team;
        Attacker = attacker;
        SelectedSkill = null;
        Target = null;
    }

    // 스킬만 갱신하는 생성자
    public BattleInfo(BattleInfo mySelf, SkillBase skill, UnitPresenter target = null)
    {
        this = new BattleInfo(mySelf.Attacker);
        this.SelectedSkill = skill;
        this.Target = target;
    }

    public void OnBattleExcute()
    {
        Attacker.StartSkillExecute(Target);
    }
}


public enum BattleState
{
    BattleStart01, TurnStart02, TurnSequence03, ActSelect04, ActStart05, TurnEnd06, BattleEnd07
}