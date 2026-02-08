using System.Collections;
using UnityEngine;

public interface IUnit
{
    IUnitData Data { get; }
    IActable Actable { get; }
    IHitable Hitable { get; }
    GameObject MyObject { get; }

    void SetSkill(SkillType skillType);
    IEnumerator StartSkillExecute(BattleInfo battleInfo);
}

public interface IHitable
{
    void OnHit(BattleInfo battleInfo, int damage, KnockbackInfo? knockbackInfo = null);
}