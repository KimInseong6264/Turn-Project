using System;
using System.Collections;
using UnityEngine;

public abstract class SkillBase
{
    protected string _name;
    protected int _coinCount;
    protected int _coinValue;
    protected ISkillable _unit;

    protected SkillBase(SkillDataSO skillData, ISkillable unit)
    {
        _name = skillData.Name;
        _coinCount = skillData.CoinCount;
        _coinValue = skillData.CoinValue;
        _unit = unit;
    }
    
    public abstract IEnumerator Execute();
}

/// <summary>
/// 스킬 생성 팩토리
/// </summary>
public static class SkillFactory
{
    public static SkillBase CreateSkill(SkillDataSO skillDataSo, ISkillable unit)
    {
        switch (skillDataSo.OwnerName)
        {
            case "플레이어" or "플레이어1" or "플레이어2":
                // Debug.Log(name + "스킬생성");
                return new Player.Skill01(skillDataSo, unit);
            case "에너미" or "에너미1" or "에너미2":
                // Debug.Log(name + "스킬생성");
                return new Enemy.Skill01(skillDataSo, unit);
            default:
                Debug.LogError(skillDataSo.OwnerName + "은 스킬을 끼울 수 없습니다.");
                return null;
        }
    }
}