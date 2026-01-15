using System;
using System.Collections;
using UnityEngine;

public abstract class Skill
{
    protected string _name;
    protected int _coinCount;
    protected int _coinValue;
    protected ISkillable _unit;

    protected Skill(ISkillable unit)
    {
        _unit = unit;
    }
    
    public abstract IEnumerator Execute();
}

public static class SkillFactory
{
    public static Skill CreateSkill(string name, ISkillable unit)
    {
        switch (name)
        {
            case "플레이어":
                // Debug.Log(name + "스킬생성");
                return new Player.Skill01(unit);
            case "에너미":
                // Debug.Log(name + "스킬생성");
                return new Enemy.Skill01(unit);
            default:
                Debug.LogError(name + "은 스킬을 끼울 수 없습니다.");
                return null;
        }
    }
}