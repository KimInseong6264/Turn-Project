using System.Collections.Generic;

public interface IUnitData
{
    string Name { get; }
    bool IsDead { get; }
    UnitTeam Team { get; }
    float AttLevel { get; }
    float DefLevel { get; }

    Dictionary<SkillType, UnitSkill> Skills { get; }
}
