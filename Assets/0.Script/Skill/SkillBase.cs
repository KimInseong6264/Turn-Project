using System;
using System.Collections;
using UnityEngine;

public abstract class SkillBase
{
    protected string _name;
    protected int _coinCount;
    protected int _coinValue;
    protected ISkillable _unit;

    protected SkillBase(ISkillable unit)
    {
        _unit = unit;
    }
    
    public abstract IEnumerator Execute();
}

/// <summary>
/// 스킬 생성 팩토리
/// </summary>
public static class SkillFactory
{
    public static SkillBase CreateSkill(string name, ISkillable unit)
    {
        switch (name)
        {
            case "플레이어" or "플레이어1" or "플레이어2":
                // Debug.Log(name + "스킬생성");
                return new Player.Skill01(unit);
            case "에너미" or "에너미1" or "에너미2":
                // Debug.Log(name + "스킬생성");
                return new Enemy.Skill01(unit);
            default:
                Debug.LogError(name + "은 스킬을 끼울 수 없습니다.");
                return null;
        }
    }
}