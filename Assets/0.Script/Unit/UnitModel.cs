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
    public Skill skill { get; private set; }
    
    public UnitModel(UnitDataSO unitData, ISkillable  skillable)
    {
        _hp = unitData.Hp;
        Name = unitData.Name;
        Team = unitData.Team;
        AttLevel = unitData.AttLevel;
        DefLevel = unitData.DefLevel;
        Speed = unitData.Speed;
        
        skill = SkillFactory.CreateSkill(Name ,skillable);
    }
}