using System.Collections.Generic;
using UnityEngine;

public interface IUnitData
{
    string Name { get; }
    bool IsDead { get; }
    UnitTeam Team { get; }
    float AttLevel { get; }
    float DefLevel { get; }
    
    Sprite UnitIcon { get; }

    Dictionary<SkillType, UnitSkill> Skills { get; }
}
