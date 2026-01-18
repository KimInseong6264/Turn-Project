using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIActSelect : UIGroup
{
    private List<ClickObject> _buttons;
    private UnityObjectPull<ClickObject> _actSelectPull;
    
    [SerializeField] private Transform _selectListTransform;
    [SerializeField] private ClickObject _skillSelectPrefab;
    
    protected override void Awake()
    {
        _actSelectPull = new UnityObjectPull<ClickObject>(_skillSelectPrefab, 5, _selectListTransform);
        _buttons = new List<ClickObject>();
        base.Awake();
    }

    private void OnEnable()
    {
        _buttons.Clear();
        
        OnCreateButton();
    }

    private void OnDisable()
    {
        ReleaseButons();
    }
    
    
    // 플레이어에 해당하는 스킬버튼 묶음 생성
    public void OnCreateButton()
    {
        var unitSequence = BattleManager.Instance.BattleSequence;
        foreach (var battleInfo in unitSequence)
        {
            if(battleInfo.Value.Team == UnitTeam.Enemy)
                continue;

            CreateSkillButton(battleInfo.Value);
        }
    }

    // 배틀 플레이어의 스킬버튼 생성
    private void CreateSkillButton(BattleInfo battleInfo)
    {
        foreach (var skill in battleInfo.Attacker.GetSkills())
        {
            var obj = _actSelectPull.GetPull();
            SetSkillButton(obj, skill.Value);
            _buttons.Add(obj);
        }
    }

    // 스킬버튼을 설정
    private void SetSkillButton(ClickObject clickObject, SkillBase skill)
    {
        clickObject.gameObject.name = skill.Name + " Buton";
        clickObject.GetComponentInChildren<Text>().text = skill.Name;
        clickObject.OnClick += () => BattleManager.Instance.SetSequenceSkill(skill);
    }

    private void ReleaseButons()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.name = "ActSelectButton";
            _actSelectPull.Release(button);
        }
    }
}