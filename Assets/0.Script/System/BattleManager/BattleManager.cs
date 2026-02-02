using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class BattleManager : MonoBehaviour, ITargetSelect
{
    public static BattleManager Instance;
    public event Action<BattleInfo> OnTargetSelected;
    public event Action<BattleInfo> OnResetTargetSelected;
    
    public List<UnitPresenter> Players => SpawnMgr.Players;
    public List<UnitPresenter> Enemies => SpawnMgr.Enemies;
    public Dictionary<string, BattleInfo> BattleSequence { get; private set; }
    
    public UnitTeam Winners { get; private set; }
    public bool IsStartBattle { get; private set; }
    
    public EnemyAI EnemyAISystema { get; private set; }
    [field: SerializeField]public SpawnManager SpawnMgr { get; private set; }
    [field: SerializeField]public SkillActManager SkillActMgr { get; private set; }
    
    
    //
    private BattleInfo _currentBattleInfo;
    //
    
    
    private void Awake()
    {
        Instance = this;
        BattleSequence = new Dictionary<string, BattleInfo>();
        EnemyAISystema = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        FindGameStart(GameManager.Instance.transform).onClick.AddListener(() => IsStartBattle = true);
        SetState();
    }
    
    
    public void SetWinners(UnitTeam winners) => Winners = winners;
    
    // BattleSequence 관련
    public void AddSequence(BattleInfo battleInfo) => BattleSequence.Add(battleInfo.Attacker.Data.Name ,battleInfo);
    
    public void SetSequenceSkill(UnitSkill skill)
    {
        var battleInfo = BattleSequence[skill.OwnerName];
        BattleSequence[skill.OwnerName] = new BattleInfo(battleInfo, skill);
        Debug.Log("스킬 세팅" + BattleSequence[skill.OwnerName].SelectedSkill);
        OnResetTargetSelected?.Invoke(battleInfo);
        
        //
        _currentBattleInfo = BattleSequence[skill.OwnerName];
        Debug.Log("현재 BattleInfo스킬 <color=red>" + _currentBattleInfo.SelectedSkill + "</color>");
        //
    }
    
    public void SetSequenceTarget(UnitPresenter target)
    {
        var newBattleInfo = new BattleInfo(_currentBattleInfo, _currentBattleInfo.SelectedSkill, target);
        BattleSequence[_currentBattleInfo.Attacker.Data.Name] = newBattleInfo;
        OnTargetSelected?.Invoke(newBattleInfo);
        Debug.Log("타겟 세팅" + BattleSequence[_currentBattleInfo.Attacker.Data.Name].Target);
    }
    public void SetSequence(string unitName, BattleInfo battleInfo) => BattleSequence[unitName] = battleInfo;
    
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
        BattleSequence.Clear();
        IsStartBattle = false;
    }
}


public enum BattleState
{
    BattleStart01, TurnStart02, TurnSequence03, ActSelect04, ActStart05, TurnEnd06, BattleEnd07
}