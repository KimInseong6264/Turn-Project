using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private Dictionary<BattleState, IState> _states;
    private IState  _currentState;
    
    public List<UnitPresenter> Players { get; private set; }
    public List<UnitPresenter> Enemies { get; private set; }
    public Dictionary<string, BattleInfo> BattleSequence { get; private set; }
    public UnitTeam Winners { get; private set; }
    public bool IsStartBattle { get; private set; }
    public EnemyAI EnemyAISystema { get; private set; }
    
    
    //
    private BattleInfo _currentBattleInfo;
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }

    public List<Text> attaker;
    public List<Text> skill;
    public List<Text> target;

    public void OnInfoStart() => StartCoroutine(OnUIInfo());
    public IEnumerator OnUIInfo()
    {
        yield return null;
        int count = 0;
        foreach (var battleInfo in BattleSequence.Values)
        {
            attaker[count].text = battleInfo.Attacker?.Data.Name;
            skill[count].text = battleInfo.SelectedSkill?.Name;
            target[count].text = battleInfo.Target?.Data.Name;
            count++;
        }
    }
    //
    
    private void Awake()
    {
        Instance = this;
        Players =  new List<UnitPresenter>();
        Enemies = new List<UnitPresenter>();
        BattleSequence = new Dictionary<string, BattleInfo>();
        EnemyAISystema = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        FindGameStart(GameManager.Instance.transform).onClick.AddListener(() => IsStartBattle = true);
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
    
    
    public void SetWinners(UnitTeam winners) => Winners = winners;
    public void AddPlayers(UnitPresenter presenter) => Players.Add(presenter);
    public void AddEnemies(UnitPresenter presenter) => Enemies.Add(presenter);
    
    // BattleSequence 관련
    public void AddSequence(BattleInfo battleInfo) => BattleSequence.Add(battleInfo.Attacker.Data.Name ,battleInfo);
    public void SetSequenceSkill(UnitSkill skill)
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
        BattleSequence[_currentBattleInfo.Attacker.Data.Name]
            = new BattleInfo(_currentBattleInfo, _currentBattleInfo.SelectedSkill, target);
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