using System.Collections.Generic;

public interface IUnitData
{
    string Name { get; }
    bool IsDead { get; }

    Dictionary<SkillType, UnitSkill> Skills { get; }
}
