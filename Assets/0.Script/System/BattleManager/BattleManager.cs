using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class BattleManager : MonoBehaviour, ITargetSelect, ISequenceSystem
{
    public static BattleManager Instance;
    public static event Action<UnitTeam> OnGameEnd;
    
    private BattleInfo _currentBattleInfo;
    private Dictionary<string, BattleInfo> _battleSequence;
    
    public event Action<BattleInfo> OnTargetSelected;
    public event Action<BattleInfo> OnResetTargetSelected;
    
    public List<UnitPresenter> Players => SpawnMgr.Players;
    public List<UnitPresenter> Enemies => SpawnMgr.Enemies;
    
    public EnemyAI EnemyAISystem { get; private set; }
    public Dictionary<string, BattleInfo> BattleSequence => _battleSequence;
    public UnitTeam Winners { get; private set; }
    public bool IsStartBattle { get; private set; }
    
    [field: SerializeField]public SpawnManager SpawnMgr { get; private set; }
    [field: SerializeField]public SkillActManager SkillActMgr { get; private set; }
    
    
    private void Awake()
    {
        Instance = this;
        _battleSequence =  new Dictionary<string, BattleInfo>();
        EnemyAISystem = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        FindGameStart(GameManager.Instance.transform).onClick.AddListener(() => IsStartBattle = true);
        SetState();
    }
    
    
    public void OnGameEnded(UnitTeam winner) => OnGameEnd?.Invoke(winner);
    public void SetWinners(UnitTeam winners) => Winners = winners;
    
    // BattleSequence 관련
    public void AddSequence(BattleInfo battleInfo) => _battleSequence.Add(battleInfo.Attacker.Data.Name ,battleInfo);
    
    public void SetSequenceSkill(UnitSkill skill)
    {
        var battleInfo = _battleSequence[skill.OwnerName];
        _battleSequence[skill.OwnerName] = new BattleInfo(battleInfo, skill);
        OnResetTargetSelected?.Invoke(battleInfo);
        
        _currentBattleInfo = _battleSequence[skill.OwnerName];
        Debug.Log("현재 BattleInfo스킬 <color=red>" + _currentBattleInfo.SelectedSkill + "</color>");
    }
    
    public void SetSequenceTarget(UnitPresenter target)
    {
        var newBattleInfo = new BattleInfo(_currentBattleInfo, _currentBattleInfo.SelectedSkill, target);
        _battleSequence[_currentBattleInfo.Attacker.Data.Name] = newBattleInfo;
        OnTargetSelected?.Invoke(newBattleInfo);
        Debug.Log("타겟 세팅" + _battleSequence[_currentBattleInfo.Attacker.Data.Name].Target);
    }

    public void SetSequence(string unitName, BattleInfo battleInfo)
    {
        _battleSequence[unitName] = battleInfo;
        OnTargetSelected?.Invoke(battleInfo);
    }

    // 배틀 스타트 버튼을 찾는 메서드
    private Button FindGameStart(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("BattleStart"))
                return child.GetComponent<Button>();

            var found = FindGameStart(child);
            if (found != null)
                return found;
        }
        return null;
    }
    
    public void Init()
    {
        _battleSequence.Clear();
        IsStartBattle = false;
    }
}


public enum BattleState
{
    BattleStart01, TurnStart02, TurnSequence03, ActSelect04, ActStart05, TurnEnd06, BattleEnd07
}