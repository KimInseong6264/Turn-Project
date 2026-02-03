using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIActSelect : UIGroup
{
    private List<ClickObject> _buttons;
    private UnityObjectPull<ClickObject> _actSelectPull;
    
    [SerializeField] private ClickObject _actSelectPrefab;
    
    
    protected override void Awake()
    {
        _actSelectPull = new UnityObjectPull<ClickObject>(_actSelectPrefab, 5, _objectPullTransform);
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
        foreach (var battleInfo in unitSequence.Values)
        {
            if(battleInfo.AttackerTeam == UnitTeam.Enemy || battleInfo.Attacker.Data.IsDead)
                continue;

            CreateSkillButton(battleInfo.Attacker);
        }
    }

    // 배틀 플레이어의 스킬버튼 생성
    private void CreateSkillButton(IUnit player)
    {
        foreach (var skill in player.Data.Skills.Values)
        {
            var obj = _actSelectPull.GetPull();
            SetSkillButton(obj, skill);
            _buttons.Add(obj);
        }
    }

    // 스킬버튼을 설정
    private void SetSkillButton(ClickObject clickObject, UnitSkill skill)
    {
        if (skill.SkillIcon == null)
            Debug.Log("스킬 아이콘 없음");

        clickObject.GetComponent<Image>().sprite = skill.SkillIcon;
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