using System.Collections.Generic;
using UnityEngine;

public class UnitModel
{
    private int _hp;
    
    public int Hp => Mathf.Max(0, _hp);
    
    public string Name { get; private set; }
    public Faction Team { get; private set; }
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
        SkillToUse = _skills[SkillType.Skill01];
    }

    private void SetSkills(UnitDataSO unitData, ISkillable unitPresenter)
    {
        _skills = new Dictionary<SkillType, SkillBase>();
        foreach (var skill in unitData.SkillList)
        {
            _skills.Add(skill.Type, SkillFactory.CreateSkill(skill, unitPresenter));
        }
    }
    public void SetSkillToUse(SkillType skillType) => SkillToUse = _skills[skillType];
}