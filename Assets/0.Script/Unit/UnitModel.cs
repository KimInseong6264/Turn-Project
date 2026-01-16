using System.Collections.Generic;
using UnityEngine;

public class UnitModel
{
    private int _hp;
    
    public int Hp => Mathf.Max(0, _hp);
    
    public string Name { get; private set; }
    public UnitTeam Team { get; private set; }
    public float AttLevel { get; private set; }
    public float DefLevel { get; private set; }
    public int Speed { get; private set; }
    public SkillBase SkillToUse { get; private set; }

    private Dictionary<SkillType, SkillBase> _skills;
    
    public UnitModel(UnitDataSO unitData, ISkillable  unitPresenter)
    {
        _hp = unitData.Hp;
        Name = unitData.Name;
        Team = unitData.Team;
        AttLevel = unitData.AttLevel;
        DefLevel = unitData.DefLevel;
        Speed = unitData.Speed;
        
        SetSkills(unitData, unitPresenter);
    }

    private void SetSkills(UnitDataSO unitData, ISkillable unitPresenter)
    {
        _skills = new Dictionary<SkillType, SkillBase>();

        if (unitData.SkillList == null || unitData.SkillList.Count == 0)
        {
            Debug.LogError(unitData.Name + "은 스킬프리팹이 없습니다.");
            return;
        }

        foreach (var skill in unitData.SkillList)
        {
            Debug.Log("스킬세팅" + skill);
            _skills.Add(skill.Type, SkillFactory.CreateSkill(skill, unitPresenter));
        }

        if(_skills.TryGetValue(SkillType.Skill01, out var skill01))
           SkillToUse = skill01;
    }
    public void SetSkillToUse(SkillType skillType) => SkillToUse = _skills[skillType];
}