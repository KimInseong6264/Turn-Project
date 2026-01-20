using System.Collections.Generic;
using UnityEngine;

public class UnitModel
{
    private int _hp;
    
    public readonly int MaxHp;
    public int Hp => Mathf.Max(0, _hp);
    
    public string Name { get; private set; }
    public UnitTeam Team { get; private set; }
    public float AttLevel { get; private set; }
    public float DefLevel { get; private set; }
    public int Speed { get; private set; }
    public bool IsDead => _hp <= 0;
    
    public Dictionary<SkillType, SkillBase> Skills { get; private set; }
    public SkillBase SkillToUse { get; private set; }

    
    public UnitModel(UnitDataSO unitData, ISkillable  unitPresenter)
    {
        MaxHp = unitData.Hp;
        _hp = unitData.Hp;
        Name = unitData.Name;
        Team = unitData.Team;
        AttLevel = unitData.AttLevel;
        DefLevel = unitData.DefLevel;
        Speed = unitData.Speed;
        
        SetSkills(unitData, unitPresenter);
    }

    // 생성시, 스킬을 세팅(들고있게 됨)
    private void SetSkills(UnitDataSO unitData, ISkillable unitPresenter)
    {
        Skills = new Dictionary<SkillType, SkillBase>();
        if (unitData.SkillList == null || unitData.SkillList.Count == 0)
        {
            Debug.LogError(unitData.Name + "은 스킬프리팹이 없습니다.");
            return;
        }

        foreach (var skill in unitData.SkillList)
        {
            Debug.Log("스킬세팅" + skill);
            bool isSkill = Skills.TryAdd(skill.Type, SkillFactory.CreateSkill(skill, unitPresenter));
            
            if(!isSkill)
                Debug.LogError(skill.OwnerName + "중복된 스킬이 들어있습니다.");
        }

        if(Skills.TryGetValue(SkillType.Skill01, out var skill01))
           SkillToUse = skill01;
    }
    
    
    public void TakeDamage(int damage) => _hp -= damage;
    public void SetSkillToUse(SkillType skillType) => SkillToUse = Skills[skillType];
}